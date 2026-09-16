import type { AuthResponse } from '../api/types'

// Persists the current session across reloads. localStorage (not cookies) is
// used because the backend issues the token/refresh token directly in the
// AuthResponse body, not as a Set-Cookie header - there is no cookie-based
// session to piggyback on.
const STORAGE_KEY = 'learnhub.session'

export interface StoredSession {
  token: string
  refreshToken: string
  refreshTokenExpiration: string
}

export function loadSession(): StoredSession | null {
  const raw = localStorage.getItem(STORAGE_KEY)
  if (!raw) return null
  try {
    return JSON.parse(raw) as StoredSession
  } catch {
    return null
  }
}

export function saveSession(auth: AuthResponse): void {
  const session: StoredSession = {
    token: auth.token,
    refreshToken: auth.refreshToken,
    refreshTokenExpiration: auth.refreshTokenExpiration,
  }
  localStorage.setItem(STORAGE_KEY, JSON.stringify(session))
}

export function clearSession(): void {
  localStorage.removeItem(STORAGE_KEY)
}
