import { z } from "zod";
import { normalizedName } from "@/lib";

export const createConversationSchema = z.object({
  name: z
    .string()
    .min(2, "Conversation name must be at least 5 characters")
    .max(512, "Conversation name must be at most 512 characters")
    .transform((name) => {
      return normalizedName(name);
    }),
});

export type CreateConversationFormValues = z.infer<
  typeof createConversationSchema
>;
