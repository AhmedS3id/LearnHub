// Mirrors LearnHub_Api/Controllers/AuthController.cs exactly.
import { apiClient } from './client'
import type {
  AuthResponse,
  ConfirmEmailRequest,
  ForgetPasswordRequest,
  LoginRequest,
  RefreshTokenRequest,
  RegisterRequest,
  ResendConfirmationEmailRequest,
  ResetPasswordRequest,
} from './types'

export async function register(request: RegisterRequest): Promise<{ message: string }> {
  const { data } = await apiClient.post('/Auth/register', request)
  return data
}

export async function login(request: LoginRequest): Promise<AuthResponse> {
  const { data } = await apiClient.post<AuthResponse>('/Auth', request)
  return data
}

export async function refreshToken(request: RefreshTokenRequest): Promise<AuthResponse> {
  const { data } = await apiClient.post<AuthResponse>('/Auth/refresh', request)
  return data
}

export async function logout(request: RefreshTokenRequest): Promise<void> {
  await apiClient.post('/Auth/logout', request)
}

export async function confirmEmail(request: ConfirmEmailRequest): Promise<void> {
  await apiClient.post('/Auth/confirm-email', request)
}

export async function resendConfirmationEmail(request: ResendConfirmationEmailRequest): Promise<void> {
  await apiClient.post('/Auth/resend-email-confirm', request)
}

export async function forgetPassword(request: ForgetPasswordRequest): Promise<void> {
  await apiClient.post('/Auth/forget-password', request)
}

export async function resetPassword(request: ResetPasswordRequest): Promise<void> {
  await apiClient.post('/Auth/reset-password', request)
}
