import { useTheme } from '#/features/theme/use-theme'
import { Button } from '#/shared/ui'

const themeModes = [
  { value: 'light', label: 'Light' },
  { value: 'dark', label: 'Dark' },
  { value: 'system', label: 'System' },
] as const

export function ThemeToggle() {
  const { mode, setMode } = useTheme()

  return (
    <div
      className="inline-flex gap-1 rounded-full border border-[var(--color-border)] bg-[var(--color-panel)] p-1"
      aria-label="Theme switcher"
      role="group"
    >
      {themeModes.map((themeMode) => (
        <Button
          key={themeMode.value}
          variant="chip"
          active={themeMode.value === mode}
          onClick={() => setMode(themeMode.value)}
        >
          {themeMode.label}
        </Button>
      ))}
    </div>
  )
}
