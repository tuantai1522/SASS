import { apiClient } from "@/lib";
import type {
  CreateConversationRequest,
  CreateConversationResponse,
} from "./types";

export async function createConversation(
  request: CreateConversationRequest,
): Promise<CreateConversationResponse> {
  const response = await apiClient.post<CreateConversationResponse>(
    "/conversations",
    request,
    {
      skipAuthRefresh: true,
    },
  );
  return response.data;
}
