import { type ClassValue, clsx } from "clsx";
import { twMerge } from "tailwind-merge";

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

export function normalizedName(name: string) {
  return name.trim().toLowerCase().replace(/\s+/g, "-").replace(/-+/g, "-");
}

export function formatDate(
  unixSeconds: number,
  locale: string = navigator.language,
): string {
  const date = new Date(unixSeconds * 1000);
  const now = new Date();

  const diffSeconds = Math.floor((now.getTime() - date.getTime()) / 1000);
  const diffMinutes = Math.floor(diffSeconds / 60);

  const rtf = new Intl.RelativeTimeFormat(locale, {
    numeric: "auto",
  });

  const timeText = new Intl.DateTimeFormat(locale, {
    hour: "2-digit",
    minute: "2-digit",
  }).format(date);

  const isSameDay =
    date.getFullYear() === now.getFullYear() &&
    date.getMonth() === now.getMonth() &&
    date.getDate() === now.getDate();

  const yesterday = new Date(now);
  yesterday.setDate(now.getDate() - 1);

  const isYesterday =
    date.getFullYear() === yesterday.getFullYear() &&
    date.getMonth() === yesterday.getMonth() &&
    date.getDate() === yesterday.getDate();

  if (diffSeconds >= 0 && diffSeconds < 60) {
    return rtf.format(-diffSeconds, "second");
  }

  if (diffMinutes >= 0 && diffMinutes < 60) {
    return rtf.format(-diffMinutes, "minute");
  }

  if (isSameDay) {
    return `${rtf.format(0, "day")} ${timeText}`;
  }

  if (isYesterday) {
    return `${rtf.format(-1, "day")} ${timeText}`;
  }

  const dateText = new Intl.DateTimeFormat(locale, {
    day: "2-digit",
    month: "short",
    year: date.getFullYear() === now.getFullYear() ? undefined : "numeric",
  }).format(date);

  return `${dateText}, ${timeText}`;
}
