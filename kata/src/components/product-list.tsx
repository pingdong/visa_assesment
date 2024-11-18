import { useCart } from "../hooks/useCart"
import { useSkus } from "../hooks/useSkus"

import "./product-list.css"

export default function ProductList() {
  const { skus, loading } = useSkus()
  const { addItem } = useCart()
  
  if (loading) return <div>Loading...</div>

  return (
    <>
      {skus.map((sku) => (
        <div key={sku.id} className="sku-item">
          <div className="name">{sku.name}</div>
          <div className="add" onClick={() => addItem(sku)}>add</div>
        </div>
      ))}
    </>
  )
}
