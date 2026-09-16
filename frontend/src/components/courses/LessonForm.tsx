import { useState, type FormEvent } from 'react'
import type { ApiError } from '../../api/errors'
import { fieldErrorFor } from '../../api/errors'
import { Button } from '../ui/Button'
import { ErrorBanner } from '../ui/ErrorBanner'
import { Input, Textarea } from '../ui/Field'

export function LessonForm({
  initialTitle = '',
  initialDescription = '',
  initialVideoUrl = '',
  initialDuration = 10,
  initialOrder = 1,
  isSubmitting,
  error,
  onSubmit,
  onCancel,
}: {
  initialTitle?: string
  initialDescription?: string
  initialVideoUrl?: string
  initialDuration?: number
  initialOrder?: number
  isSubmitting: boolean
  error: ApiError | null
  onSubmit: (values: {
    title: string
    description: string
    videoUrl: string
    durationInMinutes: number
    order: number
  }) => void
  onCancel: () => void
}) {
  const [title, setTitle] = useState(initialTitle)
  const [description, setDescription] = useState(initialDescription)
  const [videoUrl, setVideoUrl] = useState(initialVideoUrl)
  const [durationInMinutes, setDurationInMinutes] = useState(String(initialDuration))
  const [order, setOrder] = useState(String(initialOrder))

  function handleSubmit(e: FormEvent) {
    e.preventDefault()
    onSubmit({
      title,
      description,
      videoUrl,
      durationInMinutes: Number(durationInMinutes),
      order: Number(order),
    })
  }

  return (
    <form className="space-y-4" onSubmit={handleSubmit}>
      <ErrorBanner error={error} />
      <Input
        label="Title"
        required
        minLength={5}
        maxLength={500}
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        error={fieldErrorFor(error, 'Title')}
      />
      <Textarea
        label="Description"
        required
        minLength={10}
        maxLength={1000}
        rows={3}
        value={description}
        onChange={(e) => setDescription(e.target.value)}
        error={fieldErrorFor(error, 'Description')}
      />
      <Input
        label="Video URL"
        type="url"
        required
        maxLength={1000}
        value={videoUrl}
        onChange={(e) => setVideoUrl(e.target.value)}
        error={fieldErrorFor(error, 'VideoUrl')}
      />
      <div className="grid grid-cols-2 gap-3">
        <Input
          label="Duration (minutes)"
          type="number"
          required
          min="1"
          value={durationInMinutes}
          onChange={(e) => setDurationInMinutes(e.target.value)}
          error={fieldErrorFor(error, 'DurationInMinutes')}
        />
        <Input
          label="Order"
          type="number"
          required
          min="1"
          value={order}
          onChange={(e) => setOrder(e.target.value)}
          error={fieldErrorFor(error, 'Order')}
        />
      </div>
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
