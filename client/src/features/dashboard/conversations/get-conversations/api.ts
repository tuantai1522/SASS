import { apiClient } from "@/lib";
import type {
  GetConversationsRequest,
  GetConversationsResponse,
} from "./types";
import type { CursorPagedResponse } from "@/features/shared";

export async function getConversations(
  request: GetConversationsRequest,
): Promise<CursorPagedResponse<GetConversationsResponse>> {
  const response = await apiClient.get<
    CursorPagedResponse<GetConversationsResponse>
  >("/conversations", {
    params: request,
    skipAuthRefresh: true,
  });

  return response.data;
}
