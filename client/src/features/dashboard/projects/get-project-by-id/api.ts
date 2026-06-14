import { apiClient } from "@/lib";
import type {
  GetProjectByIdApiResponse,
  GetProjectByIdRequest,
  GetProjectByIdResponse,
} from "./types";

export async function getProjectById(
  params: GetProjectByIdRequest,
): Promise<GetProjectByIdResponse> {
  const response = await apiClient.get<GetProjectByIdApiResponse>(
    `/projects/${params.projectId}`,
    {
      skipAuthRefresh: true,
    },
  );

  return {
    id: response.data.id,
    code: response.data.code,
    title: response.data.title,
    description: response.data.description?.trim() || undefined,
    createdAt: response.data.createdAt,
    currentUserRole: response.data.role,
    progress: response.data.progress,
    totalTasks: response.data.totalTasks,
    totalCompletedTasks: response.data.totalCompletedTasks,
  };
}
