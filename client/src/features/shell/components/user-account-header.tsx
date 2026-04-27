import { useSuspenseQuery } from '@tanstack/react-query'
import { getMe } from '#/features/shell'
import { queryKeys } from '#/shared/lib'
import { Avatar } from '#/shared/ui'

export function UserAccountHeader() {
  const { data: me } = useSuspenseQuery({
    queryKey: queryKeys.auth.me(),
    queryFn: () => getMe(),
  })

  return (
    <div className="flex items-center gap-4">
      <Avatar name={me.displayName} src={me.avatarUrl} />
      <div>
        <p className="m-0 font-bold text-(--color-heading)">{me.displayName}</p>
      </div>
    </div>
  )
}
