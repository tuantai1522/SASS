import type { GetConversationsRequest } from "@/features/dashboard/conversations/get-conversations";
import type { GetMessagesRequest } from "@/features/dashboard/messages/get-messages";

export const queryKeys = {
  auth: {
    me: () => ["auth", "me"] as const,
  },
  conversations: {
    list: (params: GetConversationsRequest) =>
      ["conversations", "list", params] as const,
    detail: (conversationId: string) =>
      ["conversations", "detail", conversationId] as const,
  },
  messages: {
    list: (params: GetMessagesRequest) =>
      ["messages", "list", params] as const,
  },
};
