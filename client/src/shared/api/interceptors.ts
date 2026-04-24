import axios from 'axios'
import type {
  AxiosError,
  AxiosInstance,
  InternalAxiosRequestConfig,
} from 'axios'
import { env } from '#/shared/config/env'
import {
  clearAuthStore,
  getAuthStoreState,
  setAuthenticatedAccessToken,
} from '#/features/auth/features/session/index'
import { toApiClientError } from './api-error'

type RetryableRequestConfig = InternalAxiosRequestConfig & {
  _retry?: boolean
  skipAuthRefresh?: boolean
}

export function getAccessToken() {
  return getAuthStoreState().accessToken
}

export function setAccessToken(token: string | null) {
  if (!token) {
    clearAuthStore()
    return
  }

  setAuthenticatedAccessToken(token)
}

function withAuthorization(config: InternalAxiosRequestConfig) {
  const accessToken = getAccessToken()

  if (accessToken) {
    config.headers.set('Authorization', `Bearer ${accessToken}`)
  }

  return config
}

let refreshPromise: Promise<string> | null = null

async function requestRenewedAccessToken() {
  const response = await axios.get<{ token: string }>(
    `${env.apiBaseUrl}/users/renew-token`,
    {
      withCredentials: true,
      timeout: env.apiTimeoutMs,
    },
  )

  return response.data.token
}

async function renewAccessToken() {
  if (!refreshPromise) {
    refreshPromise = requestRenewedAccessToken()
      .then((token) => {
        setAuthenticatedAccessToken(token)
        return token
      })
      .catch((error: unknown) => {
        clearAuthStore()
        throw toApiClientError(error)
      })
      .finally(() => {
        refreshPromise = null
      })
  }

  return refreshPromise
}

async function retryUnauthorizedRequest(
  client: AxiosInstance,
  error: AxiosError,
) {
  const requestConfig = error.config as RetryableRequestConfig | undefined

  if (!requestConfig) {
    throw toApiClientError(error)
  }

  const shouldSkipRefresh = requestConfig.skipAuthRefresh === true
  const isUnauthorized = error.response?.status === 401

  if (!isUnauthorized || requestConfig._retry || shouldSkipRefresh) {
    throw toApiClientError(error)
  }

  requestConfig._retry = true

  const nextAccessToken = await renewAccessToken()
  requestConfig.headers.set('Authorization', `Bearer ${nextAccessToken}`)

  return client(requestConfig)
}

export function applyHttpInterceptors(client: AxiosInstance) {
  client.interceptors.request.use(withAuthorization)
  client.interceptors.response.use(
    (response) => response,
    async (error: unknown) => {
      if (axios.isAxiosError(error)) {
        try {
          return await retryUnauthorizedRequest(client, error)
        } catch (retryError) {
          return Promise.reject(retryError)
        }
      }

      return Promise.reject(toApiClientError(error))
    },
  )

  return client
}
