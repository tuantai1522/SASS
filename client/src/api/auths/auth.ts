import { apiClient } from "@/lib/api-client/client.ts";
import type { RenewAccessTokenResponse } from "@/types/auths/auth.ts";

export async function renewAccessToken(): Promise<RenewAccessTokenResponse> {
  const response =
    await apiClient.post<RenewAccessTokenResponse>("/users/renew-token");
  return response.data;
}
