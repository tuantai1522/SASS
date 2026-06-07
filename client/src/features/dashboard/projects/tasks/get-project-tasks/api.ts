import { apiClient } from "@/lib";
import type { GetProjectTasksRequest, GetProjectTasksResponse } from "./types";
import type { PagedResponse } from "@/features/shared";

export async function getProjectTasks(
  request: GetProjectTasksRequest,
): Promise<PagedResponse<GetProjectTasksResponse>> {
  const response = await apiClient.post<PagedResponse<GetProjectTasksResponse>>(
    `/projects/${request.projectId}/tasks/search`,
    request,
    {
      skipAuthRefresh: true,
    },
  );
  return response.data;
}
