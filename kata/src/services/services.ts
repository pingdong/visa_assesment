import { PricePayload } from "../types/price-payload"

const base_url = 'http://localhost:5082'

// TODO: handle error

export async function getSkus() {
  const res = await fetch(`${base_url}/api/sku`)

  return await res.json()
}

export async function postPrice(payload: { items: PricePayload[] }) {
  const res = await fetch(`${base_url}/api/price`, {
    method: 'POST',
    body: JSON.stringify(payload),
    headers: {
      'Content-Type': 'application/json',
    }
  })

  return await res.json()
}
