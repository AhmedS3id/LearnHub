// Mirrors LearnHub_Api/Controllers/SectionController.cs exactly.
import { apiClient } from './client'
import type { SectionRequest, SectionResponse } from './types'

export async function getSections(courseId: number): Promise<SectionResponse[]> {
  const { data } = await apiClient.get<SectionResponse[]>(`/Section/course/${courseId}`)
  return data
}

export async function getSectionById(courseId: number, sectionId: number): Promise<SectionResponse> {
  const { data } = await apiClient.get<SectionResponse>(`/Section/course/${courseId}/section/${sectionId}`)
  return data
}

export async function createSection(courseId: number, request: SectionRequest): Promise<SectionResponse> {
  const { data } = await apiClient.post<SectionResponse>(`/Section/course/${courseId}`, request)
  return data
}

export async function updateSection(
  courseId: number,
  sectionId: number,
  request: SectionRequest,
): Promise<void> {
  await apiClient.put(`/Section/course/${courseId}/section/${sectionId}`, request)
}

export async function deleteSection(courseId: number, sectionId: number): Promise<void> {
  await apiClient.delete(`/Section/course/${courseId}/section/${sectionId}`)
}
