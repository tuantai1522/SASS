import { ChevronDown, Hash } from "lucide-react";
import {
  buttonVariants,
  Collapsible,
  CollapsibleContent,
  CollapsibleTrigger,
  ScrollArea,
  Spinner,
  useInfiniteSidebarScroll,
} from "@/features/shared";
import { Link } from "@tanstack/react-router";
import { cn } from "@/lib";
import { useInfiniteQuery } from "@tanstack/react-query";
import { getConversationsOptions } from "../get-conversations-options";
import { useMemo, useRef } from "react";
import { defaultConversationsParams } from "../validators";

export function ConversationList() {
  const loadMoreRef = useRef<HTMLDivElement | null>(null);

  const {
    data,
    fetchNextPage,
    hasNextPage,
    isFetchingNextPage,
    isPending,
    isError,
  } = useInfiniteQuery(getConversationsOptions(defaultConversationsParams));

  const conversations = useMemo(() => {
    return data?.pages.flatMap((page) => page.items) ?? [];
  }, [data]);

  useInfiniteSidebarScroll({
    targetRef: loadMoreRef,
    hasNextPage,
    isFetchingNextPage,
    fetchNextPage,
  });

  return (
    <Collapsible defaultOpen={true}>
      <CollapsibleTrigger className="flex w-full items-center justify-between px-2 py-1 text-sm font-medium text-muted-foreground hover:text-accent-foreground">
        Main
        <ChevronDown className="size-4 transition-transform duration-200" />
      </CollapsibleTrigger>
      <CollapsibleContent>
        {isPending ? (
          <div className="flex h-72 w-full items-center justify-center">
            <Spinner className="size-16" />
          </div>
        ) : isError ? (
          <div className="px-2 py-3 text-sm text-destructive">
            Failed to load conversations
          </div>
        ) : (
          <ScrollArea className="h-72 w-full">
            <div className="space-y-0.5 py-1">
              {conversations.map((conversation) => (
                <Link
                  key={conversation.id}
                  className={buttonVariants({
                    variant: "ghost",
                    className:
                      "w-full min-w-0 overflow-hidden justify-start px-2 py-1 text-muted-foreground hover:text-accent-foreground hover:bg-accent",
                  })}
                  to="/conversations/$conversationId"
                  params={{ conversationId: conversation.id }}
                >
                  {({ isActive }) => (
                    <>
                      <Hash
                        className={cn(
                          "size-4 shrink-0",
                          isActive && "text-accent-foreground",
                        )}
                      />
                      <span
                        className={cn(
                          "min-w-0 flex-1 truncate text-left",
                          isActive && "text-accent-foreground",
                        )}
                      >
                        {conversation.name}
                      </span>
                    </>
                  )}
                </Link>
              ))}
              <div
                ref={loadMoreRef}
                className={cn(
                  isFetchingNextPage && "flex h-8 items-center justify-center",
                )}
              >
                {isFetchingNextPage && <Spinner />}
              </div>
            </div>
          </ScrollArea>
        )}
      </CollapsibleContent>
    </Collapsible>
  );
}
