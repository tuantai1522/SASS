import { queryKeys } from '#/shared/lib'
import { getProjectById } from './api.ts'
import { queryOptions } from '@tanstack/react-query'

export const projectByIdOptions = (projectId: string) =>
  queryOptions({
    queryKey: queryKeys.projects.detail(projectId),
    queryFn: () => getProjectById(projectId),
  })
