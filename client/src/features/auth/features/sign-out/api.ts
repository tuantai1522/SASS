import { httpClient } from '#/shared/api/http-client'

export async function signOut() {
  await httpClient.post(
    '/users/sign-out',
    {},
    {
      skipAuthRefresh: true,
    },
  )
}
