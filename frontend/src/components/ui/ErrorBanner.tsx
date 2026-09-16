import type { ApiError } from '../../api/errors'

export function ErrorBanner({ error }: { error: ApiError | string | null }) {
  if (!error) return null
  const message = typeof error === 'string' ? error : error.message

  return (
    <div className="rounded-md bg-red-50 p-3 text-sm text-red-700 ring-1 ring-inset ring-red-200" role="alert">
      {message}
    </div>
  )
}
