// Normalizes every error shape the backend can actually produce (confirmed
// empirically against a running instance - see the four cases below) into a
// single ApiError the UI can handle uniformly.
//
// 1. Business-logic failure (Errors/*.cs -> Result.ToProblem()):
//    { type, title, status, errors: [code, description] }   <- errors is an ARRAY
//
// 2. FluentValidation failure (auto-validation on [ApiController] actions):
//    { type, title, status: 400, errors: { FieldName: [msg, ...] }, traceId }
//    <- errors is an OBJECT keyed by the PascalCase C# property name
//
// 3. Framework-level 401 (missing/invalid/expired JWT) or 403 (failed
//    [Authorize]/[HasPermission]): EMPTY body, bare status code.
//
// 4. Unhandled exception (Errors/GlobalExceptionHandler.cs):
//    { status: 500, type, title: "Internal Server Error" }   <- no errors field

import { AxiosError } from 'axios'

export interface ApiError {
  status: number
  /** Human-readable message safe to show directly to the user. */
  message: string
  /** The stable machine error code from a business-logic failure, e.g. "User.InvalidCredentials". */
  code?: string
  /** Per-field validation messages, keyed exactly as the backend named them (PascalCase). */
  fieldErrors?: Record<string, string[]>
  /** True when the request never reached the server (offline, DNS, connection refused, etc.). */
  isNetworkError?: boolean
}

interface ProblemDetailsBody {
  type?: string
  title?: string
  status?: number
  traceId?: string
  errors?: string[] | Record<string, string[]>
}

const FALLBACK_MESSAGES: Record<number, string> = {
  400: 'The request was invalid.',
  401: 'Your session has expired. Please sign in again.',
  403: "You don't have permission to do that.",
  404: 'The requested resource was not found.',
  409: 'This conflicts with existing data.',
  423: 'This account is locked.',
  500: 'Something went wrong on our end. Please try again.',
}

export function toApiError(error: unknown): ApiError {
  if (!(error instanceof AxiosError)) {
    return { status: 0, message: 'An unexpected error occurred.' }
  }

  if (!error.response) {
    return {
      status: 0,
      isNetworkError: true,
      message: 'Could not reach the server. Check your connection and try again.',
    }
  }

  const status = error.response.status
  const body = error.response.data as ProblemDetailsBody | undefined

  if (!body || Object.keys(body).length === 0) {
    // Framework-level 401/403 - no body at all.
    return { status, message: FALLBACK_MESSAGES[status] ?? `Request failed (${status}).` }
  }

  if (Array.isArray(body.errors)) {
    // Business-logic Result.ToProblem(): [code, description]
    const [code, description] = body.errors
    return {
      status,
      code,
      message: description || body.title || FALLBACK_MESSAGES[status] || 'Request failed.',
    }
  }

  if (body.errors && typeof body.errors === 'object') {
    // FluentValidation ValidationProblemDetails: { Field: [msg, ...] }
    const fieldErrors = body.errors
    const firstMessage = Object.values(fieldErrors)[0]?.[0]
    return {
      status,
      fieldErrors,
      message: firstMessage || body.title || 'Please check the highlighted fields.',
    }
  }

  return {
    status,
    message: body.title || FALLBACK_MESSAGES[status] || `Request failed (${status}).`,
  }
}

/** Looks up a field's validation messages regardless of casing differences. */
export function fieldErrorFor(error: ApiError | null, fieldName: string): string | undefined {
  if (!error?.fieldErrors) return undefined
  const key = Object.keys(error.fieldErrors).find(
    (k) => k.toLowerCase() === fieldName.toLowerCase(),
  )
  return key ? error.fieldErrors[key][0] : undefined
}
