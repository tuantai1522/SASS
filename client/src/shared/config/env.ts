type AppEnv = {
  apiBaseUrl: string
  apiTimeoutMs: number
}

function parseNumber(value: string | undefined, fallback: number) {
  if (!value) {
    return fallback
  }

  const parsedValue = Number(value)

  return Number.isFinite(parsedValue) ? parsedValue : fallback
}

export const env: AppEnv = {
  apiBaseUrl: import.meta.env.VITE_API_BASE_URL?.trim() || '/api',
  apiTimeoutMs: parseNumber(import.meta.env.VITE_API_TIMEOUT_MS, 10000),
}
