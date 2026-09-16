import axios, { AxiosError, type InternalAxiosRequestConfig } from 'axios'
import type { AuthResponse, RefreshTokenRequest } from './types'
import { clearSession, loadSession, saveSession } from '../auth/storage'

export const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
})

// Requests to these paths never carry (or need) a bearer token, and a 401
// from them is a real credential failure, not an expired-session signal -
// the refresh interceptor must never try to "fix" a failure from here.
const PUBLIC_PATHS = [
  '/Auth/register',
  '/Auth', // login (POST /Auth "")
  '/Auth/refresh',
  '/Auth/confirm-email',
  '/Auth/resend-email-confirm',
  '/Auth/forget-password',
  '/Auth/reset-password',
]

function isPublicRequest(url: string | undefined): boolean {
  if (!url) return false
  const path = url.split('?')[0]
  return PUBLIC_PATHS.some((p) => path === p || path === `/${p.replace(/^\//, '')}`)
}

apiClient.interceptors.request.use((config) => {
  const session = loadSession()
  if (session && !isPublicRequest(config.url)) {
    config.headers.Authorization = `Bearer ${session.token}`
  }
  return config
})

/** Called once when a refresh attempt fails and the session must be torn down. */
let onSessionExpired: (() => void) | null = null
export function registerSessionExpiredHandler(handler: () => void): void {
  onSessionExpired = handler
}

// Refresh-token rotation on the backend revokes the old refresh token the
// moment it's used, so two concurrent 401s must never each fire their own
// refresh call - the second would present an already-revoked token and fail.
// This promise is shared by every request that hits a 401 while a refresh is
// already in flight.
let refreshPromise: Promise<string> | null = null

async function refreshAccessToken(): Promise<string> {
  const session = loadSession()
  if (!session) throw new Error('No session to refresh')

  const body: RefreshTokenRequest = { token: session.token, refreshToken: session.refreshToken }
  const response = await axios.post<AuthResponse>('/Auth/refresh', body, {
    baseURL: import.meta.env.VITE_API_BASE_URL,
  })
  saveSession(response.data)
  return response.data.token
}

apiClient.interceptors.response.use(
  (response) => response,
  async (error: AxiosError) => {
    const originalRequest = error.config as (InternalAxiosRequestConfig & { _retried?: boolean }) | undefined

    const isAuthFailure = error.response?.status === 401
    const alreadyRetried = originalRequest?._retried
    const isExemptRequest = isPublicRequest(originalRequest?.url)
    const hasSession = loadSession() !== null

    if (!isAuthFailure || alreadyRetried || isExemptRequest || !hasSession || !originalRequest) {
      return Promise.reject(error)
    }

    originalRequest._retried = true

    try {
      refreshPromise ??= refreshAccessToken().finally(() => {
        refreshPromise = null
      })
      const newToken = await refreshPromise
      originalRequest.headers.Authorization = `Bearer ${newToken}`
      return apiClient(originalRequest)
    } catch {
      clearSession()
      onSessionExpired?.()
      return Promise.reject(error)
    }
  },
)
