import { apiClient } from "@/lib/api-client/client.ts";
import type {
  RenewAccessTokenResponse,
  SignInRequest,
  SignInResponse,
} from "@/types/auths/auth.ts";

export async function renewAccessToken(): Promise<RenewAccessTokenResponse> {
  const response =
    await apiClient.get<RenewAccessTokenResponse>("/users/renew-token");
  return response.data;
}

export async function signIn(request: SignInRequest): Promise<SignInResponse> {
  const response = await apiClient.post<SignInResponse>(
    "/users/sign-in",
    request,
  );
  return response.data;
}
