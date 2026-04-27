export function formatUnixSecondsToDate(value: number,  locale?: Intl.LocalesArgument,): string {
  return new Intl.DateTimeFormat(locale, {
    year: 'numeric',
    month: 'short',
    day: '2-digit',
  }).format(new Date(value * 1000))
}
