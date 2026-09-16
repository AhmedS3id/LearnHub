// Rendered at /auth/emailConfirmation - this exact path (and the userId/code
// query params) is hardcoded into the backend's confirmation email
// (AuthServices.SendConfirmationEmail: "{Origin}/auth/emailConfirmation?userId=...&code=...").
import { useEffect, useState } from 'react'
import { Link, useSearchParams } from 'react-router-dom'
import * as authApi from '../../api/auth'
import { toApiError, type ApiError } from '../../api/errors'
import { AuthLayout } from '../../components/layout/AuthLayout'
import { Button } from '../../components/ui/Button'
import { ErrorBanner } from '../../components/ui/ErrorBanner'
import { PageSpinner } from '../../components/ui/Spinner'

type Status = 'confirming' | 'success' | 'error'

export function ConfirmEmailPage() {
  const [searchParams] = useSearchParams()
  const userId = searchParams.get('userId')
  const code = searchParams.get('code')

  const [status, setStatus] = useState<Status>('confirming')
  const [error, setError] = useState<ApiError | null>(null)

  useEffect(() => {
    if (!userId || !code) {
      setStatus('error')
      setError({ status: 400, message: 'This confirmation link is missing required information.' })
      return
    }

    let cancelled = false
    authApi
      .confirmEmail({ userId, code })
      .then(() => {
        if (!cancelled) setStatus('success')
      })
      .catch((err) => {
        if (!cancelled) {
          setError(toApiError(err))
          setStatus('error')
        }
      })
    return () => {
      cancelled = true
    }
  }, [userId, code])

  if (status === 'confirming') {
    return (
      <AuthLayout title="Confirming your email">
        <PageSpinner />
      </AuthLayout>
    )
  }

  if (status === 'success') {
    return (
      <AuthLayout title="Email confirmed">
        <p className="text-sm text-slate-600">Your email address has been confirmed. You can now log in.</p>
        <Link to="/login" className="mt-6 inline-block">
          <Button>Log in</Button>
        </Link>
      </AuthLayout>
    )
  }

  return (
    <AuthLayout title="Couldn't confirm your email">
      <ErrorBanner error={error} />
      <Link
        to="/resend-confirmation"
        className="mt-6 inline-block text-sm font-medium text-indigo-600 hover:text-indigo-500"
      >
        Resend confirmation email
      </Link>
    </AuthLayout>
  )
}
