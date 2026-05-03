import type { RenewAccessTokenResponse } from "./types";
import { apiClient } from "@/lib";

export async function renewAccessToken(): Promise<RenewAccessTokenResponse> {
  const response = await apiClient.get<RenewAccessTokenResponse>(
    "/users/renew-token",
    {
      skipAuthRefresh: true,
    },
  );
  return response.data;
}
