import { cn, getAvatarFallback } from '#/shared/lib'

type AvatarProps = {
  name: string
  src?: string
  alt?: string
  className?: string
  fallbackClassName?: string
  imageClassName?: string
}

export function Avatar({
  name,
  src,
  alt,
  className,
  fallbackClassName,
  imageClassName,
}: AvatarProps) {
  const baseClassName = 'grid h-10 w-10 place-items-center rounded-full'

  if (src) {
    return (
      <img
        src={src}
        alt={alt ?? name}
        className={cn(baseClassName, 'object-cover', className, imageClassName)}
      />
    )
  }

  return (
    <div
      className={cn(
        baseClassName,
        'bg-(--color-accent-soft) font-bold text-(--color-accent-strong)',
        className,
        fallbackClassName,
      )}
      aria-hidden="true"
    >
      {getAvatarFallback(name)}
    </div>
  )
}
