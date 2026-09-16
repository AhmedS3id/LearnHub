// Mirrors LearnHub_Api/Controllers/LessonController.cs exactly.
import { apiClient } from './client'
import type { LessonRequest, LessonResponse, SectionWithLessonsResponse } from './types'

export async function getCourseContent(courseId: number): Promise<SectionWithLessonsResponse[]> {
  const { data } = await apiClient.get<SectionWithLessonsResponse[]>(`/Lesson/course/${courseId}`)
  return data
}

export async function getLessonById(sectionId: number, lessonId: number): Promise<LessonResponse> {
  const { data } = await apiClient.get<LessonResponse>(`/Lesson/section/${sectionId}/lesson/${lessonId}`)
  return data
}

export async function createLesson(sectionId: number, request: LessonRequest): Promise<LessonResponse> {
  const { data } = await apiClient.post<LessonResponse>(`/Lesson/section/${sectionId}`, request)
  return data
}

export async function updateLesson(
  sectionId: number,
  lessonId: number,
  request: LessonRequest,
): Promise<void> {
  await apiClient.put(`/Lesson/section/${sectionId}/lesson/${lessonId}`, request)
}

export async function deleteLesson(sectionId: number, lessonId: number): Promise<void> {
  await apiClient.delete(`/Lesson/section/${sectionId}/lesson/${lessonId}`)
}
