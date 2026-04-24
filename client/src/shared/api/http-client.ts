import axios from 'axios'
import { env } from '#/shared/config/env'
import { applyHttpInterceptors } from './interceptors'

export const httpClient = applyHttpInterceptors(
  axios.create({
    baseURL: env.apiBaseUrl,
    timeout: env.apiTimeoutMs,
    withCredentials: true,
  }),
)
