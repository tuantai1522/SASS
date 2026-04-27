import { useState, useEffect } from 'react'

export function useDebounced<T>(value: T, delay: number) {
  const [debouncedValue, setDebouncedValue] = useState(value)

  useEffect(() => {
    const timeoutId = window.setTimeout(() => {
      setDebouncedValue(value)
    }, delay)

    return () => {
      window.clearTimeout(timeoutId)
    }
  }, [value, delay])

  return debouncedValue
}
