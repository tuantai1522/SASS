import { apiClient } from "@/lib";
import type { GetMeResponse } from "./types.ts";

export async function getMe(): Promise<GetMeResponse> {
  const response = await apiClient.get<GetMeResponse>("users/me", {
    skipAuthRefresh: false,
  });
  return response.data;
}
