// Mirrors LearnHub_Api/Controllers/CategoryController.cs exactly.
import { apiClient } from './client'
import type { CategoryRequest, CategoryResponse } from './types'

export async function getCategories(): Promise<CategoryResponse[]> {
  const { data } = await apiClient.get<CategoryResponse[]>('/Category')
  return data
}

export async function getCategoryById(id: number): Promise<CategoryResponse> {
  const { data } = await apiClient.get<CategoryResponse>(`/Category/${id}`)
  return data
}

export async function createCategory(request: CategoryRequest): Promise<CategoryResponse> {
  const { data } = await apiClient.post<CategoryResponse>('/Category', request)
  return data
}

export async function updateCategory(id: number, request: CategoryRequest): Promise<void> {
  await apiClient.put(`/Category/${id}`, request)
}

export async function deleteCategory(id: number): Promise<void> {
  await apiClient.delete(`/Category/${id}`)
}
