import { createContext, useContext, useState } from 'react'
import { PriceResponse } from '../types/price-response'

export const ResultContext = createContext<{
  result?: PriceResponse | null
  setResult: (result: PriceResponse) => void
}>({ result: null, setResult: () => {} })

export function ResultProvider({ children }: { children: React.ReactNode }) {
  const [result, setResult] = useState<PriceResponse>()

  return (
    <ResultContext.Provider value={{ result, setResult }}>
      {children}
    </ResultContext.Provider>
  )
}

export function useResult() {
  return useContext(ResultContext)
}
