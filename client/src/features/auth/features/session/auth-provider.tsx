import { useEffect, useMemo, useSyncExternalStore } from 'react'
import type { ReactNode } from 'react'
import { AuthContext } from './auth-context'
import type { AuthContextValue } from './auth-context'
import {
  clearAuthStore,
  getAuthStoreState,
  setAuthenticatedAccessToken,
  subscribeToAuthStore,
} from './auth-store'
import { ensureAuthReady } from './bootstrap-auth'

export function AuthProvider({ children }: { children: ReactNode }) {
  const authState = useSyncExternalStore(
    subscribeToAuthStore,
    getAuthStoreState,
  )

  // I want to know current user state in application
  useEffect(() => {
    void ensureAuthReady()
  }, [])

  const value = useMemo<AuthContextValue>(
    () => ({
      accessToken: authState.accessToken,
      isAuthenticated: authState.isAuthenticated,
      isBootstrapping: authState.isBootstrapping,
      signIn: (accessToken: string) => {
        setAuthenticatedAccessToken(accessToken)
      },
      setAccessToken: setAuthenticatedAccessToken,
      signOut: () => {
        clearAuthStore()
      },
    }),
    [
      authState.accessToken,
      authState.isAuthenticated,
      authState.isBootstrapping,
    ],
  )

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
}
