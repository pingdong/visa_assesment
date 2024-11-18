export type PriceResponse = {
  total: number
  totalDiscount: number
  items: {
    unitPrice: number
    amount: number
    appliedDiscount?: number
    discountDescription?: string
    skuId: number
    quantity: number
  }[]
}