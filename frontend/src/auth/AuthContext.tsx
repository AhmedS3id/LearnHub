import { createContext, useCallback, useContext, useEffect, useMemo, useState, type ReactNode } from 'react'
import * as authApi from '../api/auth'
import type { LoginRequest, RegisterRequest } from '../api/types'
import { decodeToken, isExpired, type TokenClaims } from './jwt'
import { registerSessionExpiredHandler } from '../api/client'
import { clearSession, loadSession, saveSession, type StoredSession } from './storage'
import type { Permission, Role } from './permissions'

interface AuthContextValue {
  isAuthenticated: boolean
  isInitializing: boolean
  claims: TokenClaims | null
  login: (request: LoginRequest) => Promise<void>
  register: (request: RegisterRequest) => Promise<{ message: string }>
  logout: () => Promise<void>
  hasPermission: (permission: Permission) => boolean
  hasRole: (role: Role) => boolean
}

const AuthContext = createContext<AuthContextValue | null>(null)

function claimsFromSession(session: StoredSession | null): TokenClaims | null {
  return session ? decodeToken(session.token) : null
}

export function AuthProvider({ children }: { children: ReactNode }) {
  const [claims, setClaims] = useState<TokenClaims | null>(null)
  const [isInitializing, setIsInitializing] = useState(true)

  useEffect(() => {
    let cancelled = false

    async function hydrate() {
      const session = loadSession()
      if (!session) {
        if (!cancelled) setIsInitializing(false)
        return
      }

      const decoded = claimsFromSession(session)
      if (decoded && !isExpired(decoded)) {
        if (!cancelled) {
          setClaims(decoded)
          setIsInitializing(false)
        }
        return
      }

      // Access token expired but we still have a refresh token - try a
      // silent refresh once so a reload doesn't bounce the user to /login.
      try {
        const refreshed = await authApi.refreshToken({
          token: session.token,
          refreshToken: session.refreshToken,
        })
        saveSession(refreshed)
        if (!cancelled) setClaims(decodeToken(refreshed.token))
      } catch {
        clearSession()
        if (!cancelled) setClaims(null)
      } finally {
        if (!cancelled) setIsInitializing(false)
      }
    }

    void hydrate()
    return () => {
      cancelled = true
    }
  }, [])

  useEffect(() => {
    registerSessionExpiredHandler(() => setClaims(null))
  }, [])

  const login = useCallback(async (request: LoginRequest) => {
    const response = await authApi.login(request)
    saveSession(response)
    setClaims(decodeToken(response.token))
  }, [])

  const register = useCallback((request: RegisterRequest) => authApi.register(request), [])

  const logout = useCallback(async () => {
    const session = loadSession()
    if (session) {
      try {
        await authApi.logout({ token: session.token, refreshToken: session.refreshToken })
      } catch {
        // Best-effort revoke - proceed with local logout regardless.
      }
    }
    clearSession()
    setClaims(null)
  }, [])

  const hasPermission = useCallback(
    (permission: Permission) => claims?.permissions.includes(permission) ?? false,
    [claims],
  )

  const hasRole = useCallback((role: Role) => claims?.roles.includes(role) ?? false, [claims])

  const value = useMemo<AuthContextValue>(
    () => ({
      isAuthenticated: claims !== null,
      isInitializing,
      claims,
      login,
      register,
      logout,
      hasPermission,
      hasRole,
    }),
    [claims, isInitializing, login, register, logout, hasPermission, hasRole],
  )

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
}

export function useAuth(): AuthContextValue {
  const ctx = useContext(AuthContext)
  if (!ctx) throw new Error('useAuth must be used within an AuthProvider')
  return ctx
}
