
import { useQuery } from '@tanstack/react-query'
import { SkuResponse } from '../types/sku-response'
import { getSkus } from '../services/services'

export function useSkus() {
  const { data, isLoading } = useQuery({ queryKey: ['skus'], queryFn: getSkus })

  return { skus: data as SkuResponse[], loading: isLoading }
}