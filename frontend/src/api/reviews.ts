// Mirrors LearnHub_Api/Controllers/ReviewController.cs exactly.
import { apiClient } from './client'
import { cleanRequestFilter } from './queryParams'
import type { PaginatedList, RequestFilter, ReviewRequest, ReviewResponse } from './types'

export async function getReviews(courseId: number, filter: RequestFilter): Promise<PaginatedList<ReviewResponse>> {
  const { data } = await apiClient.get<PaginatedList<ReviewResponse>>(`/Review/course/${courseId}`, {
    params: cleanRequestFilter(filter),
  })
  return data
}

export async function createReview(courseId: number, request: ReviewRequest): Promise<ReviewResponse> {
  const { data } = await apiClient.post<ReviewResponse>(`/Review/course/${courseId}`, request)
  return data
}

export async function updateReview(reviewId: number, request: ReviewRequest): Promise<void> {
  await apiClient.put(`/Review/${reviewId}`, request)
}

export async function deleteReview(reviewId: number): Promise<void> {
  await apiClient.delete(`/Review/${reviewId}`)
}
