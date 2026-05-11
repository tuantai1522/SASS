import { MessageItem } from "../components/MessageItem";
import { useInfiniteQuery } from "@tanstack/react-query";
import { getMessagesOptions } from "../get-messages-options";
import { defaultMessagesParams } from "../validators";
import { useMemo } from "react";
import { Spinner } from "@/features/shared";
import { useMessageListScroll } from "../hooks/useMessageListScroll";

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

  const { viewportRef, bottomRef, handleScroll } = useMessageListScroll({
    messages,
    fetchNextPage,
    hasNextPage,
    isFetchingNextPage,
  });

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
