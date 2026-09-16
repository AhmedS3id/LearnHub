import type { ReactNode } from 'react'
import { Navigate, useLocation } from 'react-router-dom'
import { PageSpinner } from '../components/ui/Spinner'
import { useAuth } from './AuthContext'
import type { Permission, Role } from './permissions'

export function ProtectedRoute({
  children,
  permission,
  role,
}: {
  children: ReactNode
  /** If set, the user must have this permission (from their JWT) to view the route. */
  permission?: Permission
  /** If set, the user must have this exact role (matches backend's [Authorize(Roles=...)] checks). */
  role?: Role
}) {
  const { isAuthenticated, isInitializing, hasPermission, hasRole } = useAuth()
  const location = useLocation()

  if (isInitializing) return <PageSpinner />

  if (!isAuthenticated) {
    return <Navigate to="/login" replace state={{ from: location }} />
  }

  if (permission && !hasPermission(permission)) {
    return <Navigate to="/403" replace />
  }

  if (role && !hasRole(role)) {
    return <Navigate to="/403" replace />
  }

  return <>{children}</>
}
