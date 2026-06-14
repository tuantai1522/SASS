import { queryOptions } from "@tanstack/react-query";
import { queryKeys } from "@/lib";
import { getProjectById } from "./api";
import type { GetProjectByIdRequest } from "./types";

export function getProjectByIdOptions(params: GetProjectByIdRequest) {
  return queryOptions({
    queryKey: queryKeys.projects.detail(params.projectId),
    queryFn: () => getProjectById(params),
  });
}
