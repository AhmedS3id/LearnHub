// Mirrors LearnHub_Api/Controllers/EnrollmentController.cs exactly.
// There is no update or delete/unenroll endpoint on the backend - do not
// invent one here.
import { apiClient } from './client'
import type { EnrollmentResponse } from './types'

export async function enrollInCourse(courseId: number): Promise<EnrollmentResponse> {
  const { data } = await apiClient.post<EnrollmentResponse>(`/Enrollment/course/${courseId}`)
  return data
}

export async function getMyEnrollments(): Promise<EnrollmentResponse[]> {
  const { data } = await apiClient.get<EnrollmentResponse[]>('/Enrollment')
  return data
}

export async function getEnrollmentById(enrollmentId: number): Promise<EnrollmentResponse> {
  const { data } = await apiClient.get<EnrollmentResponse>(`/Enrollment/${enrollmentId}`)
  return data
}
