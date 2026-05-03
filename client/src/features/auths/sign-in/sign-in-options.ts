import type { SignInRequest } from "./types.ts";
import { signIn } from "./api.ts";
import { mutationOptions } from "@tanstack/react-query";

export function signInOptions() {
  return mutationOptions({
    mutationFn: (request: SignInRequest) => signIn(request),
  });
}
