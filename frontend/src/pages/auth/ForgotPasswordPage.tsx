import { useState, type FormEvent } from 'react'
import { Link } from 'react-router-dom'
import * as authApi from '../../api/auth'
import { toApiError, type ApiError } from '../../api/errors'
import { AuthLayout } from '../../components/layout/AuthLayout'
import { Button } from '../../components/ui/Button'
import { ErrorBanner } from '../../components/ui/ErrorBanner'
import { Input } from '../../components/ui/Field'

export function ForgotPasswordPage() {
  const [email, setEmail] = useState('')
  const [error, setError] = useState<ApiError | null>(null)
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [isSent, setIsSent] = useState(false)

  async function handleSubmit(e: FormEvent) {
    e.preventDefault()
    setError(null)
    setIsSubmitting(true)
    try {
      await authApi.forgetPassword({ email })
      setIsSent(true)
    } catch (err) {
      setError(toApiError(err))
    } finally {
      setIsSubmitting(false)
    }
  }

  if (isSent) {
    return (
      <AuthLayout title="Check your email">
        <p className="text-sm text-slate-600">
          If an account exists for <span className="font-medium">{email}</span>, we've sent a link to reset your
          password.
        </p>
        <Link to="/login" className="mt-6 inline-block text-sm font-medium text-indigo-600 hover:text-indigo-500">
          Back to log in
        </Link>
      </AuthLayout>
    )
  }

  return (
    <AuthLayout title="Forgot your password?" subtitle="Enter your email and we'll send you a reset link.">
      <form className="space-y-4" onSubmit={handleSubmit}>
        <ErrorBanner error={error} />
        <Input
          label="Email"
          type="email"
          autoComplete="email"
          required
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <Button type="submit" isLoading={isSubmitting} className="w-full">
          Send reset link
        </Button>
      </form>
      <Link to="/login" className="mt-6 inline-block text-sm font-medium text-indigo-600 hover:text-indigo-500">
        Back to log in
      </Link>
    </AuthLayout>
  )
}
