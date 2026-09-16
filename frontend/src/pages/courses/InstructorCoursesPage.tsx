// There is no "my courses" endpoint on the backend - CourseController.GetAll
// returns every course, and CourseResponse doesn't even include an
// instructorId to filter by client-side. This page shows the full course
// list with manage actions for anyone with courses:update/delete; the
// backend is the real authority and returns 403 if you attempt to edit or
// delete a course that isn't actually yours.
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { useState } from 'react'
import { Link } from 'react-router-dom'
import * as coursesApi from '../../api/courses'
import { toApiError, type ApiError } from '../../api/errors'
import { useAuth } from '../../auth/AuthContext'
import { Permissions } from '../../auth/permissions'
import { Button } from '../../components/ui/Button'
import { Card } from '../../components/ui/Card'
import { EmptyState } from '../../components/ui/EmptyState'
import { ErrorBanner } from '../../components/ui/ErrorBanner'
import { PageSpinner } from '../../components/ui/Spinner'
import { useToast } from '../../components/ui/Toast'

export function InstructorCoursesPage() {
  const { hasPermission } = useAuth()
  const { showToast } = useToast()
  const queryClient = useQueryClient()
  const [error, setError] = useState<ApiError | null>(null)

  const coursesQuery = useQuery({ queryKey: ['courses', { search: '' }], queryFn: () => coursesApi.getCourses({}) })

  const deleteMutation = useMutation({
    mutationFn: (courseId: number) => coursesApi.deleteCourse(courseId),
    onSuccess: () => {
      showToast('Course deleted.')
      queryClient.invalidateQueries({ queryKey: ['courses'] })
    },
    onError: (err) => setError(toApiError(err)),
  })

  const canDelete = hasPermission(Permissions.DeleteCourses)

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-slate-900">Manage courses</h1>
        <Link to="/courses/new">
          <Button>New course</Button>
        </Link>
      </div>

      <ErrorBanner error={error} />

      {coursesQuery.isLoading ? (
        <PageSpinner />
      ) : coursesQuery.isError ? (
        <ErrorBanner error={toApiError(coursesQuery.error)} />
      ) : coursesQuery.data?.length === 0 ? (
        <EmptyState title="No courses yet" description="Create your first course to get started." />
      ) : (
        <div className="space-y-3">
          {coursesQuery.data?.map((course) => (
            <Card key={course.id} className="flex flex-col justify-between gap-3 sm:flex-row sm:items-center">
              <div>
                <Link to={`/courses/${course.id}`} className="font-medium text-slate-900 hover:text-indigo-600">
                  {course.title}
                </Link>
                <p className="text-sm text-slate-500">
                  {course.categoryName} · by {course.instructorName} · ${course.price.toFixed(2)}
                </p>
              </div>
              <div className="flex flex-wrap gap-2">
                <Link to={`/courses/${course.id}/manage`}>
                  <Button variant="secondary" size="sm">
                    Sections &amp; lessons
                  </Button>
                </Link>
                <Link to={`/courses/${course.id}/edit`}>
                  <Button variant="secondary" size="sm">
                    Edit
                  </Button>
                </Link>
                {canDelete && (
                  <Button
                    variant="danger"
                    size="sm"
                    isLoading={deleteMutation.isPending && deleteMutation.variables === course.id}
                    onClick={() => {
                      if (confirm(`Delete "${course.title}"?`)) deleteMutation.mutate(course.id)
                    }}
                  >
                    Delete
                  </Button>
                )}
              </div>
            </Card>
          ))}
        </div>
      )}
    </div>
  )
}
