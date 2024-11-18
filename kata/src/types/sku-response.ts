export type SkuResponse = {
  id: number
  name: string
  items: {
    productId: number
    quantity: number
  }[]
  unitPrice: number
}