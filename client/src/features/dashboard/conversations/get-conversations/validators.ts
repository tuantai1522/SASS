import { z } from "zod";
import { PAGINATION } from "@/lib";

export const getConversationsParamsSchema = z.object({
  cursor: z.string().nullable(),
  limit: z.number().int().positive().max(50),
  order: z.enum(["Asc", "Desc"]),
});

export const defaultConversationsParams = getConversationsParamsSchema.parse({
  cursor: null,
  limit: PAGINATION.DEFAULT_PAGE_SIZE,
  order: "Desc",
});
