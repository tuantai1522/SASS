import { type RefObject, useEffect } from "react";

type UseAutoScrollOnNewMessageParams = {
  latestMessageId: string | null;
  latestMessageIsMe: boolean;

  hasInitialScrolledRef: RefObject<boolean>;
  lastMessageIdRef: RefObject<string | null>;
  wasNearBottomRef: RefObject<boolean>;
  prevScrollHeightRef: RefObject<number | null>;
  scrollToBottom: (behavior?: ScrollBehavior) => void;
};

/**
 * Handles auto-scroll behavior when a new latest message appears.
 *
 * Own messages always scroll to the bottom, because the sender expects to see
 * the message they just sent.
 *
 * Messages from other users only scroll to the bottom when the current user was
 * already near the bottom. This prevents interrupting users who are reading
 * older messages.
 *
 * The hook also skips auto-scroll while older messages are being prepended,
 * because that flow has its own scroll preservation logic.
 */
export function useAutoScrollOnNewMessage({
  latestMessageId,
  latestMessageIsMe,
  hasInitialScrolledRef,
  lastMessageIdRef,
  wasNearBottomRef,
  prevScrollHeightRef,
  scrollToBottom,
}: UseAutoScrollOnNewMessageParams) {
  useEffect(() => {
    if (!hasInitialScrolledRef.current || !latestMessageId) {
      return;
    }

    // Skip auto-scroll while preserving scroll after loading older messages.
    if (prevScrollHeightRef.current !== null) {
      return;
    }

    if (latestMessageId === lastMessageIdRef.current) {
      return;
    }

    // Own messages always scroll to bottom.
    // Other people's messages only scroll if the user was already near the bottom.
    const shouldScroll = latestMessageIsMe || wasNearBottomRef.current;

    lastMessageIdRef.current = latestMessageId;

    if (shouldScroll) {
      scrollToBottom("smooth");
    }
  }, [
    latestMessageId,
    latestMessageIsMe,
    hasInitialScrolledRef,
    lastMessageIdRef,
    wasNearBottomRef,
    prevScrollHeightRef,
    scrollToBottom,
  ]);
}
