import { z } from "zod";

export const signInSchema = z.object({
  email: z.email("Email is invalid").min(1, "Email is required"),
  password: z.string().min(1, "Password is required"),
});

export type SignInFormValues = z.infer<typeof signInSchema>;
