type AuthStoreState = {
  accessToken: string | null
  isAuthenticated: boolean
  isBootstrapping: boolean
}

type AuthStoreListener = () => void

const listeners = new Set<AuthStoreListener>()

let state: AuthStoreState = {
  accessToken: null,
  isAuthenticated: false,
  isBootstrapping: true,
}

function emitChange() {
  listeners.forEach((listener) => listener())
}

export function subscribeToAuthStore(listener: AuthStoreListener) {
  listeners.add(listener)

  return () => {
    listeners.delete(listener)
  }
}

export function getAuthStoreState() {
  return state
}

export function setAuthStoreState(nextState: Partial<AuthStoreState>) {
  state = {
    ...state,
    ...nextState,
  }

  emitChange()
}

export function setAuthenticatedAccessToken(accessToken: string) {
  setAuthStoreState({
    accessToken,
    isAuthenticated: true,
    isBootstrapping: false,
  })
}

export function clearAuthStore() {
  setAuthStoreState({
    accessToken: null,
    isAuthenticated: false,
    isBootstrapping: false,
  })
}
