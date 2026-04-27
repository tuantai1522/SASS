export function getAvatarFallback(displayName: string) {
  const trimmedName = displayName.trim()

  return trimmedName.slice(0, 2).toUpperCase()
}
