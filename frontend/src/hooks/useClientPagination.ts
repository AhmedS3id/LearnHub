import { useMemo, useState } from 'react'

// The backend's course list endpoints return a plain array with no
// server-side pagination (see api/courses.ts). This paginates an
// already-fetched array purely as a UI concern - it does not re-implement
// any backend search/filtering logic.
export function useClientPagination<T>(items: T[], pageSize = 9) {
  const [pageNumber, setPageNumber] = useState(1)

  const totalPages = Math.max(1, Math.ceil(items.length / pageSize))
  const clampedPage = Math.min(pageNumber, totalPages)

  const pageItems = useMemo(() => {
    const start = (clampedPage - 1) * pageSize
    return items.slice(start, start + pageSize)
  }, [items, clampedPage, pageSize])

  return {
    pageItems,
    pageNumber: clampedPage,
    totalPages,
    hasNextPage: clampedPage < totalPages,
    hasPreviousPages: clampedPage > 1,
    setPageNumber,
  }
}
