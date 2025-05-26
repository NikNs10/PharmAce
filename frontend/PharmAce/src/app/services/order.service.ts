import { Injectable } from "@angular/core"
import  { HttpClient } from "@angular/common/http"
import   { Observable } from "rxjs"
import   { Order } from "../models/order.model"
import   { OrderItem } from "../models/order-item.model"
import   { CartService } from "./cart.service"
import { appConfig } from "../config/app.config"


@Injectable({
  providedIn: "root",
})
export class OrderService {
  private apiUrl = `${appConfig.apiUrl}/Order`

  constructor(
    private http: HttpClient,
    private cartService: CartService,
  ) {}

  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.apiUrl)
  }

  getOrderById(id: string): Observable<Order> {
    return this.http.get<Order>(`${this.apiUrl}/${id}`)
  }

  getOrderItems(orderId: string): Observable<OrderItem[]> {
    return this.http.get<OrderItem[]>(`${appConfig.apiUrl}/orderitems/order/${orderId}`)
  }

  createOrder(paymentMethod: string): Observable<Order> {
    const cartItems = this.cartService.getCartItems()
    const totalAmount = this.cartService.getCartTotal()

    const orderData = {
      userId: "", // Will be set by the backend based on the authenticated user
      status: "Pending",
      orderDate: new Date(),
      totalAmount: totalAmount,
      paymentMethod: paymentMethod,
      transactionAmount: totalAmount,
      orderItems: cartItems.map((item) => ({
        drugId: item.drug.drugId,
        quantity: item.quantity,
      })),
    }

    return this.http.post<Order>(this.apiUrl, orderData)
  }
}
