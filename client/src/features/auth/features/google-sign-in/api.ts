import { httpClient } from '#/shared/api/http-client'
import type { GoogleSignInLinkResponse } from './types'

export async function getGoogleSignInLink() {
  const response =
    await httpClient.get<GoogleSignInLinkResponse>('/users/google/link')
  return response.data.url
}
