import { useState, type FormEvent } from 'react'
import type { ApiError } from '../../api/errors'
import { fieldErrorFor } from '../../api/errors'
import { Button } from '../ui/Button'
import { ErrorBanner } from '../ui/ErrorBanner'
import { Input } from '../ui/Field'

export function SectionForm({
  initialTitle = '',
  initialOrder = 1,
  isSubmitting,
  error,
  onSubmit,
  onCancel,
}: {
  initialTitle?: string
  initialOrder?: number
  isSubmitting: boolean
  error: ApiError | null
  onSubmit: (values: { title: string; order: number }) => void
  onCancel: () => void
}) {
  const [title, setTitle] = useState(initialTitle)
  const [order, setOrder] = useState(String(initialOrder))

  function handleSubmit(e: FormEvent) {
    e.preventDefault()
    onSubmit({ title, order: Number(order) })
  }

  return (
    <form className="space-y-4" onSubmit={handleSubmit}>
      <ErrorBanner error={error} />
      <Input
        label="Title"
        required
        minLength={1}
        maxLength={200}
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        error={fieldErrorFor(error, 'Title')}
      />
      <Input
        label="Order"
        type="number"
        required
        min="1"
        value={order}
        onChange={(e) => setOrder(e.target.value)}
        error={fieldErrorFor(error, 'Order')}
        hint="Sections are shown in ascending order; each order value must be unique within the course."
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
