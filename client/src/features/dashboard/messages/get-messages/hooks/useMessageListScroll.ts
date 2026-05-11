import React, { useCallback, useRef } from "react";
import { useScrollToBottom } from "./useScrollToBottom";
import { usePreserveScrollOnPrepend } from "./usePreserveScrollOnPrepend";
import { useAutoScrollOnInitialLoad } from "./useAutoScrollOnInitialLoad";
import { useAutoScrollOnNewMessage } from "./useAutoScrollOnNewMessage";
import { useLoadOlderMessagesOnScroll } from "./useLoadOlderMessagesOnScroll";

type MessageLike = {
  id: string;
  isMe: boolean;
};

type UseMessageListScrollParams = {
  messages: MessageLike[];
  hasNextPage: boolean;
  isFetchingNextPage: boolean;
  fetchNextPage: () => void | Promise<unknown>;
  autoScrollThreshold?: number;
  loadMoreThreshold?: number;
};

/**
 * Composes all message-list scroll behaviors into a single hook for the UI.
 *
 * Responsibilities:
 * - Scroll to the latest message on initial load.
 * - Preserve viewport position when older messages are prepended.
 * - Auto-scroll for new messages when appropriate.
 * - Trigger loading older pages when the user scrolls near the top.
 *
 * Components should use this hook instead of implementing scroll logic directly.
 */
export function useMessageListScroll({
  messages,
  hasNextPage,
  isFetchingNextPage,
  fetchNextPage,
  autoScrollThreshold = 120,
  loadMoreThreshold = 80,
}: UseMessageListScrollParams) {
  const viewportRef = useRef<HTMLDivElement | null>(null);

  const latestMessage = messages[messages.length - 1];

  const latestMessageId = latestMessage?.id ?? null;
  const latestMessageIsMe = latestMessage?.isMe ?? false;
  const hasMessages = messages.length > 0;

  const { bottomRef, wasNearBottomRef, scrollToBottom } = useScrollToBottom();

  const isNearBottom = useCallback(
    (element: HTMLDivElement) => {
      const distanceToBottom =
        element.scrollHeight - element.scrollTop - element.clientHeight;

      return distanceToBottom <= autoScrollThreshold;
    },
    [autoScrollThreshold],
  );

  const { prevScrollHeightRef, markScrollHeightBeforePrepend } =
    usePreserveScrollOnPrepend({
      viewportRef,
      dependency: messages.length,
      onPreserved: () => {
        wasNearBottomRef.current = false;
      },
    });

  const { hasInitialScrolledRef, lastMessageIdRef } =
    useAutoScrollOnInitialLoad({
      hasMessages,
      latestMessageId,
      scrollToBottom,
    });

  useAutoScrollOnNewMessage({
    latestMessageId,
    latestMessageIsMe,
    hasInitialScrolledRef,
    lastMessageIdRef,
    wasNearBottomRef,
    prevScrollHeightRef,
    scrollToBottom,
  });

  const { maybeLoadOlderMessages } = useLoadOlderMessagesOnScroll({
    hasNextPage,
    isFetchingNextPage,
    fetchNextPage,
    markScrollHeightBeforePrepend,
    prevScrollHeightRef,
    loadMoreThreshold,
  });

  const handleScroll = useCallback(
    (event: React.UIEvent<HTMLDivElement>) => {
      if (!hasInitialScrolledRef.current) return;

      const element = event.currentTarget;

      wasNearBottomRef.current = isNearBottom(element);

      maybeLoadOlderMessages(element);
    },
    [
      hasInitialScrolledRef,
      isNearBottom,
      maybeLoadOlderMessages,
      wasNearBottomRef,
    ],
  );

  return {
    viewportRef,
    bottomRef,
    handleScroll,
    scrollToBottom,
  };
}
