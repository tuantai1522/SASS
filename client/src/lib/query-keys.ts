import type { GetConversationsRequest } from "@/features/dashboard/conversations/get-conversations";
import type { GetMessagesRequest } from "@/features/dashboard/messages/get-messages";
import type { GetProjectTasksRequest } from "@/features/dashboard/projects/tasks/get-project-tasks";

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
    detail: (projectId: string) => ["projects", "detail", projectId] as const,
  },
  tasks: {
    all: ["tasks"] as const,
    list: (request: GetProjectTasksRequest) =>
      [...queryKeys.tasks.all, "list", request] as const,
  },

  messages: {
    list: (params: GetMessagesRequest) => ["messages", "list", params] as const,
  },
};
