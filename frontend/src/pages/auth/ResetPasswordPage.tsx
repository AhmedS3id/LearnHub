// Rendered at /auth/forgetPassword - this exact path (and the email/code
// query params) is hardcoded into the backend's password-reset email
// (AuthServices.SendForgetPasswordEmail: "{Origin}/auth/forgetPassword?email=...&code=...").
import { useState, type FormEvent } from 'react'
import { Link, useSearchParams } from 'react-router-dom'
import * as authApi from '../../api/auth'
import { fieldErrorFor, toApiError, type ApiError } from '../../api/errors'
import { AuthLayout } from '../../components/layout/AuthLayout'
import { Button } from '../../components/ui/Button'
import { ErrorBanner } from '../../components/ui/ErrorBanner'
import { Input } from '../../components/ui/Field'

export function ResetPasswordPage() {
  const [searchParams] = useSearchParams()
  const email = searchParams.get('email') ?? ''
  const code = searchParams.get('code') ?? ''

  const [newPassword, setNewPassword] = useState('')
  const [error, setError] = useState<ApiError | null>(null)
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [isDone, setIsDone] = useState(false)

  if (!email || !code) {
    return (
      <AuthLayout title="Invalid reset link">
        <p className="text-sm text-slate-600">
          This password reset link is missing required information. Please request a new one.
        </p>
        <Link to="/forgot-password" className="mt-6 inline-block text-sm font-medium text-indigo-600 hover:text-indigo-500">
          Request a new link
        </Link>
      </AuthLayout>
    )
  }

  async function handleSubmit(e: FormEvent) {
    e.preventDefault()
    setError(null)
    setIsSubmitting(true)
    try {
      await authApi.resetPassword({ email, code, newPassword })
      setIsDone(true)
    } catch (err) {
      setError(toApiError(err))
    } finally {
      setIsSubmitting(false)
    }
  }

  if (isDone) {
    return (
      <AuthLayout title="Password updated">
        <p className="text-sm text-slate-600">Your password has been reset. You can now log in.</p>
        <Link to="/login" className="mt-6 inline-block">
          <Button>Log in</Button>
        </Link>
      </AuthLayout>
    )
  }

  return (
    <AuthLayout title="Set a new password" subtitle={`Resetting the password for ${email}`}>
      <form className="space-y-4" onSubmit={handleSubmit}>
        <ErrorBanner error={error} />
        <Input
          label="New password"
          type="password"
          autoComplete="new-password"
          required
          value={newPassword}
          onChange={(e) => setNewPassword(e.target.value)}
          error={fieldErrorFor(error, 'NewPassword')}
          hint="At least 8 characters, with upper and lower case letters, a number, and a symbol."
        />
        <Button type="submit" isLoading={isSubmitting} className="w-full">
          Reset password
        </Button>
      </form>
    </AuthLayout>
  )
}
