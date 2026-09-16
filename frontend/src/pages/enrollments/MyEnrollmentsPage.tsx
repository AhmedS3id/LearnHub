import { useQuery } from '@tanstack/react-query'
import { Link } from 'react-router-dom'
import * as enrollmentsApi from '../../api/enrollments'
import { toApiError } from '../../api/errors'
import { Card } from '../../components/ui/Card'
import { EmptyState } from '../../components/ui/EmptyState'
import { ErrorBanner } from '../../components/ui/ErrorBanner'
import { PageSpinner } from '../../components/ui/Spinner'
import { Button } from '../../components/ui/Button'

export function MyEnrollmentsPage() {
  const enrollmentsQuery = useQuery({ queryKey: ['my-enrollments'], queryFn: enrollmentsApi.getMyEnrollments })

  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-slate-900">My enrollments</h1>

      {enrollmentsQuery.isLoading ? (
        <PageSpinner />
      ) : enrollmentsQuery.isError ? (
        <ErrorBanner error={toApiError(enrollmentsQuery.error)} />
      ) : enrollmentsQuery.data?.length === 0 ? (
        <EmptyState
          title="You're not enrolled in any courses yet"
          description="Browse the catalog and enroll in a course to get started."
          action={
            <Link to="/">
              <Button>Browse courses</Button>
            </Link>
          }
        />
      ) : (
        <div className="space-y-3">
          {enrollmentsQuery.data?.map((enrollment) => (
            <Card key={enrollment.id} className="flex flex-col justify-between gap-2 sm:flex-row sm:items-center">
              <div>
                <Link
                  to={`/courses/${enrollment.courseId}`}
                  className="font-medium text-slate-900 hover:text-indigo-600"
                >
                  {enrollment.courseTitle}
                </Link>
                <p className="text-sm text-slate-500">
                  Enrolled {new Date(enrollment.enrolledOn).toLocaleDateString()}
                </p>
              </div>
              <div className="w-full sm:w-48">
                <div className="flex items-center justify-between text-xs text-slate-500">
                  <span>Progress</span>
                  <span>{enrollment.progress}%</span>
                </div>
                <div className="mt-1 h-2 w-full overflow-hidden rounded-full bg-slate-100">
                  <div
                    className="h-full rounded-full bg-indigo-600"
                    style={{ width: `${Math.min(100, Math.max(0, enrollment.progress))}%` }}
                  />
                </div>
              </div>
            </Card>
          ))}
        </div>
      )}
    </div>
  )
}
