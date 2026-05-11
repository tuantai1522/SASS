import { useCallback, useRef } from "react";

/**
 * To help scroll the message viewport to the latest message
 *
 * It only exposes `bottomRef`, which should be rendered at the end of the message list.
 *
 * It also tracks whether the user should be considered near the bottom after
 * an explicit scroll-to-bottom action.
 */
export function useScrollToBottom() {
  const bottomRef = useRef<HTMLDivElement | null>(null);
  const wasNearBottomRef = useRef(true);

  const scrollToBottom = useCallback((behavior: ScrollBehavior = "auto") => {
    requestAnimationFrame(() => {
      bottomRef.current?.scrollIntoView({
        block: "end",
        behavior,
      });

      wasNearBottomRef.current = true;
    });
  }, []);

  return {
    bottomRef,
    wasNearBottomRef,
    scrollToBottom,
  };
}
