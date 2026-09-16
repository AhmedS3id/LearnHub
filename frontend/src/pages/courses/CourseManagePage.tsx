import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { useState } from 'react'
import { Link, useParams } from 'react-router-dom'
import * as coursesApi from '../../api/courses'
import * as lessonsApi from '../../api/lessons'
import * as sectionsApi from '../../api/sections'
import { toApiError, type ApiError } from '../../api/errors'
import type { LessonResponse, SectionWithLessonsResponse } from '../../api/types'
import { LessonForm } from '../../components/courses/LessonForm'
import { SectionForm } from '../../components/courses/SectionForm'
import { Button } from '../../components/ui/Button'
import { Card } from '../../components/ui/Card'
import { EmptyState } from '../../components/ui/EmptyState'
import { ErrorBanner } from '../../components/ui/ErrorBanner'
import { Modal } from '../../components/ui/Modal'
import { PageSpinner } from '../../components/ui/Spinner'
import { useToast } from '../../components/ui/Toast'

type ModalState =
  | { type: 'add-section' }
  | { type: 'edit-section'; section: SectionWithLessonsResponse }
  | { type: 'add-lesson'; sectionId: number }
  | { type: 'edit-lesson'; sectionId: number; lesson: LessonResponse }
  | null

export function CourseManagePage() {
  const { courseId } = useParams<{ courseId: string }>()
  const id = Number(courseId)
  const { showToast } = useToast()
  const queryClient = useQueryClient()

  const [modal, setModal] = useState<ModalState>(null)
  const [modalError, setModalError] = useState<ApiError | null>(null)

  const courseQuery = useQuery({ queryKey: ['course', id], queryFn: () => coursesApi.getCourseById(id) })
  const contentQuery = useQuery({ queryKey: ['course', id, 'content'], queryFn: () => lessonsApi.getCourseContent(id) })

  const invalidateContent = () => {
    queryClient.invalidateQueries({ queryKey: ['course', id, 'content'] })
  }

  function closeModal() {
    setModal(null)
    setModalError(null)
  }

  const createSectionMutation = useMutation({
    mutationFn: (values: { title: string; order: number }) => sectionsApi.createSection(id, values),
    onSuccess: () => {
      showToast('Section added.')
      invalidateContent()
      closeModal()
    },
    onError: (err) => setModalError(toApiError(err)),
  })

  const updateSectionMutation = useMutation({
    mutationFn: (values: { sectionId: number; title: string; order: number }) =>
      sectionsApi.updateSection(id, values.sectionId, { title: values.title, order: values.order }),
    onSuccess: () => {
      showToast('Section updated.')
      invalidateContent()
      closeModal()
    },
    onError: (err) => setModalError(toApiError(err)),
  })

  const deleteSectionMutation = useMutation({
    mutationFn: (sectionId: number) => sectionsApi.deleteSection(id, sectionId),
    onSuccess: () => {
      showToast('Section deleted.')
      invalidateContent()
    },
    onError: (err) => setModalError(toApiError(err)),
  })

  const createLessonMutation = useMutation({
    mutationFn: (values: {
      sectionId: number
      title: string
      description: string
      videoUrl: string
      durationInMinutes: number
      order: number
    }) => lessonsApi.createLesson(values.sectionId, values),
    onSuccess: () => {
      showToast('Lesson added.')
      invalidateContent()
      closeModal()
    },
    onError: (err) => setModalError(toApiError(err)),
  })

  const updateLessonMutation = useMutation({
    mutationFn: (values: {
      sectionId: number
      lessonId: number
      title: string
      description: string
      videoUrl: string
      durationInMinutes: number
      order: number
    }) => lessonsApi.updateLesson(values.sectionId, values.lessonId, values),
    onSuccess: () => {
      showToast('Lesson updated.')
      invalidateContent()
      closeModal()
    },
    onError: (err) => setModalError(toApiError(err)),
  })

  const deleteLessonMutation = useMutation({
    mutationFn: (values: { sectionId: number; lessonId: number }) =>
      lessonsApi.deleteLesson(values.sectionId, values.lessonId),
    onSuccess: () => {
      showToast('Lesson deleted.')
      invalidateContent()
    },
    onError: (err) => setModalError(toApiError(err)),
  })

  if (courseQuery.isLoading || contentQuery.isLoading) return <PageSpinner />
  if (courseQuery.isError) return <ErrorBanner error={toApiError(courseQuery.error)} />
  if (contentQuery.isError) return <ErrorBanner error={toApiError(contentQuery.error)} />

  const sections = contentQuery.data ?? []

  return (
    <div className="space-y-6">
      <div>
        <Link to={`/courses/${id}`} className="text-sm text-indigo-600 hover:text-indigo-500">
          ← Back to course
        </Link>
        <div className="mt-2 flex items-center justify-between">
          <h1 className="text-2xl font-bold text-slate-900">{courseQuery.data?.title}: sections &amp; lessons</h1>
          <Button size="sm" onClick={() => setModal({ type: 'add-section' })}>
            Add section
          </Button>
        </div>
      </div>

      {sections.length === 0 ? (
        <EmptyState title="No sections yet" description="Add your first section to start building this course." />
      ) : (
        <div className="space-y-4">
          {sections.map((section) => (
            <Card key={section.id}>
              <div className="flex items-center justify-between">
                <h2 className="text-base font-semibold text-slate-900">
                  {section.order}. {section.title}
                </h2>
                <div className="flex gap-2">
                  <Button variant="secondary" size="sm" onClick={() => setModal({ type: 'edit-section', section })}>
                    Edit
                  </Button>
                  <Button
                    variant="danger"
                    size="sm"
                    isLoading={deleteSectionMutation.isPending && deleteSectionMutation.variables === section.id}
                    onClick={() => {
                      if (confirm(`Delete section "${section.title}" and all its lessons?`)) {
                        deleteSectionMutation.mutate(section.id)
                      }
                    }}
                  >
                    Delete
                  </Button>
                </div>
              </div>

              <ul className="mt-3 divide-y divide-slate-100 border-t border-slate-100">
                {section.lessons.map((lesson) => (
                  <li key={lesson.id} className="flex items-center justify-between py-2 text-sm">
                    <span className="text-slate-700">
                      {lesson.order}. {lesson.title} · {lesson.durationInMinutes} min
                    </span>
                    <div className="flex gap-2">
                      <Button
                        variant="ghost"
                        size="sm"
                        onClick={() => setModal({ type: 'edit-lesson', sectionId: section.id, lesson })}
                      >
                        Edit
                      </Button>
                      <Button
                        variant="ghost"
                        size="sm"
                        isLoading={
                          deleteLessonMutation.isPending &&
                          deleteLessonMutation.variables?.lessonId === lesson.id
                        }
                        onClick={() => {
                          if (confirm(`Delete lesson "${lesson.title}"?`)) {
                            deleteLessonMutation.mutate({ sectionId: section.id, lessonId: lesson.id })
                          }
                        }}
                      >
                        Delete
                      </Button>
                    </div>
                  </li>
                ))}
              </ul>

              <Button
                variant="secondary"
                size="sm"
                className="mt-3"
                onClick={() => setModal({ type: 'add-lesson', sectionId: section.id })}
              >
                Add lesson
              </Button>
            </Card>
          ))}
        </div>
      )}

      {modal?.type === 'add-section' && (
        <Modal title="Add section" onClose={closeModal}>
          <SectionForm
            isSubmitting={createSectionMutation.isPending}
            error={modalError}
            onCancel={closeModal}
            onSubmit={(values) => createSectionMutation.mutate(values)}
          />
        </Modal>
      )}

      {modal?.type === 'edit-section' && (
        <Modal title="Edit section" onClose={closeModal}>
          <SectionForm
            initialTitle={modal.section.title}
            initialOrder={modal.section.order}
            isSubmitting={updateSectionMutation.isPending}
            error={modalError}
            onCancel={closeModal}
            onSubmit={(values) => updateSectionMutation.mutate({ sectionId: modal.section.id, ...values })}
          />
        </Modal>
      )}

      {modal?.type === 'add-lesson' && (
        <Modal title="Add lesson" onClose={closeModal}>
          <LessonForm
            isSubmitting={createLessonMutation.isPending}
            error={modalError}
            onCancel={closeModal}
            onSubmit={(values) => createLessonMutation.mutate({ sectionId: modal.sectionId, ...values })}
          />
        </Modal>
      )}

      {modal?.type === 'edit-lesson' && (
        <Modal title="Edit lesson" onClose={closeModal}>
          <LessonForm
            initialTitle={modal.lesson.title}
            initialDescription={modal.lesson.description}
            initialVideoUrl={modal.lesson.videoUrl}
            initialDuration={modal.lesson.durationInMinutes}
            initialOrder={modal.lesson.order}
            isSubmitting={updateLessonMutation.isPending}
            error={modalError}
            onCancel={closeModal}
            onSubmit={(values) =>
              updateLessonMutation.mutate({ sectionId: modal.sectionId, lessonId: modal.lesson.id, ...values })
            }
          />
        </Modal>
      )}
    </div>
  )
}
