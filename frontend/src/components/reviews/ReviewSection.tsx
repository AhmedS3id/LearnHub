import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { useState } from 'react'
import * as reviewsApi from '../../api/reviews'
import { toApiError, type ApiError } from '../../api/errors'
import type { ReviewResponse } from '../../api/types'
import { useAuth } from '../../auth/AuthContext'
import { Permissions } from '../../auth/permissions'
import { useToast } from '../ui/Toast'
import { Button } from '../ui/Button'
import { EmptyState } from '../ui/EmptyState'
import { ErrorBanner } from '../ui/ErrorBanner'
import { Pagination } from '../ui/Pagination'
import { PageSpinner } from '../ui/Spinner'
import { ReviewForm } from './ReviewForm'

// ReviewResponse does not include the reviewing student's id (only their
// display name - Contracts/Review/ReviewResponse.cs), so "is this my
// review" can only be approximated by matching the current user's full
// name. This is a best-effort UI affordance only: the backend independently
// enforces real ownership on every PUT/DELETE /Review/{id} call regardless
// of what the UI shows.
function useIsMyReview() {
  const { claims } = useAuth()
  const myName = claims ? `${claims.firstName} ${claims.lastName}` : null
  return (review: ReviewResponse) => myName !== null && review.studentName === myName
}

export function ReviewSection({ courseId }: { courseId: number }) {
  const { hasPermission } = useAuth()
  const { showToast } = useToast()
  const queryClient = useQueryClient()
  const isMyReview = useIsMyReview()

  const [pageNumber, setPageNumber] = useState(1)
  const [isAdding, setIsAdding] = useState(false)
  const [editingReviewId, setEditingReviewId] = useState<number | null>(null)
  const [listError, setListError] = useState<ApiError | null>(null)

  const reviewsQuery = useQuery({
    queryKey: ['reviews', courseId, pageNumber],
    queryFn: () => reviewsApi.getReviews(courseId, { pageNumber, pageSize: 10 }),
  })

  const invalidate = () => queryClient.invalidateQueries({ queryKey: ['reviews', courseId] })

  const createMutation = useMutation({
    mutationFn: (values: { rating: number; comment: string }) => reviewsApi.createReview(courseId, values),
    onSuccess: () => {
      setIsAdding(false)
      showToast('Review submitted.')
      invalidate()
    },
    onError: (err) => setListError(toApiError(err)),
  })

  const updateMutation = useMutation({
    mutationFn: (values: { id: number; rating: number; comment: string }) =>
      reviewsApi.updateReview(values.id, { rating: values.rating, comment: values.comment }),
    onSuccess: () => {
      setEditingReviewId(null)
      showToast('Review updated.')
      invalidate()
    },
    onError: (err) => setListError(toApiError(err)),
  })

  const deleteMutation = useMutation({
    mutationFn: (reviewId: number) => reviewsApi.deleteReview(reviewId),
    onSuccess: () => {
      showToast('Review deleted.')
      invalidate()
    },
    onError: (err) => setListError(toApiError(err)),
  })

  const reviews = reviewsQuery.data?.items ?? []
  const alreadyReviewed = createMutation.error && toApiError(createMutation.error).code === 'Review.AlreadyReviewed'

  return (
    <section className="space-y-4">
      <div className="flex items-center justify-between">
        <h2 className="text-lg font-semibold text-slate-900">Reviews</h2>
        {hasPermission(Permissions.AddReviews) && !isAdding && (
          <Button variant="secondary" size="sm" onClick={() => setIsAdding(true)}>
            Write a review
          </Button>
        )}
      </div>

      <ErrorBanner error={listError} />

      {isAdding && (
        <div className="rounded-lg border border-slate-200 p-4">
          {alreadyReviewed && (
            <p className="mb-3 text-sm text-amber-700">
              You've already reviewed this course. Find your review below to edit it.
            </p>
          )}
          <ReviewForm
            submitLabel="Submit review"
            isSubmitting={createMutation.isPending}
            onCancel={() => setIsAdding(false)}
            onSubmit={(values) => createMutation.mutate(values)}
          />
        </div>
      )}

      {reviewsQuery.isLoading ? (
        <PageSpinner />
      ) : reviewsQuery.isError ? (
        <ErrorBanner error={toApiError(reviewsQuery.error)} />
      ) : reviews.length === 0 ? (
        <EmptyState title="No reviews yet" description="Be the first to share your thoughts on this course." />
      ) : (
        <ul className="space-y-3">
          {reviews.map((review) => (
            <li key={review.id} className="rounded-lg border border-slate-200 p-4">
              {editingReviewId === review.id ? (
                <ReviewForm
                  initialRating={review.rating}
                  initialComment={review.comment}
                  submitLabel="Save changes"
                  isSubmitting={updateMutation.isPending}
                  onCancel={() => setEditingReviewId(null)}
                  onSubmit={(values) => updateMutation.mutate({ id: review.id, ...values })}
                />
              ) : (
                <>
                  <div className="flex items-center justify-between">
                    <div>
                      <p className="text-sm font-semibold text-slate-900">{review.studentName}</p>
                      <p className="text-amber-500" aria-label={`${review.rating} out of 5 stars`}>
                        {'★'.repeat(review.rating)}
                        <span className="text-slate-300">{'★'.repeat(5 - review.rating)}</span>
                      </p>
                    </div>
                    {isMyReview(review) && (
                      <div className="flex gap-2">
                        <Button variant="ghost" size="sm" onClick={() => setEditingReviewId(review.id)}>
                          Edit
                        </Button>
                        <Button
                          variant="ghost"
                          size="sm"
                          isLoading={deleteMutation.isPending && deleteMutation.variables === review.id}
                          onClick={() => {
                            if (confirm('Delete your review?')) deleteMutation.mutate(review.id)
                          }}
                        >
                          Delete
                        </Button>
                      </div>
                    )}
                  </div>
                  <p className="mt-2 text-sm text-slate-600">{review.comment}</p>
                </>
              )}
            </li>
          ))}
        </ul>
      )}

      {reviewsQuery.data && (
        <Pagination
          pageNumber={reviewsQuery.data.pageNumber}
          totalPages={reviewsQuery.data.totalPages}
          hasNextPage={reviewsQuery.data.hasNextPage}
          hasPreviousPages={reviewsQuery.data.hasPreviousPages}
          onPageChange={setPageNumber}
        />
      )}
    </section>
  )
}
