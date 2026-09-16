import { Link } from 'react-router-dom'
import { Button } from '../components/ui/Button'

export function ForbiddenPage() {
  return (
    <div className="flex flex-col items-center py-24 text-center">
      <p className="text-sm font-semibold text-red-600">403</p>
      <h1 className="mt-2 text-2xl font-bold text-slate-900">Access denied</h1>
      <p className="mt-2 text-sm text-slate-500">You don't have permission to view this page.</p>
      <Link to="/" className="mt-6">
        <Button>Back to courses</Button>
      </Link>
    </div>
  )
}
