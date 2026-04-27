import { queryKeys } from '#/shared/lib'
import type { GetProjectsRequest } from '#/features/projects'
import { getProjects } from './api.ts'
import { queryOptions } from "@tanstack/react-query";

export const projectListOptions = (params: GetProjectsRequest) =>
  queryOptions({
    queryKey: queryKeys.projects.list(params),
    queryFn: () => getProjects(params),
  });
