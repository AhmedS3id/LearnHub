import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { useEffect, useState, type FormEvent } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import * as categoriesApi from '../../api/categories'
import * as coursesApi from '../../api/courses'
import { fieldErrorFor, toApiError, type ApiError } from '../../api/errors'
import type { CourseResponse } from '../../api/types'
import { Button } from '../../components/ui/Button'
import { Card } from '../../components/ui/Card'
import { ErrorBanner } from '../../components/ui/ErrorBanner'
import { Input, Select, Textarea } from '../../components/ui/Field'
import { PageSpinner } from '../../components/ui/Spinner'
import { useToast } from '../../components/ui/Toast'

export function CourseFormPage({ mode }: { mode: 'create' | 'edit' }) {
  const { courseId } = useParams<{ courseId: string }>()
  const id = Number(courseId)
  const navigate = useNavigate()
  const { showToast } = useToast()
  const queryClient = useQueryClient()

  const categoriesQuery = useQuery({ queryKey: ['categories'], queryFn: categoriesApi.getCategories })

  const courseQuery = useQuery({
    queryKey: ['course', id],
    queryFn: () => coursesApi.getCourseById(id),
    enabled: mode === 'edit' && Number.isFinite(id),
  })

  const [title, setTitle] = useState('')
  const [description, setDescription] = useState('')
  const [price, setPrice] = useState('')
  const [categoryId, setCategoryId] = useState('')
  const [error, setError] = useState<ApiError | null>(null)

  // CourseResponse only returns categoryName, not categoryId (Contracts/Course/CourseResponse.cs),
  // so the edit form derives the selected category by matching the unique
  // category name against the fetched category list.
  useEffect(() => {
    if (mode !== 'edit' || !courseQuery.data || !categoriesQuery.data) return
    const course = courseQuery.data
    setTitle(course.title)
    setDescription(course.description)
    setPrice(String(course.price))
    const match = categoriesQuery.data.find((c) => c.name === course.categoryName)
    if (match) setCategoryId(String(match.id))
  }, [mode, courseQuery.data, categoriesQuery.data])

  const mutation = useMutation({
    mutationFn: async (): Promise<CourseResponse | undefined> => {
      const request = { title, description, price: Number(price), categoryId: Number(categoryId) }
      if (mode === 'create') return coursesApi.createCourse(request)
      await coursesApi.updateCourse(id, request)
      return undefined
    },
    onSuccess: (result) => {
      showToast(mode === 'create' ? 'Course created.' : 'Course updated.')
      queryClient.invalidateQueries({ queryKey: ['courses'] })
      queryClient.invalidateQueries({ queryKey: ['course', id] })
      navigate(`/courses/${result ? result.id : id}`)
    },
    onError: (err) => setError(toApiError(err)),
  })

  function handleSubmit(e: FormEvent) {
    e.preventDefault()
    mutation.mutate()
  }

  if (mode === 'edit' && courseQuery.isLoading) return <PageSpinner />
  if (mode === 'edit' && courseQuery.isError) return <ErrorBanner error={toApiError(courseQuery.error)} />

  return (
    <Card className="mx-auto max-w-xl">
      <h1 className="text-xl font-semibold text-slate-900">
        {mode === 'create' ? 'Create a new course' : 'Edit course'}
      </h1>
      <form className="mt-6 space-y-4" onSubmit={handleSubmit}>
        <ErrorBanner error={error} />
        <Input
          label="Title"
          required
          minLength={5}
          maxLength={500}
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          error={fieldErrorFor(error, 'Title')}
        />
        <Textarea
          label="Description"
          required
          minLength={10}
          maxLength={1000}
          rows={5}
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          error={fieldErrorFor(error, 'Description')}
        />
        <div className="grid grid-cols-2 gap-3">
          <Input
            label="Price"
            type="number"
            required
            min="0.01"
            step="0.01"
            value={price}
            onChange={(e) => setPrice(e.target.value)}
            error={fieldErrorFor(error, 'Price')}
          />
          <Select
            label="Category"
            required
            value={categoryId}
            onChange={(e) => setCategoryId(e.target.value)}
            error={fieldErrorFor(error, 'CategoryId')}
          >
            <option value="" disabled>
              Select a category
            </option>
            {categoriesQuery.data?.map((category) => (
              <option key={category.id} value={category.id}>
                {category.name}
              </option>
            ))}
          </Select>
        </div>
        <div className="flex gap-2">
          <Button type="submit" isLoading={mutation.isPending}>
            {mode === 'create' ? 'Create course' : 'Save changes'}
          </Button>
          <Button type="button" variant="secondary" onClick={() => navigate(-1)}>
            Cancel
          </Button>
        </div>
      </form>
    </Card>
  )
}
