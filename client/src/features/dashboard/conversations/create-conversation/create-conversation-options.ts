import type { CreateConversationRequest } from "./types.ts";
import { createConversation } from "./api.ts";
import { mutationOptions } from "@tanstack/react-query";

export function createConversationOptions() {
  return mutationOptions({
    mutationFn: (request: CreateConversationRequest) =>
      createConversation(request),
  });
}
