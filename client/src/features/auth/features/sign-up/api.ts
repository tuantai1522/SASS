import { httpClient } from '#/shared/api/http-client'
import type { SignUpRequest, SignUpResponse } from './types'

export async function signUp(request: SignUpRequest) {
  const response = await httpClient.post<SignUpResponse>(
    '/users/sign-up',
    request,
  )
  return response.data
}
