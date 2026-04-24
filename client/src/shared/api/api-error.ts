import { isAxiosError } from 'axios'
import type { ApiErrorPayload } from './types'

export class ApiClientError extends Error implements ApiErrorPayload {
  constructor(
    public readonly detail: string,
    public readonly status: number,
    public readonly title: string,
  ) {
    super(detail)
    this.name = 'ApiClientError'
  }

  static fromPayload(payload: ApiErrorPayload) {
    return new ApiClientError(payload.detail, payload.status, payload.title)
  }
}

export function toApiClientError(error: unknown): ApiClientError {
  if (error instanceof ApiClientError) {
    return error
  }

  if (isAxiosError<ApiErrorPayload>(error)) {
    const payload = error.response?.data

    return ApiClientError.fromPayload({
      detail: payload?.detail ?? 'Unknown request error',
      status: payload?.status ?? error.response?.status ?? 0,
      title: payload?.title ?? 'Request error',
    })
  }

  if (error instanceof Error) {
    return new ApiClientError(error.message, 0, 'Unexpected error')
  }

  return new ApiClientError('Unknown request error', 0, 'Unknown error')
}
