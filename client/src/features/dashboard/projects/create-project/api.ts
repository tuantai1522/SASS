import { apiClient } from "@/lib";
import type { CreateProjectRequest } from "./types";
import type { IdResponse } from "@/features/shared";

export async function createProject(
  request: CreateProjectRequest,
): Promise<IdResponse> {
  const response = await apiClient.post<IdResponse>("/projects", request, {
    skipAuthRefresh: true,
  });
  return response.data;
}
