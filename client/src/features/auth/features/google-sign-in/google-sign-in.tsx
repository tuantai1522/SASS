import { Button } from '#/shared/ui'
import { useGoogleSignInMutation } from './use-google-sign-in-mutation'

export const GoogleSignIn = () => {
  const googleSignInMutation = useGoogleSignInMutation()

  return (
    <Button
      type="button"
      variant="ghost"
      disabled={googleSignInMutation.isPending}
      onClick={() => googleSignInMutation.mutate()}
    >
      {googleSignInMutation.isPending
        ? 'Opening Google...'
        : 'Continue with Google'}
    </Button>
  )
}
