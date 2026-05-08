import { apiClient } from "@/lib";
import type { GetMessagesRequest, GetMessagesResponse } from "./types";
import type { CursorPagedResponse } from "@/features/shared";

export async function getMessages(
  request: GetMessagesRequest,
): Promise<CursorPagedResponse<GetMessagesResponse>> {
  const response = await apiClient.post<
    CursorPagedResponse<GetMessagesResponse>
  >(`/conversations/${request.conversationId}/messages/query`, request, {
    skipAuthRefresh: true,
  });
  return response.data;
}
