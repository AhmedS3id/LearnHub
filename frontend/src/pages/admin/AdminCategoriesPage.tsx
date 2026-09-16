import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { useState } from 'react'
import * as categoriesApi from '../../api/categories'
import { toApiError, type ApiError } from '../../api/errors'
import type { CategoryResponse } from '../../api/types'
import { CategoryForm } from '../../components/categories/CategoryForm'
import { Button } from '../../components/ui/Button'
import { Card } from '../../components/ui/Card'
import { EmptyState } from '../../components/ui/EmptyState'
import { ErrorBanner } from '../../components/ui/ErrorBanner'
import { Modal } from '../../components/ui/Modal'
import { PageSpinner } from '../../components/ui/Spinner'
import { useToast } from '../../components/ui/Toast'

type ModalState = { type: 'add' } | { type: 'edit'; category: CategoryResponse } | null

export function AdminCategoriesPage() {
  const { showToast } = useToast()
  const queryClient = useQueryClient()
  const [modal, setModal] = useState<ModalState>(null)
  const [modalError, setModalError] = useState<ApiError | null>(null)
  const [listError, setListError] = useState<ApiError | null>(null)

  const categoriesQuery = useQuery({ queryKey: ['categories'], queryFn: categoriesApi.getCategories })

  function closeModal() {
    setModal(null)
    setModalError(null)
  }

  const invalidate = () => queryClient.invalidateQueries({ queryKey: ['categories'] })

  const createMutation = useMutation({
    mutationFn: (values: { name: string; description: string }) => categoriesApi.createCategory(values),
    onSuccess: () => {
      showToast('Category created.')
      invalidate()
      closeModal()
    },
    onError: (err) => setModalError(toApiError(err)),
  })

  const updateMutation = useMutation({
    mutationFn: (values: { id: number; name: string; description: string }) =>
      categoriesApi.updateCategory(values.id, { name: values.name, description: values.description }),
    onSuccess: () => {
      showToast('Category updated.')
      invalidate()
      closeModal()
    },
    onError: (err) => setModalError(toApiError(err)),
  })

  const deleteMutation = useMutation({
    mutationFn: (id: number) => categoriesApi.deleteCategory(id),
    onSuccess: () => {
      showToast('Category deleted.')
      invalidate()
    },
    onError: (err) => setListError(toApiError(err)),
  })

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-slate-900">Categories</h1>
        <Button onClick={() => setModal({ type: 'add' })}>New category</Button>
      </div>

      <ErrorBanner error={listError} />

      {categoriesQuery.isLoading ? (
        <PageSpinner />
      ) : categoriesQuery.isError ? (
        <ErrorBanner error={toApiError(categoriesQuery.error)} />
      ) : categoriesQuery.data?.length === 0 ? (
        <EmptyState title="No categories yet" description="Create your first category to organize courses." />
      ) : (
        <div className="space-y-3">
          {categoriesQuery.data?.map((category) => (
            <Card key={category.id} className="flex flex-col justify-between gap-3 sm:flex-row sm:items-center">
              <div>
                <p className="font-medium text-slate-900">{category.name}</p>
                <p className="text-sm text-slate-500">{category.description}</p>
              </div>
              <div className="flex gap-2">
                <Button variant="secondary" size="sm" onClick={() => setModal({ type: 'edit', category })}>
                  Edit
                </Button>
                <Button
                  variant="danger"
                  size="sm"
                  isLoading={deleteMutation.isPending && deleteMutation.variables === category.id}
                  onClick={() => {
                    if (confirm(`Delete category "${category.name}"?`)) deleteMutation.mutate(category.id)
                  }}
                >
                  Delete
                </Button>
              </div>
            </Card>
          ))}
        </div>
      )}

      {modal?.type === 'add' && (
        <Modal title="New category" onClose={closeModal}>
          <CategoryForm
            isSubmitting={createMutation.isPending}
            error={modalError}
            onCancel={closeModal}
            onSubmit={(values) => createMutation.mutate(values)}
          />
        </Modal>
      )}

      {modal?.type === 'edit' && (
        <Modal title="Edit category" onClose={closeModal}>
          <CategoryForm
            initialName={modal.category.name}
            initialDescription={modal.category.description}
            isSubmitting={updateMutation.isPending}
            error={modalError}
            onCancel={closeModal}
            onSubmit={(values) => updateMutation.mutate({ id: modal.category.id, ...values })}
          />
        </Modal>
      )}
    </div>
  )
}
