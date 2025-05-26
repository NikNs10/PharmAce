import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MaterialModule } from '../../../material/material.module';
import { Router, RouterLink } from '@angular/router';
import { ApiService } from '../../../services/api.service';
import { OrderService } from '../../../services/order.service';
import   { Order } from "../../../models/order.model"

@Component({
  selector: 'app-orders',
  imports: [CommonModule , MaterialModule , RouterLink],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.scss'
})
export class OrdersComponent {
  display = [ 'orderId', 'orderDate', 'totalAmount', 'transactionId'];
  Orders: Order[]= [];

  constructor(
    private router: Router,
    private orderService: OrderService
  ) {  }
  ngOnInit(): void {
    this.getOrders();
  }
  
  getOrders(): void {
    this.orderService.getOrders().subscribe({
      next: (OrderDetails) => {
        this.Orders = OrderDetails
        .filter((order: any) => order.status === 0)
        .sort((a: any, b: any) => {
          return new Date(b.orderDate).getTime() - new Date(a.orderDate).getTime();
        });
      },
      error: () => {
        console.error('Error fetching Orders');
      }
    })
  }
 
}
