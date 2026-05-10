import { MessageItem } from "../components/MessageItem";
import { useInfiniteQuery } from "@tanstack/react-query";
import { getMessagesOptions } from "../get-messages-options";
import { defaultMessagesParams } from "../validators";
import React, {
  useCallback,
  useEffect,
  useMemo,
  useRef,
  useState,
} from "react";
import { Spinner } from "@/features/shared";

interface MessageListProps {
  conversationId: string;
}
export function MessageList({ conversationId }: MessageListProps) {
  const {
    data,
    fetchNextPage,
    hasNextPage,
    isFetchingNextPage,
    isPending,
    // isError,
  } = useInfiniteQuery(
    getMessagesOptions({ ...defaultMessagesParams, conversationId }),
  );

  const messages = useMemo(() => {
    return [...(data?.pages ?? [])].reverse().flatMap((page) => page.items);
  }, [data?.pages]);

  const [hasInitialScrolled, setHasInitialScrolled] = useState(false);

  const viewportRef = useRef<HTMLDivElement | null>(null);
  const bottomRef = useRef<HTMLDivElement | null>(null);
  const prevScrollHeightRef = useRef<number | null>(null);

  useEffect(() => {
    if (hasInitialScrolled || !messages.length) return;

    requestAnimationFrame(() => {
      bottomRef.current?.scrollIntoView({ block: "end" });
      setHasInitialScrolled(true);
    });
  }, [hasInitialScrolled, messages.length]);

  useEffect(() => {
    const prevScrollHeight = prevScrollHeightRef.current;

    if (prevScrollHeight === null) return;

    const viewport = viewportRef.current;
    if (!viewport) return;

    requestAnimationFrame(() => {
      viewport.scrollTop = viewport.scrollHeight - prevScrollHeight;
      prevScrollHeightRef.current = null;
    });
  }, [messages.length]);

  const handleScroll = useCallback(
    (event: React.UIEvent<HTMLDivElement>) => {
      if (!hasInitialScrolled) return;

      const el = event.currentTarget;
      const isAtTop = el.scrollTop <= 80;

      if (isAtTop && hasNextPage && !isFetchingNextPage) {
        prevScrollHeightRef.current = el.scrollHeight;
        void fetchNextPage();
      }
    },
    [fetchNextPage, hasNextPage, isFetchingNextPage, hasInitialScrolled],
  );

  return (
    <div className="relative h-full">
      <div
        ref={viewportRef}
        className="h-full min-h-0 w-full overflow-y-auto"
        onScroll={handleScroll}
      >
        <div className="flex min-h-full flex-col gap-2 px-4 pt-4">
          {isFetchingNextPage && (
            <div className="flex justify-center py-2">
              <Spinner className="size-6" />
            </div>
          )}

          {isPending ? (
            <div className="flex min-h-32 items-center justify-center">
              <Spinner className="size-16" />
            </div>
          ) : (
            messages.map((message) => (
              <MessageItem key={message.id} message={message} />
            ))
          )}

          <div ref={bottomRef} />
        </div>
      </div>
    </div>
  );
}
