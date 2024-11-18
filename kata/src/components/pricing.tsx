
import { useResult } from "../hooks/useResult"

import "./pricing.css"

export default function PricingBoard() {
  const { result, message } = useResult()

  if (!result) return null

  return (
    <div className="result">
      <div>
        Total: {result?.total}
      </div>
      <div>
        Total Discount: {result?.totalDiscount}
      </div>
      
      {result?.items.map((item) => {
                return (
                  <table key={item.skuId}>
                    <thead>
                      <tr>
                        <th>SKU ID</th>
                        <th>Quantity</th>
                        <th>Unit Price</th>
                        <th>Amount</th>
                        <th>Applied Discount</th>
                        <th>Discount Description</th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr>
                        <td>{item.skuId}</td>
                        <td>{item.quantity}</td>
                        <td>{item.unitPrice}</td>
                        <td>{item.amount}</td>
                        <td>{item.appliedDiscount}</td>
                        <td>{item.discountDescription}</td>
                      </tr>
                    </tbody>
                  </table>
                )
              })}
    </div>
  )
}