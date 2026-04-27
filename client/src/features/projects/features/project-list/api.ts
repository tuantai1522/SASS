import { httpClient } from '#/shared/api/http-client'
import type { PagedResponse } from '#/shared/types'
import type {
  GetProjectsRequest,
  GetProjectsItemResponse,
} from '#/features/projects/features/project-list/types.ts'

export async function getProjects(params: GetProjectsRequest) {
  const response = await httpClient.get<PagedResponse<GetProjectsItemResponse>>(
    '/projects',
    {
      params,
    },
  )

  return response.data
}
