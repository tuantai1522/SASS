import { httpClient } from '#/shared/api/http-client.ts'
import type { GetMeResponse } from './types'

export async function getMe() {
  const response = await httpClient.get<GetMeResponse>(`/users/me`)
  return response.data
}
