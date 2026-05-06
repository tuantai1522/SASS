import type { GetConversationByIdRequest } from "./types.ts";
import { getConversationById } from "./api.ts";
import { queryOptions } from "@tanstack/react-query";
import { queryKeys } from "@/lib";

export function getConversationByIdOptions(params: GetConversationByIdRequest) {
  return queryOptions({
    queryKey: queryKeys.conversations.detail(params.conversationId),
    queryFn: () => getConversationById(params),
  });
}
