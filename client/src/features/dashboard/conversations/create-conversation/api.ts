import { apiClient } from "@/lib";
import type { CreateConversationRequest } from "./types";
import type { IdResponse } from "@/features/shared";

export async function createConversation(
  request: CreateConversationRequest,
): Promise<IdResponse> {
  const response = await apiClient.post<IdResponse>("/conversations", request, {
    skipAuthRefresh: true,
  });
  return response.data;
}
