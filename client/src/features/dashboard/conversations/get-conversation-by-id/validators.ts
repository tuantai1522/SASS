import { z } from "zod";

export const getConversationByIdParamsSchema = z.object({
  conversationId: z.uuid(),
});
