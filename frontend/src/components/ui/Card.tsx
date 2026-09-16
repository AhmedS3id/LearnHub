import type { ReactNode } from 'react'

export function Card({ children, className = '' }: { children: ReactNode; className?: string }) {
  return (
    <div className={`rounded-lg bg-white p-4 shadow ring-1 ring-slate-900/5 sm:p-6 ${className}`}>{children}</div>
  )
}
