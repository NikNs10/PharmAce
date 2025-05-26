import type { Drug } from "./drug.model"

export interface CartItem {
  drug: Drug
  quantity: number
}
