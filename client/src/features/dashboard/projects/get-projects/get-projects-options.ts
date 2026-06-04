import { queryOptions } from "@tanstack/react-query";
import { queryKeys } from "@/lib";
import { getProjects } from "./api";

export function getProjectsOptions() {
  return queryOptions({
    queryKey: queryKeys.projects.list(),
    queryFn: () => getProjects(),
  });
}
