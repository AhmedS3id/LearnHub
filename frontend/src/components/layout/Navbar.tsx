import { useState } from 'react'
import { NavLink, useNavigate } from 'react-router-dom'
import { useAuth } from '../../auth/AuthContext'
import { Permissions, Roles } from '../../auth/permissions'
import { Button } from '../ui/Button'

const linkClass = ({ isActive }: { isActive: boolean }) =>
  `rounded-md px-3 py-2 text-sm font-medium ${
    isActive ? 'bg-indigo-50 text-indigo-700' : 'text-slate-600 hover:bg-slate-100 hover:text-slate-900'
  }`

export function Navbar() {
  const { isAuthenticated, claims, hasPermission, hasRole, logout } = useAuth()
  const navigate = useNavigate()
  const [mobileOpen, setMobileOpen] = useState(false)

  const canManageCourses = hasPermission(Permissions.AddCourses)
  const isAdmin = hasRole(Roles.Admin)

  async function handleLogout() {
    await logout()
    navigate('/login')
  }

  return (
    <header className="border-b border-slate-200 bg-white">
      <nav className="mx-auto flex max-w-6xl items-center justify-between px-4 py-3 sm:px-6">
        <div className="flex items-center gap-6">
          <NavLink to="/" className="text-lg font-bold text-indigo-600">
            LearnHub
          </NavLink>
          <div className="hidden items-center gap-1 md:flex">
            <NavLink to="/" end className={linkClass}>
              Courses
            </NavLink>
            {isAuthenticated && (
              <NavLink to="/my-enrollments" className={linkClass}>
                My Enrollments
              </NavLink>
            )}
            {canManageCourses && (
              <NavLink to="/instructor/courses" className={linkClass}>
                Manage Courses
              </NavLink>
            )}
            {isAdmin && (
              <>
                <NavLink to="/admin/categories" className={linkClass}>
                  Categories
                </NavLink>
                <NavLink to="/admin/users" className={linkClass}>
                  Users
                </NavLink>
              </>
            )}
          </div>
        </div>

        <div className="hidden items-center gap-3 md:flex">
          {isAuthenticated ? (
            <>
              <NavLink to="/profile" className="text-sm text-slate-600 hover:text-slate-900">
                {claims?.firstName ?? 'Profile'}
              </NavLink>
              <Button variant="secondary" size="sm" onClick={handleLogout}>
                Log out
              </Button>
            </>
          ) : (
            <>
              <NavLink to="/login" className="text-sm font-medium text-slate-600 hover:text-slate-900">
                Log in
              </NavLink>
              <Button size="sm" onClick={() => navigate('/register')}>
                Sign up
              </Button>
            </>
          )}
        </div>

        <button
          className="rounded-md p-2 text-slate-600 hover:bg-slate-100 md:hidden"
          onClick={() => setMobileOpen((v) => !v)}
          aria-label="Toggle menu"
          aria-expanded={mobileOpen}
        >
          <svg className="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h16" />
          </svg>
        </button>
      </nav>

      {mobileOpen && (
        <div className="space-y-1 border-t border-slate-200 px-4 pb-3 pt-2 md:hidden">
          <NavLink to="/" end className={linkClass} onClick={() => setMobileOpen(false)}>
            Courses
          </NavLink>
          {isAuthenticated && (
            <NavLink to="/my-enrollments" className={linkClass} onClick={() => setMobileOpen(false)}>
              My Enrollments
            </NavLink>
          )}
          {canManageCourses && (
            <NavLink to="/instructor/courses" className={linkClass} onClick={() => setMobileOpen(false)}>
              Manage Courses
            </NavLink>
          )}
          {isAdmin && (
            <>
              <NavLink to="/admin/categories" className={linkClass} onClick={() => setMobileOpen(false)}>
                Categories
              </NavLink>
              <NavLink to="/admin/users" className={linkClass} onClick={() => setMobileOpen(false)}>
                Users
              </NavLink>
            </>
          )}
          <div className="border-t border-slate-200 pt-2">
            {isAuthenticated ? (
              <>
                <NavLink to="/profile" className={linkClass} onClick={() => setMobileOpen(false)}>
                  Profile ({claims?.firstName})
                </NavLink>
                <button
                  onClick={() => {
                    setMobileOpen(false)
                    void handleLogout()
                  }}
                  className="w-full rounded-md px-3 py-2 text-left text-sm font-medium text-slate-600 hover:bg-slate-100"
                >
                  Log out
                </button>
              </>
            ) : (
              <>
                <NavLink to="/login" className={linkClass} onClick={() => setMobileOpen(false)}>
                  Log in
                </NavLink>
                <NavLink to="/register" className={linkClass} onClick={() => setMobileOpen(false)}>
                  Sign up
                </NavLink>
              </>
            )}
          </div>
        </div>
      )}
    </header>
  )
}
