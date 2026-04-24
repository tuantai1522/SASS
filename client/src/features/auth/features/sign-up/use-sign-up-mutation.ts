import { useMutation } from '@tanstack/react-query'
import { toApiClientError } from '#/shared/api/api-error'
import type { SignUpRequest } from './types'
import { signUp } from './api'

export function useSignUpMutation() {
  const mutation = useMutation({
    mutationFn: (request: SignUpRequest) => signUp(request),
  })

  return {
    signUp: mutation.mutate,
    signUpAsync: mutation.mutateAsync,
    isPending: mutation.isPending,
    errorMessage: mutation.error
      ? toApiClientError(mutation.error).detail
      : null,
  }
}
