import { httpClient } from '#/shared/api/http-client'
import type { GetProjectByIdResponse } from './types'

export async function getProjectById(id: string) {
  const response = await httpClient.get<GetProjectByIdResponse>(
    `/projects/${id}`,
  )
  return response.data
}
