import { useQuery } from '@tanstack/react-query'
import { useState, type FormEvent } from 'react'
import { Link } from 'react-router-dom'
import * as categoriesApi from '../../api/categories'
import * as coursesApi from '../../api/courses'
import { toApiError } from '../../api/errors'
import { useAuth } from '../../auth/AuthContext'
import { Permissions } from '../../auth/permissions'
import { CourseCard } from '../../components/courses/CourseCard'
import { Button } from '../../components/ui/Button'
import { EmptyState } from '../../components/ui/EmptyState'
import { ErrorBanner } from '../../components/ui/ErrorBanner'
import { Pagination } from '../../components/ui/Pagination'
import { Select } from '../../components/ui/Field'
import { PageSpinner } from '../../components/ui/Spinner'
import { useClientPagination } from '../../hooks/useClientPagination'

export function CourseCatalogPage() {
  const { hasPermission } = useAuth()
  const [searchInput, setSearchInput] = useState('')
  const [search, setSearch] = useState('')
  const [categoryId, setCategoryId] = useState<number | 'all'>('all')

  const categoriesQuery = useQuery({
    queryKey: ['categories'],
    queryFn: categoriesApi.getCategories,
  })

  // The backend only supports "search all courses by title" OR "browse one
  // category" - there is no endpoint that combines both, so selecting a
  // category clears the free-text search rather than pretending to combine them.
  const coursesQuery = useQuery({
    queryKey: categoryId === 'all' ? ['courses', { search }] : ['courses', 'category', categoryId],
    queryFn: () =>
      categoryId === 'all'
        ? coursesApi.getCourses({ searchValue: search })
        : coursesApi.getCoursesByCategory(categoryId),
  })

  const courses = coursesQuery.data ?? []
  const { pageItems, pageNumber, totalPages, hasNextPage, hasPreviousPages, setPageNumber } =
    useClientPagination(courses, 9)

  function handleSearchSubmit(e: FormEvent) {
    e.preventDefault()
    setCategoryId('all')
    setSearch(searchInput)
  }

  return (
    <div className="space-y-6">
      <div className="flex flex-col justify-between gap-4 sm:flex-row sm:items-center">
        <div>
          <h1 className="text-2xl font-bold text-slate-900">Courses</h1>
          <p className="text-sm text-slate-500">Browse everything LearnHub has to offer.</p>
        </div>
        {hasPermission(Permissions.AddCourses) && (
          <Link to="/courses/new">
            <Button>New course</Button>
          </Link>
        )}
      </div>

      <div className="flex flex-col gap-3 sm:flex-row">
        <form className="flex flex-1 gap-2" onSubmit={handleSearchSubmit}>
          <input
            type="search"
            placeholder="Search courses by title..."
            value={searchInput}
            onChange={(e) => setSearchInput(e.target.value)}
            className="w-full rounded-md border-0 px-3 py-2 text-sm text-slate-900 shadow-sm ring-1 ring-inset ring-slate-300 placeholder:text-slate-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600"
          />
          <Button type="submit" variant="secondary">
            Search
          </Button>
        </form>
        <div className="sm:w-56">
          <Select
            label="Category"
            value={categoryId}
            onChange={(e) => {
              const value = e.target.value
              setCategoryId(value === 'all' ? 'all' : Number(value))
              setSearchInput('')
              setSearch('')
            }}
          >
            <option value="all">All categories</option>
            {categoriesQuery.data?.map((category) => (
              <option key={category.id} value={category.id}>
                {category.name}
              </option>
            ))}
          </Select>
        </div>
      </div>

      {coursesQuery.isLoading ? (
        <PageSpinner />
      ) : coursesQuery.isError ? (
        <ErrorBanner error={toApiError(coursesQuery.error)} />
      ) : courses.length === 0 ? (
        <EmptyState title="No courses found" description="Try a different search or category." />
      ) : (
        <>
          <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-3">
            {pageItems.map((course) => (
              <CourseCard key={course.id} course={course} />
            ))}
          </div>
          <Pagination
            pageNumber={pageNumber}
            totalPages={totalPages}
            hasNextPage={hasNextPage}
            hasPreviousPages={hasPreviousPages}
            onPageChange={setPageNumber}
          />
        </>
      )}
    </div>
  )
}
