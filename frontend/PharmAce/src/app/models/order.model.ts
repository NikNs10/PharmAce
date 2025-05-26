export enum OrderStatus {
    Pending = "Pending",
    Verified = "Verified",
  }
  
  export interface Order {
    orderId: string
    userId: string
    status: OrderStatus
    orderDate: Date
    totalAmount: number
    transactionId?: string
  }
  