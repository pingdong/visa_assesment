import { useMutation } from '@tanstack/react-query'
import { postPrice } from '../services/services'

export function usePostPrice() {
  return useMutation({ mutationFn: postPrice })
}
