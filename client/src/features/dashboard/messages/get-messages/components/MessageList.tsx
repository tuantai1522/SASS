import { MessageItem } from "../components/MessageItem";
import { useInfiniteQuery } from "@tanstack/react-query";
import { getMessagesOptions } from "../get-messages-options";
import { defaultMessagesParams } from "../validators";
import { useMemo } from "react";
import { Spinner } from "@/features/shared";

interface MessageListProps {
  conversationId: string;
}
export function MessageList({ conversationId }: MessageListProps) {
  const {
    data,
    // fetchNextPage,
    // hasNextPage,
    // isFetchingNextPage,
    isPending,
    // isError,
  } = useInfiniteQuery(
    getMessagesOptions({ ...defaultMessagesParams, conversationId }),
  );

  const messages = useMemo(() => {
    return data?.pages.flatMap((page) => page.items) ?? [];
  }, [data]);

  return (
    <div className="relative w-full">
      <div className="flex h-full flex-col gap-2 overflow-y-auto px-4">
        {isPending ? (
          <>
            <Spinner className="size-16" />
          </>
        ) : (
          <>
            {messages.map((message) => (
              <MessageItem message={message} />
            ))}
          </>
        )}
      </div>
    </div>
  );
}
