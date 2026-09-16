// UserServices.GetAllAsync excludes users in the Member role entirely
// (LearnHub_Api/Services/UserServices.cs), so this list only ever shows
// Instructors and Admins - that's existing backend behavior, not a bug this
// page works around.
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { useState, type FormEvent } from 'react'
import * as usersApi from '../../api/users'
import { fieldErrorFor, toApiError, type ApiError } from '../../api/errors'
import type { UserResponse } from '../../api/types'
import { Roles } from '../../auth/permissions'
import { Badge } from '../../components/ui/Badge'
import { Button } from '../../components/ui/Button'
import { Card } from '../../components/ui/Card'
import { EmptyState } from '../../components/ui/EmptyState'
import { ErrorBanner } from '../../components/ui/ErrorBanner'
import { Input, Select } from '../../components/ui/Field'
import { Modal } from '../../components/ui/Modal'
import { Pagination } from '../../components/ui/Pagination'
import { PageSpinner } from '../../components/ui/Spinner'
import { useToast } from '../../components/ui/Toast'

function EditUserForm({
  user,
  isSubmitting,
  error,
  onSubmit,
  onCancel,
}: {
  user: UserResponse
  isSubmitting: boolean
  error: ApiError | null
  onSubmit: (values: { firstName: string; lastName: string; email: string }) => void
  onCancel: () => void
}) {
  const [firstName, setFirstName] = useState(user.firstName)
  const [lastName, setLastName] = useState(user.lastName)
  const [email, setEmail] = useState(user.email)

  function handleSubmit(e: FormEvent) {
    e.preventDefault()
    onSubmit({ firstName, lastName, email })
  }

  return (
    <form className="space-y-4" onSubmit={handleSubmit}>
      <ErrorBanner error={error} />
      <Input
        label="First name"
        required
        minLength={3}
        value={firstName}
        onChange={(e) => setFirstName(e.target.value)}
        error={fieldErrorFor(error, 'FirstName')}
      />
      <Input
        label="Last name"
        required
        minLength={3}
        value={lastName}
        onChange={(e) => setLastName(e.target.value)}
        error={fieldErrorFor(error, 'LastName')}
      />
      <Input
        label="Email"
        type="email"
        required
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        error={fieldErrorFor(error, 'Email')}
      />
      <div className="flex gap-2">
        <Button type="submit" isLoading={isSubmitting}>
          Save
        </Button>
        <Button type="button" variant="secondary" onClick={onCancel}>
          Cancel
        </Button>
      </div>
    </form>
  )
}

