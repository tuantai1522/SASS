import { useMutation } from '@tanstack/react-query'
import { toApiClientError } from '#/shared/api/api-error'
import type { SignInRequest } from './types'
import { signIn } from './api'
import { useAuth } from '../session'

export function useSignInMutation() {
  const { signIn: logIn } = useAuth()

  const mutation = useMutation({
    mutationFn: (request: SignInRequest) => signIn(request),
    onSuccess: (accessToken) => {
      logIn(accessToken)
    },
  })

  return {
    signIn: mutation.mutate,
    signInAsync: mutation.mutateAsync,
    isPending: mutation.isPending,
    errorMessage: mutation.error
      ? toApiClientError(mutation.error).detail
      : null,
    reset: mutation.reset,
  }
}
