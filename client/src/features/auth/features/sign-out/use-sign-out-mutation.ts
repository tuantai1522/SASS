import { useMutation, useQueryClient } from '@tanstack/react-query'
import { signOut } from './api'
import { useAuth } from '../session'

export function useSignOutMutation() {
  const { signOut: logout } = useAuth()
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: signOut,
    onSuccess: async () => {
      logout()
      queryClient.clear()
    },
    onError: async () => {
      logout()
      queryClient.clear()
    },
  })
}
