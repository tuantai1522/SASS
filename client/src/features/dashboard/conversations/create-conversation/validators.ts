import { z } from "zod";

export const createConversationSchema = z.object({
  name: z.string().min(1, "Name is required"),
});

export type CreateConversationFormValues = z.infer<typeof createConversationSchema>;
