import type { GetConversationsRequest } from "@/features/dashboard/conversations/get-conversations";

export const queryKeys = {
  auth: {
    me: () => ["auth", "me"] as const,
  },
  conversations: {
    list: (params: GetConversationsRequest) =>
      ["conversations", "list", params] as const,
  },
};
