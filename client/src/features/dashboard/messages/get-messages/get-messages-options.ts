import type { GetMessagesRequest } from "./types.ts";
import { infiniteQueryOptions } from "@tanstack/react-query";
import { queryKeys } from "@/lib";
import { getMessages } from "./api";

export function getMessagesOptions(params: GetMessagesRequest) {
  return infiniteQueryOptions({
    queryKey: queryKeys.messages.list(params),
    initialPageParam: params.cursor ?? null,
    queryFn: ({ pageParam }) => {
      return getMessages({
        ...params,
        cursor: pageParam,
      });
    },
    getNextPageParam: (lastPage) => {
      return lastPage.hasNextPage ? lastPage.nextCursor : null;
    },
  });
}
