import { createContext } from 'react'

export type AuthContextValue = {
  accessToken: string | null
  isAuthenticated: boolean
  isBootstrapping: boolean
  signIn: (accessToken: string) => void
  setAccessToken: (accessToken: string) => void
  signOut: () => void
}

export const AuthContext = createContext<AuthContextValue | undefined>(
  undefined,
)
