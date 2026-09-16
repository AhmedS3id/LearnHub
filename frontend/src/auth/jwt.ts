// AuthResponse (Contracts/Authentication/AuthResponse.cs) does not include
// roles or permissions - those only exist as claims inside the JWT itself
// (see Authentication/JwtProvider.cs). So the token is decoded client-side
// purely to read its claims; the signature is never (and cannot be) verified
// in the browser - the backend remains the sole authority on every request.

const ROLE_CLAIM = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
const PERMISSIONS_CLAIM = 'permissions'

interface DecodedToken {
  sub?: string
  email?: string
  given_name?: string
  family_name?: string
  exp?: number
  [ROLE_CLAIM]?: string | string[]
  [PERMISSIONS_CLAIM]?: string | string[]
}

export interface TokenClaims {
  userId: string
  email: string | undefined
  firstName: string | undefined
  lastName: string | undefined
  roles: string[]
  permissions: string[]
  expiresAt: number | undefined
}

function base64UrlDecode(input: string): string {
  const padded = input.replace(/-/g, '+').replace(/_/g, '/').padEnd(input.length + ((4 - (input.length % 4)) % 4), '=')
  return decodeURIComponent(
    atob(padded)
      .split('')
      .map((c) => '%' + c.charCodeAt(0).toString(16).padStart(2, '0'))
      .join(''),
  )
}

function asArray(value: string | string[] | undefined): string[] {
  if (!value) return []
  return Array.isArray(value) ? value : [value]
}

export function decodeToken(token: string): TokenClaims | null {
  const parts = token.split('.')
  if (parts.length !== 3) return null

  try {
    const payload = JSON.parse(base64UrlDecode(parts[1])) as DecodedToken
    return {
      userId: payload.sub ?? '',
      email: payload.email,
      firstName: payload.given_name,
      lastName: payload.family_name,
      roles: asArray(payload[ROLE_CLAIM]),
      permissions: asArray(payload[PERMISSIONS_CLAIM]),
      expiresAt: payload.exp,
    }
  } catch {
    return null
  }
}

export function isExpired(claims: TokenClaims | null): boolean {
  if (!claims?.expiresAt) return true
  return Date.now() >= claims.expiresAt * 1000
}
