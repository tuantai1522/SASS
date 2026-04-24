import { useMutation } from '@tanstack/react-query'
import { getGoogleSignInLink } from './api'

export function useGoogleSignInMutation() {
  return useMutation({
    mutationFn: getGoogleSignInLink,
    onSuccess: (url) => {
      window.location.href = url
    },
  })
}
