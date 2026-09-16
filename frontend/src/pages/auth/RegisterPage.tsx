import { useState, type FormEvent } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { useAuth } from '../../auth/AuthContext'
import { AuthLayout } from '../../components/layout/AuthLayout'
import { Button } from '../../components/ui/Button'
import { ErrorBanner } from '../../components/ui/ErrorBanner'
import { Input } from '../../components/ui/Field'
import { fieldErrorFor, toApiError, type ApiError } from '../../api/errors'

const initialForm = {
  firstName: '',
  lastName: '',
  email: '',
  password: '',
  confirmPassword: '',
}

export function RegisterPage() {
  const { register } = useAuth()
  const navigate = useNavigate()

  const [form, setForm] = useState(initialForm)
  const [error, setError] = useState<ApiError | null>(null)
  const [isSubmitting, setIsSubmitting] = useState(false)

  function update<K extends keyof typeof form>(key: K, value: (typeof form)[K]) {
    setForm((f) => ({ ...f, [key]: value }))
  }

  async function handleSubmit(e: FormEvent) {
    e.preventDefault()
    setError(null)
    setIsSubmitting(true)
    try {
      await register(form)
      navigate('/login', {
        replace: true,
        state: { registeredEmail: form.email },
      })
    } catch (err) {
      setError(toApiError(err))
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <AuthLayout title="Create your account" subtitle="Join LearnHub to start learning.">
      <form className="space-y-4" onSubmit={handleSubmit}>
        <ErrorBanner error={error} />
        <div className="grid grid-cols-2 gap-3">
          <Input
            label="First name"
            required
            minLength={3}
            value={form.firstName}
            onChange={(e) => update('firstName', e.target.value)}
            error={fieldErrorFor(error, 'FirstName')}
          />
          <Input
            label="Last name"
            required
            minLength={3}
            value={form.lastName}
            onChange={(e) => update('lastName', e.target.value)}
            error={fieldErrorFor(error, 'LastName')}
          />
        </div>
        <Input
          label="Email"
          type="email"
          autoComplete="email"
          required
          value={form.email}
          onChange={(e) => update('email', e.target.value)}
          error={fieldErrorFor(error, 'Email')}
        />
        <Input
          label="Password"
          type="password"
          autoComplete="new-password"
          required
          value={form.password}
          onChange={(e) => update('password', e.target.value)}
          error={fieldErrorFor(error, 'Password')}
          hint="At least 8 characters, with upper and lower case letters, a number, and a symbol."
        />
        <Input
          label="Confirm password"
          type="password"
          autoComplete="new-password"
          required
          value={form.confirmPassword}
          onChange={(e) => update('confirmPassword', e.target.value)}
          error={fieldErrorFor(error, 'ConfirmPassword')}
        />
        <Button type="submit" isLoading={isSubmitting} className="w-full">
          Create account
        </Button>
      </form>
      <p className="mt-6 text-center text-sm text-slate-500">
        Already have an account?{' '}
        <Link to="/login" className="font-medium text-indigo-600 hover:text-indigo-500">
          Log in
        </Link>
      </p>
    </AuthLayout>
  )
}
