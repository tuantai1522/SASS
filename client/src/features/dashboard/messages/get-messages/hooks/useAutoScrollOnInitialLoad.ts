import { useEffect, useRef } from "react";

type UseAutoScrollOnInitialLoadParams = {
  hasMessages: boolean;
  latestMessageId: string | null;
  scrollToBottom: () => void;
};

/**
 * Scrolls to the latest message once after the first successful message load.
 *
 * This prevents the chat from opening at the top of the message history.
 * The hook also stores the initial latest message id so future effects can
 * distinguish between the initial load and truly new incoming messages.
 */
export function useAutoScrollOnInitialLoad({
  hasMessages,
  latestMessageId,
  scrollToBottom,
}: UseAutoScrollOnInitialLoadParams) {
  const hasInitialScrolledRef = useRef(false);
  const lastMessageIdRef = useRef<string | null>(null);

  useEffect(() => {
    if (hasInitialScrolledRef.current || !hasMessages) return;

    // Scroll to the latest message after the first successful message load.
    scrollToBottom();

    hasInitialScrolledRef.current = true;
    lastMessageIdRef.current = latestMessageId;
  }, [hasMessages, latestMessageId, scrollToBottom]);

  return {
    hasInitialScrolledRef,
    lastMessageIdRef,
  };
}
