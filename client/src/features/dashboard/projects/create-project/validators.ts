import { z } from "zod";

export const createProjectSchema = z.object({
  code: z
    .string()
    .min(1, "Code must be at least 2 characters")
    .max(64, "Code must be at most 64 characters"),
  title: z
    .string()
    .min(1, "Title must be at least 2 characters")
    .max(512, "Title must be at most 512 characters"),
  description: z.string(),
});

export type CreateProjectFormValues = z.infer<typeof createProjectSchema>;
