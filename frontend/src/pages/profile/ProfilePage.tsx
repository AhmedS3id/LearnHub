import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { useEffect, useState, type FormEvent } from 'react'
import * as usersApi from '../../api/users'
import { fieldErrorFor, toApiError, type ApiError } from '../../api/errors'
import { Permissions } from '../../auth/permissions'
import { useAuth } from '../../auth/AuthContext'
import { Button } from '../../components/ui/Button'
import { Card } from '../../components/ui/Card'
import { ErrorBanner } from '../../components/ui/ErrorBanner'
import { Input } from '../../components/ui/Field'
import { PageSpinner } from '../../components/ui/Spinner'
import { useToast } from '../../components/ui/Toast'

export function ProfilePage() {
  const { hasPermission } = useAuth()
  const { showToast } = useToast()
  const queryClient = useQueryClient()

  const profileQuery = useQuery({ queryKey: ['my-profile'], queryFn: usersApi.getMyProfile })

  const [firstName, setFirstName] = useState('')
  const [lastName, setLastName] = useState('')
  const [profileError, setProfileError] = useState<ApiError | null>(null)

  useEffect(() => {
    if (profileQuery.data) {
      setFirstName(profileQuery.data.firstName)
      setLastName(profileQuery.data.lastName)
    }
  }, [profileQuery.data])

  const updateProfileMutation = useMutation({
    mutationFn: () => usersApi.updateMyProfile({ firstName, lastName }),
    onSuccess: () => {
      showToast('Profile updated.')
      queryClient.invalidateQueries({ queryKey: ['my-profile'] })
    },
    onError: (err) => setProfileError(toApiError(err)),
  })

  const [currentPassword, setCurrentPassword] = useState('')
  const [newPassword, setNewPassword] = useState('')
  const [passwordError, setPasswordError] = useState<ApiError | null>(null)

  const changePasswordMutation = useMutation({
    mutationFn: () => usersApi.changeMyPassword({ currentPassword, newPassword }),
    onSuccess: () => {
      showToast('Password changed.')
      setCurrentPassword('')
      setNewPassword('')
    },
    onError: (err) => setPasswordError(toApiError(err)),
  })

  function handleProfileSubmit(e: FormEvent) {
    e.preventDefault()
    setProfileError(null)
    updateProfileMutation.mutate()
  }

  function handlePasswordSubmit(e: FormEvent) {
    e.preventDefault()
    setPasswordError(null)
    changePasswordMutation.mutate()
  }

  if (profileQuery.isLoading) return <PageSpinner />
  if (profileQuery.isError) return <ErrorBanner error={toApiError(profileQuery.error)} />

  return (
    <div className="mx-auto max-w-xl space-y-6">
      <h1 className="text-2xl font-bold text-slate-900">Your profile</h1>

      <Card>
        <h2 className="text-base font-semibold text-slate-900">Account details</h2>
        <dl className="mt-3 space-y-1 text-sm">
          <div className="flex justify-between">
            <dt className="text-slate-500">Email</dt>
            <dd className="text-slate-900">{profileQuery.data?.email}</dd>
          </div>
          <div className="flex justify-between">
            <dt className="text-slate-500">Username</dt>
            <dd className="text-slate-900">{profileQuery.data?.userName}</dd>
          </div>
        </dl>
      </Card>

      <Card>
        <h2 className="text-base font-semibold text-slate-900">Edit name</h2>
        <form className="mt-4 space-y-4" onSubmit={handleProfileSubmit}>
          <ErrorBanner error={profileError} />
          <div className="grid grid-cols-2 gap-3">
            <Input
              label="First name"
              required
              minLength={3}
              value={firstName}
              onChange={(e) => setFirstName(e.target.value)}
              error={fieldErrorFor(profileError, 'FirstName')}
              disabled={!hasPermission(Permissions.UpdateProfile)}
            />
            <Input
              label="Last name"
              required
              minLength={3}
              value={lastName}
              onChange={(e) => setLastName(e.target.value)}
              error={fieldErrorFor(profileError, 'LastName')}
              disabled={!hasPermission(Permissions.UpdateProfile)}
            />
          </div>
          <Button type="submit" isLoading={updateProfileMutation.isPending} disabled={!hasPermission(Permissions.UpdateProfile)}>
            Save changes
          </Button>
        </form>
      </Card>

      <Card>
        <h2 className="text-base font-semibold text-slate-900">Change password</h2>
        <form className="mt-4 space-y-4" onSubmit={handlePasswordSubmit}>
          <ErrorBanner error={passwordError} />
          <Input
            label="Current password"
            type="password"
            autoComplete="current-password"
            required
            value={currentPassword}
            onChange={(e) => setCurrentPassword(e.target.value)}
            error={fieldErrorFor(passwordError, 'CurrentPassword')}
          />
          <Input
            label="New password"
            type="password"
            autoComplete="new-password"
            required
            value={newPassword}
            onChange={(e) => setNewPassword(e.target.value)}
            error={fieldErrorFor(passwordError, 'NewPassword')}
            hint="At least 8 characters, with upper and lower case letters, a number, and a symbol."
          />
          <Button type="submit" isLoading={changePasswordMutation.isPending}>
            Change password
          </Button>
        </form>
      </Card>
    </div>
  )
}
