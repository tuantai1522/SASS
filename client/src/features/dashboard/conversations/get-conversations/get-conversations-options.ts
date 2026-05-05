import type { GetConversationsRequest } from "./types.ts";
import { getConversations } from "./api.ts";
import { infiniteQueryOptions } from "@tanstack/react-query";
import { queryKeys } from "@/lib";

export function getConversationsOptions(params: GetConversationsRequest) {
  return infiniteQueryOptions({
    queryKey: queryKeys.conversations.list(params),
    initialPageParam: params.cursor ?? null,
    queryFn: ({ pageParam }) => {
      return getConversations({
        ...params,
        cursor: pageParam,
      });
    },
    getNextPageParam: (lastPage) => {
      return lastPage.hasNextPage ? lastPage.nextCursor : null;
    },
  });
}
