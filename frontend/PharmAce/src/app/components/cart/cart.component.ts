import { Component,  OnInit } from "@angular/core"
import  { Router } from "@angular/router"
import  { CartService } from "../../services/cart.service"
import  { CartItem } from "../../models/cart-item.model"
import  { MatSnackBar } from "@angular/material/snack-bar"
import { CommonModule } from "@angular/common"
import { MaterialModule } from "../../material/material.module"
import { AuthService } from "../../services/auth.service"

@Component({
  selector: "app-cart",
  templateUrl: "./cart.component.html",
  styleUrls: ["./cart.component.scss"],
  standalone: true,
  imports: [CommonModule , MaterialModule],
})
export class CartComponent implements OnInit {
  cartItems: CartItem[] = []
  displayedColumns: string[] = ["image", "name", "price", "quantity", "total", "actions"]
  UserId:string ='';

  constructor(
    private cartService: CartService,
    private router: Router,
    private snackBar: MatSnackBar,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.UserId=this.authService.getUserIdFromToken()!;
    this.cartService.cartItems$.subscribe((items) => {
      this.cartItems = items
    })
  }

  updateQuantity(item: CartItem, change: number): void {
    const newQuantity = item.quantity + change

    if (newQuantity <= 0) {
      this.removeItem(item)
      return
    }

    if (newQuantity > item.drug.stock) {
      this.snackBar.open(`Sorry, only ${item.drug.stock} units available in stock.`, "Close", {
        duration: 3000,
      })
      return
    }

    this.cartService.updateQuantity(item.drug.drugId, newQuantity , this.UserId)
  }

  removeItem(item: CartItem ): void {
    this.cartService.removeFromCart(item.drug.drugId , this.UserId)
    this.snackBar.open(`${item.drug.name} removed from cart`, "Close", {
      duration: 3000,
    })
  }

  clearCart(): void {
    this.cartService.clearCart(this.UserId)
    this.snackBar.open("Cart cleared", "Close", {
      duration: 3000,
    })
  }

  getItemTotal(item: CartItem): number {
    return item.drug.price * item.quantity
  }

  getCartTotal(): number {
    return this.cartService.getCartTotal()
  }

  checkout(): void {
    if (this.cartItems.length === 0) {
      this.snackBar.open("Your cart is empty", "Close", {
        duration: 3000,
      })
      return
    }

    this.router.navigate(["/checkout"])
  }

  continueShopping(): void {
    this.router.navigate(["/"])
  }
}
