import { queryOptions } from "@tanstack/react-query";
import { queryKeys } from "@/lib";
import { getProjectTasks } from "./api";
import type { GetProjectTasksRequest } from "./types";

export function getProjectTasksOptions(request: GetProjectTasksRequest) {
  return queryOptions({
    queryKey: queryKeys.tasks.list(request),
    queryFn: () => getProjectTasks(request),
  });
}
