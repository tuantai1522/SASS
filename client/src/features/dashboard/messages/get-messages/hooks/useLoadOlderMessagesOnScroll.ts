import { type RefObject, useCallback } from "react";

type UseLoadOlderMessagesOnScrollParams = {
  hasNextPage: boolean;
  isFetchingNextPage: boolean;
  fetchNextPage: () => void | Promise<unknown>;
  markScrollHeightBeforePrepend: () => void;
  prevScrollHeightRef: RefObject<number | null>;
  loadMoreThreshold?: number;
};

/**
 * Detects when the user scrolls near the top of the message viewport and
 * triggers loading older messages.
 *
 * Before calling `fetchNextPage`, it records the current scroll height so
 * `usePreserveScrollOnPrepend` can restore the viewport after the older page
 * has been rendered.
 *
 * It also guards against duplicate fetches while a previous prepend operation
 * is still being processed.
 */
export function useLoadOlderMessagesOnScroll({
  hasNextPage,
  isFetchingNextPage,
  fetchNextPage,
  markScrollHeightBeforePrepend,
  prevScrollHeightRef,
  loadMoreThreshold = 80,
}: UseLoadOlderMessagesOnScrollParams) {
  const maybeLoadOlderMessages = useCallback(
    (element: HTMLDivElement) => {
      const isAtTop = element.scrollTop <= loadMoreThreshold;

      if (!isAtTop) return;
      if (!hasNextPage) return;
      if (isFetchingNextPage) return;

      // Prevent duplicate fetches before React Query updates isFetchingNextPage.
      if (prevScrollHeightRef.current !== null) return;

      markScrollHeightBeforePrepend();

      void fetchNextPage();
    },
    [
      fetchNextPage,
      hasNextPage,
      isFetchingNextPage,
      loadMoreThreshold,
      markScrollHeightBeforePrepend,
      prevScrollHeightRef,
    ],
  );

  return {
    maybeLoadOlderMessages,
  };
}
