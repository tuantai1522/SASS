import { createFileRoute } from '@tanstack/react-router'
import { SignInPage } from '#/features/auth'

export const Route = createFileRoute('/_public/sign-in')({
  component: SignInRoute,
})

function SignInRoute() {
  return <SignInPage />
}
