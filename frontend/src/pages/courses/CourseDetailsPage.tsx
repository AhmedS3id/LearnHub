import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { useState } from 'react'
import { Link, useNavigate, useParams } from 'react-router-dom'
import * as coursesApi from '../../api/courses'
import * as enrollmentsApi from '../../api/enrollments'
import * as lessonsApi from '../../api/lessons'
import { toApiError, type ApiError } from '../../api/errors'
import { useAuth } from '../../auth/AuthContext'
import { Permissions } from '../../auth/permissions'
import { CourseContent } from '../../components/courses/CourseContent'
import { ReviewSection } from '../../components/reviews/ReviewSection'
import { Badge } from '../../components/ui/Badge'
import { Button } from '../../components/ui/Button'
import { Card } from '../../components/ui/Card'
import { ErrorBanner } from '../../components/ui/ErrorBanner'
import { PageSpinner } from '../../components/ui/Spinner'
import { useToast } from '../../components/ui/Toast'

export function CourseDetailsPage() {
  const { courseId } = useParams<{ courseId: string }>()
  const id = Number(courseId)
  const navigate = useNavigate()
  const { hasPermission } = useAuth()
  const { showToast } = useToast()
  const queryClient = useQueryClient()
  const [actionError, setActionError] = useState<ApiError | null>(null)

  const courseQuery = useQuery({
    queryKey: ['course', id],
    queryFn: () => coursesApi.getCourseById(id),
    enabled: Number.isFinite(id),
  })

  const contentQuery = useQuery({
    queryKey: ['course', id, 'content'],
    queryFn: () => lessonsApi.getCourseContent(id),
    enabled: Number.isFinite(id),
  })

  const myEnrollmentsQuery = useQuery({
    queryKey: ['my-enrollments'],
    queryFn: enrollmentsApi.getMyEnrollments,
  })

  const isEnrolled = myEnrollmentsQuery.data?.some((e) => e.courseId === id) ?? false

  const enrollMutation = useMutation({
    mutationFn: () => enrollmentsApi.enrollInCourse(id),
    onSuccess: () => {
      showToast('Enrolled successfully.')
      queryClient.invalidateQueries({ queryKey: ['my-enrollments'] })
    },
    onError: (err) => setActionError(toApiError(err)),
  })

  const deleteMutation = useMutation({
    mutationFn: () => coursesApi.deleteCourse(id),
    onSuccess: () => {
      showToast('Course deleted.')
      navigate('/')
    },
    onError: (err) => setActionError(toApiError(err)),
  })

  if (!Number.isFinite(id)) {
    return <ErrorBanner error="Invalid course." />
  }

  if (courseQuery.isLoading) return <PageSpinner />

  if (courseQuery.isError) {
    return <ErrorBanner error={toApiError(courseQuery.error)} />
  }

  const course = courseQuery.data!
  const canManage = hasPermission(Permissions.UpdateCourses)
  const canDelete = hasPermission(Permissions.DeleteCourses)
  const canEnroll = hasPermission(Permissions.AddEnrollments)

  return (
    <div className="space-y-8">
      <Card>
        <div className="flex flex-col justify-between gap-4 sm:flex-row sm:items-start">
          <div>
            <div className="mb-2 flex items-center gap-2">
              <Badge tone="indigo">{course.categoryName}</Badge>
            </div>
            <h1 className="text-2xl font-bold text-slate-900">{course.title}</h1>
            <p className="mt-1 text-sm text-slate-500">Taught by {course.instructorName}</p>
          </div>
          <div className="text-right">
            <p className="text-2xl font-bold text-slate-900">${course.price.toFixed(2)}</p>
          </div>
        </div>

        <p className="mt-4 whitespace-pre-line text-sm text-slate-700">{course.description}</p>

        <ErrorBanner error={actionError} />

        <div className="mt-4 flex flex-wrap gap-2">
          {canEnroll &&
            (isEnrolled ? (
              <Link to="/my-enrollments">
                <Button variant="secondary" size="sm">
                  ✓ Enrolled - view progress
                </Button>
              </Link>
            ) : (
              <Button size="sm" isLoading={enrollMutation.isPending} onClick={() => enrollMutation.mutate()}>
                Enroll in this course
              </Button>
            ))}
          {canManage && (
            <>
              <Link to={`/courses/${id}/edit`}>
                <Button variant="secondary" size="sm">
                  Edit course
                </Button>
              </Link>
              <Link to={`/courses/${id}/manage`}>
                <Button variant="secondary" size="sm">
                  Manage sections &amp; lessons
                </Button>
              </Link>
            </>
          )}
          {canDelete && (
            <Button
              variant="danger"
              size="sm"
              isLoading={deleteMutation.isPending}
              onClick={() => {
                if (confirm('Delete this course? This cannot be undone.')) deleteMutation.mutate()
              }}
            >
              Delete course
            </Button>
          )}
        </div>
      </Card>

      <div>
        <h2 className="mb-3 text-lg font-semibold text-slate-900">Course content</h2>
        {contentQuery.isLoading ? (
          <PageSpinner />
        ) : contentQuery.isError ? (
          <ErrorBanner error={toApiError(contentQuery.error)} />
        ) : (
          <CourseContent sections={contentQuery.data ?? []} />
        )}
      </div>

      <ReviewSection courseId={id} />
    </div>
  )
}
