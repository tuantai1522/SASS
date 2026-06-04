import type { GetConversationsRequest } from "@/features/dashboard/conversations/get-conversations";
import type { GetMessagesRequest } from "@/features/dashboard/messages/get-messages";

export const queryKeys = {
  auth: {
    me: () => ["auth", "me"] as const,
  },
  conversations: {
    all: ["conversations"] as const,
    lists: () => [...queryKeys.conversations.all, "list"] as const,
    list: (params: GetConversationsRequest) =>
      [...queryKeys.conversations.lists(), params] as const,
    detail: (conversationId: string) =>
      ["conversations", "detail", conversationId] as const,
  },
  projects: {
    list: () => ["projects", "list"] as const,
  },
  messages: {
    list: (params: GetMessagesRequest) => ["messages", "list", params] as const,
  },
};
