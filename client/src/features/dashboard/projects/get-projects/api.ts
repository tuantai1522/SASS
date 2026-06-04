import { apiClient } from "@/lib";
import type { GetProjectsResponse } from "./types";

export async function getProjects(): Promise<GetProjectsResponse[]> {
  const response = await apiClient.get<GetProjectsResponse[]>("/projects", {
    skipAuthRefresh: true,
  });
  return response.data;
}
