import type { CreateMessageRequest } from "./types.ts";
import { createMessage } from "./api.ts";
import { mutationOptions } from "@tanstack/react-query";

export function createMessageOptions() {
  return mutationOptions({
    mutationFn: (request: CreateMessageRequest) => createMessage(request),
  });
}
