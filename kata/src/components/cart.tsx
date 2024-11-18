import { useEffect } from "react"
import { useCart } from "../hooks/useCart"
import { useResult } from "../hooks/useResult"
import { SkuResponse } from "../types/sku-response"
import { PricePayload } from "../types/price-payload"

import "./cart.css"
import { usePostPrice } from "../hooks/usePostPrice"

export default function ShoppingCart() {
  const { items } = useCart()
  const { mutate: getPrice } = usePostPrice()
  const { setResult } = useResult()
  
  useEffect(() => {
    (async () => {
      const itemToSend = toPayload(items)
      if (itemToSend.items.length === 0) return

      getPrice(itemToSend, {
        onSuccess: (data) => {
          setResult(data)
        },
      })
    })()
  }, [items, getPrice, setResult])

  if (items.length === 0) return <div className="right content">Cart is empty</div>

  return (
    <div className="right content">
      {items.map((item, idx) => (
        <div key={idx}>{item.name}</div>
      ))}
    </div>
  )
}

function toPayload(items: SkuResponse[]) {
  const itemToSend = items.reduce((acc, curr) => {
    const quantity = acc[curr.id]?.quantity || 0

    acc[curr.id] = {
      skuId: curr.id,
      quantity: quantity + 1,
    }
    
    return acc
  }, {} as Record<number, PricePayload>)

  return { items: Object.values(itemToSend) }
}
