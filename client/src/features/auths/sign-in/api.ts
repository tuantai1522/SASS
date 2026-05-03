import { apiClient } from "@/lib";
import type { SignInRequest, SignInResponse } from "./types";

export async function signIn(request: SignInRequest): Promise<SignInResponse> {
  const response = await apiClient.post<SignInResponse>(
    "/users/sign-in",
    request,
    {
      skipAuthRefresh: true,
    },
  );
  return response.data;
}
