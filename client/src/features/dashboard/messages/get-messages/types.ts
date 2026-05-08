import { z } from "zod";
import { getMessagesParamsSchema } from "./validators.ts";

export type GetMessagesRequest = z.infer<typeof getMessagesParamsSchema>;

export type GetMessagesResponse = {
  id: string;
  content: string;
  createdAt: number;
  isMe: boolean;
  senderId?: string;
  displayName?: string;
  avatarUrl?: string;
};
