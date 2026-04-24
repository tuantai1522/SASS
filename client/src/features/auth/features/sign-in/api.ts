import { httpClient } from '#/shared/api/http-client'
import type { AuthTokenResponse, SignInRequest } from './types'

export async function signIn(request: SignInRequest) {
  const response = await httpClient.post<AuthTokenResponse>(
    '/users/sign-in',
    request,
    {
      skipAuthRefresh: true,
    },
  )

  return response.data.token
}
