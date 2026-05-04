import { mutationOptions } from "@tanstack/react-query";
import { signOut } from "./api";

export function signOutOptions() {
  return mutationOptions({
    mutationFn: () => signOut(),
  });
}
