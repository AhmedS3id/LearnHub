import type { RequestFilter } from './types'

// The backend's [FromQuery] RequestFilter binds SearchValue as a
// non-nullable string (the project has <Nullable>enable</Nullable>), and
// ASP.NET Core's implicit required-property validation for non-nullable
// reference types rejects an explicitly-empty "?searchValue=" query value
// with 400 Bad Request ("The SearchValue field is required") - even though
// omitting the key entirely is fine and just leaves it at its string.Empty
// default server-side. So empty/undefined filter values must be dropped
// from the query string entirely rather than sent as "".
export function cleanRequestFilter(filter: RequestFilter): RequestFilter {
  const clean: RequestFilter = {}
  if (filter.pageNumber !== undefined) clean.pageNumber = filter.pageNumber
  if (filter.pageSize !== undefined) clean.pageSize = filter.pageSize
  if (filter.searchValue) clean.searchValue = filter.searchValue
  return clean
}
