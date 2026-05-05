import { type ClassValue, clsx } from "clsx";
import { twMerge } from "tailwind-merge";

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

export function normalizedName(name: string) {
  return name.trim().toLowerCase().replace(/\s+/g, "-").replace(/-+/g, "-");
}
