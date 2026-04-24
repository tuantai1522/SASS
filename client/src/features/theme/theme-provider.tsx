import { useEffect, useMemo, useState } from 'react'
import type { ReactNode } from 'react'
import { ThemeContext } from './theme-context'
import type {
  ResolvedTheme,
  ThemeContextValue,
  ThemeMode,
} from './theme-context'

const THEME_STORAGE_KEY = 'sass-client-theme'

function getSystemTheme(): ResolvedTheme {
  if (typeof window === 'undefined') {
    return 'light'
  }

  return window.matchMedia('(prefers-color-scheme: dark)').matches
    ? 'dark'
    : 'light'
}

function getInitialMode(): ThemeMode {
  if (typeof window === 'undefined') {
    return 'system'
  }

  const storedValue = window.localStorage.getItem(THEME_STORAGE_KEY)

  if (
    storedValue === 'light' ||
    storedValue === 'dark' ||
    storedValue === 'system'
  ) {
    return storedValue
  }

  return 'system'
}

export function ThemeProvider({ children }: { children: ReactNode }) {
  const [mode, setModeState] = useState<ThemeMode>(getInitialMode)
  const [systemTheme, setSystemTheme] = useState<ResolvedTheme>(getSystemTheme)

  const resolvedTheme = mode === 'system' ? systemTheme : mode

  useEffect(() => {
    if (typeof window === 'undefined') {
      return undefined
    }

    const mediaQuery = window.matchMedia('(prefers-color-scheme: dark)')

    const handleChange = () => {
      setSystemTheme(mediaQuery.matches ? 'dark' : 'light')
    }

    mediaQuery.addEventListener('change', handleChange)

    return () => {
      mediaQuery.removeEventListener('change', handleChange)
    }
  }, [])

  useEffect(() => {
    if (typeof document === 'undefined') {
      return
    }

    document.documentElement.dataset.theme = resolvedTheme
    document.documentElement.style.colorScheme = resolvedTheme
  }, [resolvedTheme])

  const value = useMemo<ThemeContextValue>(
    () => ({
      mode,
      resolvedTheme,
      setMode: (nextMode: ThemeMode) => {
        if (typeof window !== 'undefined') {
          window.localStorage.setItem(THEME_STORAGE_KEY, nextMode)
        }

        setModeState(nextMode)
      },
    }),
    [mode, resolvedTheme],
  )

  return <ThemeContext.Provider value={value}>{children}</ThemeContext.Provider>
}
