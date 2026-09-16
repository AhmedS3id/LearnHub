// Mirrors LearnHub_Api/Abstractions/Consts/Permissions.cs exactly. These are
// the literal string values placed in the JWT's "permissions" claim array.
export const Permissions = {
  ManageUsers: 'users:manage',

  GetCategories: 'categories:read',
  AddCategories: 'categories:add',
  UpdateCategories: 'categories:update',
  DeleteCategories: 'categories:delete',

  GetCourses: 'courses:read',
  AddCourses: 'courses:add',
  UpdateCourses: 'courses:update',
  DeleteCourses: 'courses:delete',

  GetEnrollments: 'enrollments:read',
  AddEnrollments: 'enrollments:add',
  UpdateEnrollments: 'enrollments:update',

  GetLessons: 'lessons:read',
  AddLessons: 'lessons:add',
  UpdateLessons: 'lessons:update',
  DeleteLessons: 'lessons:delete',

  GetSections: 'sections:read',
  AddSections: 'sections:add',
  UpdateSections: 'sections:update',
  DeleteSections: 'sections:delete',

  GetReviews: 'reviews:read',
  AddReviews: 'reviews:add',
  UpdateReviews: 'reviews:update',
  DeleteReviews: 'reviews:delete',

  GetProfile: 'profile:read',
  UpdateProfile: 'profile:update',
} as const

export type Permission = (typeof Permissions)[keyof typeof Permissions]

// Mirrors LearnHub_Api/Abstractions/Consts/DefaultRoles.cs
export const Roles = {
  Admin: 'Admin',
  Instructor: 'Instructor',
  Member: 'Member',
} as const

export type Role = (typeof Roles)[keyof typeof Roles]
