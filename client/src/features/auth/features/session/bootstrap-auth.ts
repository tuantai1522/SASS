import { renewAccessToken } from './api'
import {
  clearAuthStore,
  getAuthStoreState,
  setAuthenticatedAccessToken,
} from './auth-store'

let bootstrapPromise: Promise<ReturnType<typeof getAuthStoreState>> | null = null

export function ensureAuthReady() {
  const authState = getAuthStoreState()

  if (!authState.isBootstrapping) {
    return Promise.resolve(authState)
  }

  if (!bootstrapPromise) {
    bootstrapPromise = renewAccessToken()
      .then((accessToken) => {
        setAuthenticatedAccessToken(accessToken)
        return getAuthStoreState()
      })
      .catch(() => {
        clearAuthStore()
        return getAuthStoreState()
      })
      .finally(() => {
        bootstrapPromise = null
      })
  }

  return bootstrapPromise
}
