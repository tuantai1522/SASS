import type { InfiniteData, QueryKey } from "@tanstack/react-query";
import { queryClient } from "@/router";
import { queryKeys } from "@/lib";
import type { CursorPagedResponse } from "@/features/shared";
import type { GetConversationByIdResponse } from "@/features/dashboard/conversations/get-conversation-by-id";
import type { GetConversationsResponse } from "@/features/dashboard/conversations/get-conversations";
import type {
  GetMessagesRequest,
  GetMessagesResponse,
} from "@/features/dashboard/messages/get-messages";
import type { RealtimeEventMap } from "../types";
import { useAuthStore } from "@/features/auths/manage-token";
import { ApplicationEventNames } from "../constants";

function isConversationMessageQuery(
  queryKey: QueryKey,
  conversationId: string,
): queryKey is readonly ["messages", "list", GetMessagesRequest] {
  if (
    !Array.isArray(queryKey) ||
    queryKey[0] !== "messages" ||
    queryKey[1] !== "list"
  ) {
    return false;
  }

  const params = queryKey[2];

  return (
    typeof params === "object" &&
    params !== null &&
    "conversationId" in params &&
    params.conversationId === conversationId
  );
}

function prependConversation(
  items: GetConversationsResponse[],
  nextItem: GetConversationsResponse,
) {
  const remainingItems = items.filter((item) => item.id !== nextItem.id);

  return [nextItem, ...remainingItems];
}

export function handleMessageCreated(
  payload: RealtimeEventMap[typeof ApplicationEventNames.MessageCreated],
) {
  const userId = useAuthStore.getState().userId;
  const nextMessage: GetMessagesResponse = {
    id: payload.id,
    content: payload.content,
    createdAt: payload.createdAt,
    isMe: payload.senderId === userId,
    senderId: payload.senderId,
    displayName: payload.displayName,
    avatarUrl: payload.avatarUrl,
  };

  const messageQueries = queryClient.getQueriesData<
    InfiniteData<CursorPagedResponse<GetMessagesResponse>, string | null>
  >({
    queryKey: ["messages", "list"],
  });

  for (const [queryKey, currentData] of messageQueries) {
    if (
      !isConversationMessageQuery(queryKey, payload.conversationId) ||
      !currentData
    ) {
      continue;
    }

    const alreadyExists = currentData.pages.some((page) =>
      page.items.some((item) => item.id === nextMessage.id),
    );

    if (alreadyExists || currentData.pages.length === 0) {
      continue;
    }

    const firstPage = currentData.pages[0];

    queryClient.setQueryData(queryKey, {
      ...currentData,
      pages: [
        {
          ...firstPage,
          items: [...firstPage.items, nextMessage],
        },
        ...currentData.pages.slice(1),
      ],
    });
  }

  queryClient.setQueryData<GetConversationByIdResponse>(
    queryKeys.conversations.detail(payload.conversationId),
    (currentConversation) => {
      if (!currentConversation) {
        return currentConversation;
      }

      return {
        ...currentConversation,
        lastMessageUpdatedAt: payload.createdAt,
      };
    },
  );

  const conversationQueries = queryClient.getQueriesData<
    InfiniteData<CursorPagedResponse<GetConversationsResponse>, string | null>
  >({
    queryKey: ["conversations", "list"],
  });

  for (const [queryKey, currentData] of conversationQueries) {
    if (!currentData || currentData.pages.length === 0) {
      continue;
    }

    const allItems = currentData.pages.flatMap((page) => page.items);
    const existingConversation = allItems.find(
      (conversation) => conversation.id === payload.conversationId,
    );

    if (!existingConversation) {
      void queryClient.invalidateQueries({
        queryKey: ["conversations", "list"],
      });
      continue;
    }

    const updatedConversation: GetConversationsResponse = {
      ...existingConversation,
      lastMessageUpdatedAt: payload.createdAt,
    };

    const reorderedItems = prependConversation(allItems, updatedConversation);
    const pageSizes = currentData.pages.map((page) => page.items.length);
    let cursor = 0;

    const nextPages = pageSizes.map((pageSize, index) => {
      const nextItems = reorderedItems.slice(cursor, cursor + pageSize);
      cursor += pageSize;

      return {
        ...currentData.pages[index],
        items: nextItems,
      };
    });

    queryClient.setQueryData(queryKey, {
      ...currentData,
      pages: nextPages,
    });
  }
}
