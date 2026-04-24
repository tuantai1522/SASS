import type { AxiosRequestConfig } from 'axios'

declare module 'axios' {
  export interface AxiosRequestConfig {
    skipAuthRefresh?: boolean
  }
}

export type ApiEnvelope<TData> = {
  data: TData
  message?: string
}

export interface ApiErrorPayload {
  detail: string
  status: number
  title: string
}

export type ApiRequestConfig = AxiosRequestConfig
