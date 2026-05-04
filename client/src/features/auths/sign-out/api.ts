import { apiClient } from "@/lib";

export async function signOut(): Promise<void> {
  await apiClient.post("users/sign-out", {
    skipAuthRefresh: false,
  });
}
