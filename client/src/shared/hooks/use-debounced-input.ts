import { useEffect, useState } from 'react'

import { useDebounced } from './use-debounced.ts'

type UseDebouncedInputOptions = {
  value: string
  delay?: number
  onDebouncedChange: (value: string) => void
}

export function useDebouncedInput({
  value,
  delay = 1000,
  onDebouncedChange,
}: UseDebouncedInputOptions) {
  const [inputValue, setInputValue] = useState(value)
  const debouncedValue = useDebounced(inputValue, delay)

  useEffect(() => {
    setInputValue(value)
  }, [value])

  useEffect(() => {
    if (debouncedValue === value) {
      return
    }

    onDebouncedChange(debouncedValue)
  }, [debouncedValue, onDebouncedChange, value])

  return {
    value: inputValue,
    setValue: setInputValue,
  }
}
