import { Button } from './Button'

export function Pagination({
  pageNumber,
  totalPages,
  hasNextPage,
  hasPreviousPages,
  onPageChange,
}: {
  pageNumber: number
  totalPages: number
  hasNextPage: boolean
  hasPreviousPages: boolean
  onPageChange: (page: number) => void
}) {
  if (totalPages <= 1) return null

  return (
    <div className="flex items-center justify-between border-t border-slate-200 px-2 py-3">
      <p className="text-sm text-slate-600">
        Page <span className="font-medium">{pageNumber}</span> of{' '}
        <span className="font-medium">{totalPages}</span>
      </p>
      <div className="flex gap-2">
        <Button
          variant="secondary"
          size="sm"
          disabled={!hasPreviousPages}
          onClick={() => onPageChange(pageNumber - 1)}
        >
          Previous
        </Button>
        <Button variant="secondary" size="sm" disabled={!hasNextPage} onClick={() => onPageChange(pageNumber + 1)}>
          Next
        </Button>
      </div>
    </div>
  )
}
