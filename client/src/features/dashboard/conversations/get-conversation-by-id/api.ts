import { apiClient } from "@/lib";
import type {
  GetConversationByIdRequest,
  GetConversationByIdResponse,
} from "./types";

export async function getConversationById(
  params: GetConversationByIdRequest,
): Promise<GetConversationByIdResponse> {
  const response = await apiClient.get<GetConversationByIdResponse>(
    `/conversations/${params.conversationId}`,
    {
      skipAuthRefresh: true,
    },
  );

  return response.data;
}
