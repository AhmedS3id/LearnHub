// These types mirror the backend's C# records/DTOs field-for-field, using the
// camelCase JSON property names ASP.NET Core's default System.Text.Json
// naming policy produces. Nothing here is invented - every shape traces back
// to a specific Contracts/*.cs record in LearnHub_Api.

// ---------- Pagination (Abstractions/PaginatedList.cs) ----------

export interface PaginatedList<T> {
  items: T[]
  pageNumber: number
  totalPages: number
  hasNextPage: boolean
  hasPreviousPages: boolean
}

// Common/RequestFilter.cs - accepted by any endpoint bound to [FromQuery] RequestFilter
export interface RequestFilter {
  pageNumber?: number
  pageSize?: number
  searchValue?: string
}

// ---------- Authentication (Contracts/Authentication/*.cs) ----------

export interface RegisterRequest {
  firstName: string
  lastName: string
  email: string
  password: string
  confirmPassword: string
}

export interface LoginRequest {
  email: string
  password: string
}

export interface RefreshTokenRequest {
  token: string
  refreshToken: string
}

export interface ConfirmEmailRequest {
  userId: string
  code: string
}

export interface ResendConfirmationEmailRequest {
  email: string
}

export interface ForgetPasswordRequest {
  email: string
}

export interface ResetPasswordRequest {
  email: string
  code: string
  newPassword: string
}

export interface AuthResponse {
  id: string
  email: string | null
  firstName: string
  lastName: string
  token: string
  expiresIn: number
  refreshToken: string
  refreshTokenExpiration: string
}

// ---------- User (Contracts/User/*.cs) ----------

export interface UsersProfileResponse {
  email: string
  userName: string
  firstName: string
  lastName: string
}

export interface UserResponse {
  id: string
  firstName: string
  lastName: string
  email: string
  isDisabled: boolean
  roles: string[]
}

export interface UpdateProfileRequest {
  firstName: string
  lastName: string
}

export interface UpdateUserRequest {
  firstName: string
  lastName: string
  email: string
}

export interface ChangePasswordRequest {
  currentPassword: string
  newPassword: string
}

export interface ChangeRoleRequest {
  role: string
}

// ---------- Category (Contracts/Category/*.cs) ----------

export interface CategoryRequest {
  name: string
  description: string
}

export interface CategoryResponse {
  id: number
  name: string
  description: string
}

// ---------- Course (Contracts/Course/*.cs) ----------

export interface CourseRequest {
  title: string
  description: string
  price: number
  categoryId: number
}

export interface CourseResponse {
  id: number
  title: string
  description: string
  price: number
  categoryName: string
  instructorName: string
}

// ---------- Section (Contracts/Section/*.cs) ----------

export interface SectionRequest {
  title: string
  order: number
}

export interface SectionResponse {
  id: number
  title: string
  order: number
  courseTitle: string
}

// ---------- Lesson (Contracts/Lesson/*.cs) ----------

export interface LessonRequest {
  title: string
  description: string
  videoUrl: string
  durationInMinutes: number
  order: number
}

export interface LessonResponse {
  id: number
  title: string
  description: string
  videoUrl: string
  durationInMinutes: number
  order: number
}

export interface SectionWithLessonsResponse {
  id: number
  title: string
  order: number
  lessons: LessonResponse[]
}

// ---------- Enrollment (Contracts/Enrollment/*.cs) ----------

export interface EnrollmentResponse {
  id: number
  courseId: number
  courseTitle: string
  progress: number
  enrolledOn: string
}

// ---------- Review (Contracts/Review/*.cs) ----------

export interface ReviewRequest {
  comment: string
  rating: number
}

export interface ReviewResponse {
  id: number
  studentName: string
  courseName: string
  comment: string
  rating: number
  createdOn: string
}