export function AdminUsersPage() {
  const { showToast } = useToast()
  const queryClient = useQueryClient()

  const [searchInput, setSearchInput] = useState('')
  const [search, setSearch] = useState('')
  const [pageNumber, setPageNumber] = useState(1)
  const [editingUser, setEditingUser] = useState<UserResponse | null>(null)
  const [modalError, setModalError] = useState<ApiError | null>(null)
  const [rowError, setRowError] = useState<ApiError | null>(null)

  const usersQuery = useQuery({
    queryKey: ['users', pageNumber, search],
    queryFn: () => usersApi.getUsers({ pageNumber, pageSize: 10, searchValue: search }),
  })

  const invalidate = () => queryClient.invalidateQueries({ queryKey: ['users'] })

  const roleMutation = useMutation({
    mutationFn: (values: { userId: string; role: string }) => usersApi.changeUserRole(values.userId, { role: values.role }),
    onSuccess: () => {
      showToast('Role updated.')
      invalidate()
    },
    onError: (err) => setRowError(toApiError(err)),
  })

  const toggleStatusMutation = useMutation({
    mutationFn: (userId: string) => usersApi.toggleUserStatus(userId),
    onSuccess: () => {
      showToast('Status updated.')
      invalidate()
    },
    onError: (err) => setRowError(toApiError(err)),
  })

  const unlockMutation = useMutation({
    mutationFn: (userId: string) => usersApi.unlockUser(userId),
    onSuccess: () => {
      showToast('Account unlocked.')
      invalidate()
    },
    onError: (err) => setRowError(toApiError(err)),
  })

  const updateUserMutation = useMutation({
    mutationFn: (values: { userId: string; firstName: string; lastName: string; email: string }) =>
      usersApi.updateUser(values.userId, values),
    onSuccess: () => {
      showToast('User updated.')
      invalidate()
      setEditingUser(null)
      setModalError(null)
    },
    onError: (err) => setModalError(toApiError(err)),
  })

  function handleSearchSubmit(e: FormEvent) {
    e.preventDefault()
    setPageNumber(1)
    setSearch(searchInput)
  }

  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-slate-900">User management</h1>
      <p className="text-sm text-slate-500">
        Shows Instructor and Admin accounts. Member accounts aren't included in this list.
      </p>

      <form className="flex max-w-sm gap-2" onSubmit={handleSearchSubmit}>
        <input
          type="search"
          placeholder="Search by name or email..."
          value={searchInput}
          onChange={(e) => setSearchInput(e.target.value)}
          className="w-full rounded-md border-0 px-3 py-2 text-sm text-slate-900 shadow-sm ring-1 ring-inset ring-slate-300 placeholder:text-slate-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600"
        />
        <Button type="submit" variant="secondary">
          Search
        </Button>
      </form>

      <ErrorBanner error={rowError} />

      {usersQuery.isLoading ? (
        <PageSpinner />
      ) : usersQuery.isError ? (
        <ErrorBanner error={toApiError(usersQuery.error)} />
      ) : usersQuery.data?.items.length === 0 ? (
        <EmptyState title="No users found" />
      ) : (
        <>
          <div className="space-y-3">
            {usersQuery.data?.items.map((user) => (
              <Card key={user.id} className="flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
                <div>
                  <div className="flex items-center gap-2">
                    <p className="font-medium text-slate-900">
                      {user.firstName} {user.lastName}
                    </p>
                    {user.isDisabled && <Badge tone="red">Disabled</Badge>}
                  </div>
                  <p className="text-sm text-slate-500">{user.email}</p>
                </div>
                <div className="flex flex-wrap items-center gap-2">
                  <Select
                    label=""
                    value={user.roles[0] ?? ''}
                    onChange={(e) => roleMutation.mutate({ userId: user.id, role: e.target.value })}
                    disabled={roleMutation.isPending}
                    className="w-36"
                  >
                    {Object.values(Roles).map((role) => (
                      <option key={role} value={role}>
                        {role}
                      </option>
                    ))}
                  </Select>
                  <Button variant="secondary" size="sm" onClick={() => setEditingUser(user)}>
                    Edit
                  </Button>
                  <Button
                    variant="secondary"
                    size="sm"
                    isLoading={toggleStatusMutation.isPending && toggleStatusMutation.variables === user.id}
                    onClick={() => toggleStatusMutation.mutate(user.id)}
                  >
                    {user.isDisabled ? 'Enable' : 'Disable'}
                  </Button>
                  <Button
                    variant="secondary"
                    size="sm"
                    isLoading={unlockMutation.isPending && unlockMutation.variables === user.id}
                    onClick={() => unlockMutation.mutate(user.id)}
                  >
                    Unlock
                  </Button>
                </div>
              </Card>
            ))}
          </div>
          {usersQuery.data && (
            <Pagination
              pageNumber={usersQuery.data.pageNumber}
              totalPages={usersQuery.data.totalPages}
              hasNextPage={usersQuery.data.hasNextPage}
              hasPreviousPages={usersQuery.data.hasPreviousPages}
              onPageChange={setPageNumber}
            />
          )}
        </>
      )}

      {editingUser && (
        <Modal title={`Edit ${editingUser.firstName} ${editingUser.lastName}`} onClose={() => setEditingUser(null)}>
          <EditUserForm
            user={editingUser}
            isSubmitting={updateUserMutation.isPending}
            error={modalError}
            onCancel={() => setEditingUser(null)}
            onSubmit={(values) => updateUserMutation.mutate({ userId: editingUser.id, ...values })}
          />
        </Modal>
      )}
    </div>
  )
}
