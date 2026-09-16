import { useState, type FormEvent } from 'react'
import type { ApiError } from '../../api/errors'
import { fieldErrorFor } from '../../api/errors'
import { Button } from '../ui/Button'
import { ErrorBanner } from '../ui/ErrorBanner'
import { Input, Textarea } from '../ui/Field'

export function CategoryForm({
  initialName = '',
  initialDescription = '',
  isSubmitting,
  error,
  onSubmit,
  onCancel,
}: {
  initialName?: string
  initialDescription?: string
  isSubmitting: boolean
  error: ApiError | null
  onSubmit: (values: { name: string; description: string }) => void
  onCancel: () => void
}) {
  const [name, setName] = useState(initialName)
  const [description, setDescription] = useState(initialDescription)

  function handleSubmit(e: FormEvent) {
    e.preventDefault()
    onSubmit({ name, description })
  }

  return (
    <form className="space-y-4" onSubmit={handleSubmit}>
      <ErrorBanner error={error} />
      <Input
        label="Name"
        required
        value={name}
        onChange={(e) => setName(e.target.value)}
        error={fieldErrorFor(error, 'Name')}
      />
      <Textarea
        label="Description"
        required
        minLength={5}
        maxLength={1000}
        rows={3}
        value={description}
        onChange={(e) => setDescription(e.target.value)}
        error={fieldErrorFor(error, 'Description')}
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
