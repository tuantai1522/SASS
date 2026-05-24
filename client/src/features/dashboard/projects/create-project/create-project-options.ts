import type { CreateProjectRequest } from "./types.ts";
import { createProject } from "./api.ts";
import { mutationOptions } from "@tanstack/react-query";

export function createProjectOptions() {
  return mutationOptions({
    mutationFn: (request: CreateProjectRequest) => createProject(request),
  });
}
