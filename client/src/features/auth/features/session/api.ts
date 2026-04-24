import { httpClient } from "#/shared/api/http-client"
import type { RenewAccessTokenResponse } from "./types"

export async function renewAccessToken() {
  const response = await httpClient.get<RenewAccessTokenResponse>(
    '/users/renew-token',
    {
      skipAuthRefresh: true,
    },
  )

  return response.data.token
}
