import { useNavigate } from '@tanstack/react-router'
import { Button } from '#/shared/ui'
import { useSignOutMutation } from './use-sign-out-mutation'

export function SignOut() {
  const signOutMutation = useSignOutMutation()
  const navigate = useNavigate()

  const handleSignOut = () => {
    signOutMutation.mutate(undefined, {
      // Always redirect to sign-in route
      onSettled: () => {
        navigate({ to: '/sign-in' })
      },
    })
  }

  return (
    <Button type="button" variant="ghost" onClick={handleSignOut}>
      Sign out
    </Button>
  )
}
