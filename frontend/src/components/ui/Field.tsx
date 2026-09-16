import {
  type InputHTMLAttributes,
  type ReactNode,
  type SelectHTMLAttributes,
  type TextareaHTMLAttributes,
  useId,
} from 'react'

interface FieldWrapperProps {
  label: string
  error?: string
  hint?: string
  children: (id: string) => ReactNode
}

function FieldWrapper({ label, error, hint, children }: FieldWrapperProps) {
  const id = useId()
  return (
    <div>
      <label htmlFor={id} className="block text-sm font-medium text-slate-700">
        {label}
      </label>
      <div className="mt-1">{children(id)}</div>
      {error ? (
        <p className="mt-1 text-sm text-red-600">{error}</p>
      ) : hint ? (
        <p className="mt-1 text-sm text-slate-500">{hint}</p>
      ) : null}
    </div>
  )
}

const baseInputClasses =
  'block w-full rounded-md border-0 px-3 py-2 text-slate-900 shadow-sm ring-1 ring-inset ring-slate-300 placeholder:text-slate-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm'

type InputProps = InputHTMLAttributes<HTMLInputElement> & { label: string; error?: string; hint?: string }

export function Input({ label, error, hint, className = '', ...props }: InputProps) {
  return (
    <FieldWrapper label={label} error={error} hint={hint}>
      {(id) => (
        <input
          id={id}
          className={`${baseInputClasses} ${error ? 'ring-red-400 focus:ring-red-500' : ''} ${className}`}
          aria-invalid={Boolean(error)}
          {...props}
        />
      )}
    </FieldWrapper>
  )
}

type TextareaProps = TextareaHTMLAttributes<HTMLTextAreaElement> & {
  label: string
  error?: string
  hint?: string
}

export function Textarea({ label, error, hint, className = '', ...props }: TextareaProps) {
  return (
    <FieldWrapper label={label} error={error} hint={hint}>
      {(id) => (
        <textarea
          id={id}
          className={`${baseInputClasses} ${error ? 'ring-red-400 focus:ring-red-500' : ''} ${className}`}
          aria-invalid={Boolean(error)}
          {...props}
        />
      )}
    </FieldWrapper>
  )
}

type SelectProps = SelectHTMLAttributes<HTMLSelectElement> & {
  label: string
  error?: string
  hint?: string
}

export function Select({ label, error, hint, className = '', children, ...props }: SelectProps) {
  return (
    <FieldWrapper label={label} error={error} hint={hint}>
      {(id) => (
        <select
          id={id}
          className={`${baseInputClasses} ${error ? 'ring-red-400 focus:ring-red-500' : ''} ${className}`}
          aria-invalid={Boolean(error)}
          {...props}
        >
          {children}
        </select>
      )}
    </FieldWrapper>
  )
}
