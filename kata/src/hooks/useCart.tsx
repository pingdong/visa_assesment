import { createContext, useContext, useState } from 'react'
import { SkuResponse } from '../types/sku-response'

export const CartContext = createContext<{
  items: SkuResponse[]
  addItem: (item: SkuResponse) => void
}>({ items: [], addItem: () => {} })

export function CartProvider({ children }: { children: React.ReactNode }) {
  const [items, setItems] = useState<SkuResponse[]>([])

  function addItem(item: SkuResponse) {
    setItems([...items, item])
  }

  return (
    <CartContext.Provider value={{ items, addItem }}>
      {children}
    </CartContext.Provider>
  )
}

export function useCart() {
  return useContext(CartContext)
}
