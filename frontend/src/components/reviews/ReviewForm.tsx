import { useState, type FormEvent } from 'react'
import { Button } from '../ui/Button'
import { Textarea } from '../ui/Field'

export function ReviewForm({
  initialRating = 5,
  initialComment = '',
  submitLabel,
  isSubmitting,
  onSubmit,
  onCancel,
}: {
  initialRating?: number
  initialComment?: string
  submitLabel: string
  isSubmitting: boolean
  onSubmit: (values: { rating: number; comment: string }) => void
  onCancel: () => void
}) {
  const [rating, setRating] = useState(initialRating)
  const [comment, setComment] = useState(initialComment)

  function handleSubmit(e: FormEvent) {
    e.preventDefault()
    onSubmit({ rating, comment })
  }

  return (
    <form className="space-y-3" onSubmit={handleSubmit}>
      <div>
        <label className="block text-sm font-medium text-slate-700">Rating</label>
        <div className="mt-1 flex gap-1" role="radiogroup" aria-label="Rating">
          {[1, 2, 3, 4, 5].map((value) => (
            <button
              key={value}
              type="button"
              role="radio"
              aria-checked={rating === value}
              onClick={() => setRating(value)}
              className={`text-2xl ${value <= rating ? 'text-amber-500' : 'text-slate-300'}`}
            >
              ★
            </button>
          ))}
        </div>
      </div>
      <Textarea
        label="Comment"
        required
        minLength={1}
        maxLength={1000}
        rows={3}
        value={comment}
        onChange={(e) => setComment(e.target.value)}
      />
      <div className="flex gap-2">
        <Button type="submit" size="sm" isLoading={isSubmitting}>
          {submitLabel}
        </Button>
        <Button type="button" variant="secondary" size="sm" onClick={onCancel}>
          Cancel
        </Button>
      </div>
    </form>
  )
}
