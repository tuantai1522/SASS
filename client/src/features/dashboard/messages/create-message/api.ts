import { apiClient } from "@/lib";
import type { CreateMessageRequest } from "./types";
import type { IdResponse } from "@/features/shared";

export async function createMessage(
  request: CreateMessageRequest,
): Promise<IdResponse> {
  const response = await apiClient.post<IdResponse>(
    `/conversations/${request.conversationId}/messages`,
    request,
    {
      skipAuthRefresh: true,
    },
  );
  return response.data;
}
