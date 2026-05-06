import { z } from "zod";
import { getConversationByIdParamsSchema } from "./validators.ts";

export type GetConversationByIdRequest = z.infer<
  typeof getConversationByIdParamsSchema
>;

export type GetConversationByIdResponse = {
  id: string;
  name: string;
  createdAt: number;
  lastMessageUpdatedAt: number;
};
