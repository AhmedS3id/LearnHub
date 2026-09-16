// Mirrors LearnHub_Api/Controllers/UserController.cs ("/me") and
// LearnHub_Api/Controllers/UserManagementController.cs ("/users", Admin-only).
import { apiClient } from './client'
import { cleanRequestFilter } from './queryParams'
import type {
  ChangePasswordRequest,
  ChangeRoleRequest,
  PaginatedList,
  RequestFilter,
  UpdateProfileRequest,
  UpdateUserRequest,
  UserResponse,
  UsersProfileResponse,
} from './types'

// ---- /me ----

export async function getMyProfile(): Promise<UsersProfileResponse> {
  const { data } = await apiClient.get<UsersProfileResponse>('/me')
  return data
}

export async function changeMyPassword(request: ChangePasswordRequest): Promise<void> {
  await apiClient.put('/me/change-password', request)
}

export async function updateMyProfile(request: UpdateProfileRequest): Promise<void> {
  await apiClient.put('/me/info', request)
}

// ---- /users (Admin only) ----

export async function getUsers(filter: RequestFilter): Promise<PaginatedList<UserResponse>> {
  const { data } = await apiClient.get<PaginatedList<UserResponse>>('/users', {
    params: cleanRequestFilter(filter),
  })
  return data
}

export async function getUserById(userId: string): Promise<UserResponse> {
  const { data } = await apiClient.get<UserResponse>(`/users/${userId}`)
  return data
}

export async function updateUser(userId: string, request: UpdateUserRequest): Promise<void> {
  await apiClient.put(`/users/${userId}`, request)
}

export async function changeUserRole(userId: string, request: ChangeRoleRequest): Promise<void> {
  await apiClient.put(`/users/${userId}/role`, request)
}

export async function toggleUserStatus(userId: string): Promise<void> {
  await apiClient.put(`/users/${userId}/toggle-status`)
}

export async function unlockUser(userId: string): Promise<void> {
  await apiClient.put(`/users/${userId}/unlock`)
}
