import type { ReactNode } from 'react'
import { Link } from 'react-router-dom'

export function AuthLayout({ title, subtitle, children }: { title: string; subtitle?: string; children: ReactNode }) {
  return (
    <div className="flex min-h-screen flex-col items-center justify-center bg-slate-50 px-4 py-12">
      <Link to="/" className="mb-6 text-2xl font-bold text-indigo-600">
        LearnHub
      </Link>
      <div className="w-full max-w-md rounded-lg bg-white p-6 shadow ring-1 ring-slate-900/5 sm:p-8">
        <h1 className="text-xl font-semibold text-slate-900">{title}</h1>
        {subtitle && <p className="mt-1 text-sm text-slate-500">{subtitle}</p>}
        <div className="mt-6">{children}</div>
      </div>
    </div>
  )
}
