// Mirrors LearnHub_Api/Controllers/CourseController.cs exactly.
//
// Note: GetAll and GetByCategory return a plain CourseResponse[], not a
// PaginatedList - CourseServices.GetAllAsync/GetByCategoryAsync never apply
// Skip/Take, so there is no server-side pagination or total count for
// courses despite the endpoint accepting pageNumber/pageSize query params.
import { apiClient } from './client'
import { cleanRequestFilter } from './queryParams'
import type { CourseRequest, CourseResponse, RequestFilter } from './types'

export async function getCourses(filter: RequestFilter): Promise<CourseResponse[]> {
  const { data } = await apiClient.get<CourseResponse[]>('/Course', { params: cleanRequestFilter(filter) })
  return data
}

export async function getCoursesByCategory(categoryId: number): Promise<CourseResponse[]> {
  const { data } = await apiClient.get<CourseResponse[]>(`/Course/category/${categoryId}`)
  return data
}

export async function getCourseById(id: number): Promise<CourseResponse> {
  const { data } = await apiClient.get<CourseResponse>(`/Course/${id}`)
  return data
}

export async function createCourse(request: CourseRequest): Promise<CourseResponse> {
  const { data } = await apiClient.post<CourseResponse>('/Course', request)
  return data
}

export async function updateCourse(courseId: number, request: CourseRequest): Promise<void> {
  await apiClient.put(`/Course/${courseId}`, request)
}

export async function deleteCourse(courseId: number): Promise<void> {
  await apiClient.delete(`/Course/${courseId}`)
}
