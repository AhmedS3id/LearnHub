import { Route, Routes } from 'react-router-dom'
import { ProtectedRoute } from './auth/ProtectedRoute'
import { Permissions, Roles } from './auth/permissions'
import { AppLayout } from './components/layout/AppLayout'
import { ForbiddenPage } from './pages/ForbiddenPage'
import { NotFoundPage } from './pages/NotFoundPage'
import { ConfirmEmailPage } from './pages/auth/ConfirmEmailPage'
import { ForgotPasswordPage } from './pages/auth/ForgotPasswordPage'
import { LoginPage } from './pages/auth/LoginPage'
import { RegisterPage } from './pages/auth/RegisterPage'
import { ResendConfirmationPage } from './pages/auth/ResendConfirmationPage'
import { ResetPasswordPage } from './pages/auth/ResetPasswordPage'
import { CourseCatalogPage } from './pages/courses/CourseCatalogPage'
import { CourseDetailsPage } from './pages/courses/CourseDetailsPage'
import { CourseFormPage } from './pages/courses/CourseFormPage'
import { CourseManagePage } from './pages/courses/CourseManagePage'
import { InstructorCoursesPage } from './pages/courses/InstructorCoursesPage'
import { MyEnrollmentsPage } from './pages/enrollments/MyEnrollmentsPage'
import { ProfilePage } from './pages/profile/ProfilePage'
import { AdminCategoriesPage } from './pages/admin/AdminCategoriesPage'
import { AdminUsersPage } from './pages/admin/AdminUsersPage'

export default function App() {
  return (
    <Routes>
      {/* Public auth routes */}
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage />} />
      <Route path="/forgot-password" element={<ForgotPasswordPage />} />
      {/* These two exact paths are hardcoded into the backend's emails. */}
      <Route path="/auth/forgetPassword" element={<ResetPasswordPage />} />
      <Route path="/auth/emailConfirmation" element={<ConfirmEmailPage />} />
      <Route path="/resend-confirmation" element={<ResendConfirmationPage />} />

      {/* Authenticated app shell */}
      <Route element={<AppLayout />}>
        <Route
          path="/"
          element={
            <ProtectedRoute permission={Permissions.GetCourses}>
              <CourseCatalogPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="/courses/new"
          element={
            <ProtectedRoute permission={Permissions.AddCourses}>
              <CourseFormPage mode="create" />
            </ProtectedRoute>
          }
        />
        <Route
          path="/courses/:courseId"
          element={
            <ProtectedRoute permission={Permissions.GetCourses}>
              <CourseDetailsPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="/courses/:courseId/edit"
          element={
            <ProtectedRoute permission={Permissions.UpdateCourses}>
              <CourseFormPage mode="edit" />
            </ProtectedRoute>
          }
        />
        <Route
          path="/courses/:courseId/manage"
          element={
            <ProtectedRoute permission={Permissions.UpdateCourses}>
              <CourseManagePage />
            </ProtectedRoute>
          }
        />
        <Route
          path="/instructor/courses"
          element={
            <ProtectedRoute permission={Permissions.AddCourses}>
              <InstructorCoursesPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="/my-enrollments"
          element={
            <ProtectedRoute>
              <MyEnrollmentsPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="/profile"
          element={
            <ProtectedRoute>
              <ProfilePage />
            </ProtectedRoute>
          }
        />
        <Route
          path="/admin/categories"
          element={
            <ProtectedRoute role={Roles.Admin}>
              <AdminCategoriesPage />
            </ProtectedRoute>
          }
        />
        <Route
          path="/admin/users"
          element={
            <ProtectedRoute role={Roles.Admin}>
              <AdminUsersPage />
            </ProtectedRoute>
          }
        />

        <Route path="/403" element={<ForbiddenPage />} />
      </Route>

      <Route path="*" element={<NotFoundPage />} />
    </Routes>
  )
}
