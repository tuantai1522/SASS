import { type RefObject, useCallback, useEffect, useRef } from "react";

type UsePreserveScrollOnPrependParams = {
  viewportRef: RefObject<HTMLDivElement | null>;
  dependency: unknown;
  onPreserved?: () => void;
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
export function usePreserveScrollOnPrepend({
  viewportRef,
  dependency,
  onPreserved,
}: UsePreserveScrollOnPrependParams) {
  const prevScrollHeightRef = useRef<number | null>(null);

  const markScrollHeightBeforePrepend = useCallback(() => {
    const viewport = viewportRef.current;
    if (!viewport) return;

    prevScrollHeightRef.current = viewport.scrollHeight;
  }, [viewportRef]);

  useEffect(() => {
    const prevScrollHeight = prevScrollHeightRef.current;

    if (prevScrollHeight === null) return;

    const viewport = viewportRef.current;
    if (!viewport) return;

    requestAnimationFrame(() => {
      // Keep the same visible content after older messages are prepended.
      viewport.scrollTop = viewport.scrollHeight - prevScrollHeight;

      prevScrollHeightRef.current = null;

      onPreserved?.();
    });
  }, [dependency, viewportRef, onPreserved]);

  return {
    prevScrollHeightRef,
    markScrollHeightBeforePrepend,
  };
}
