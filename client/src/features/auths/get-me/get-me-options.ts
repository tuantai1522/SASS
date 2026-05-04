import { getMe } from "./api.ts";
import { queryOptions } from "@tanstack/react-query";
import { queryKeys } from "@/lib";

export function getMeOptions() {
  return queryOptions({
    queryKey: queryKeys.auth.me(),
    queryFn: () => getMe(),
  });
}
