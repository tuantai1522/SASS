import { z } from "zod";
import { PAGINATION } from "@/lib";

export const getMessagesBodySchema = z.object({
  cursor: z.string().nullable(),
  limit: z.number().int().positive().max(50),
  order: z.enum(["Asc", "Desc"]),
});

export const getMessagesParamsSchema = getMessagesBodySchema.extend({
  conversationId: z.uuid(),
});

export const defaultMessagesParams = getMessagesBodySchema.parse({
  cursor: null,
  limit: PAGINATION.DEFAULT_MESSAGE_PAGE_SIZE,
  order: "Desc",
});
