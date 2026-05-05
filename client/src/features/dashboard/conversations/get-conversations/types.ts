import { z } from "zod";
import { getConversationsParamsSchema } from "./validators.ts";

export type GetConversationsRequest = z.infer<
  typeof getConversationsParamsSchema
>;

export type GetConversationsResponse = {
  id: string;
  name: string;
  createdAt: number;
  lastMessageUpdatedAt: number;
};
