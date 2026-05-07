import { z } from "zod";

export const createMessageSchema = z.object({
  content: z.string(),
});

export type CreateMessageFormValues = z.infer<typeof createMessageSchema>;
