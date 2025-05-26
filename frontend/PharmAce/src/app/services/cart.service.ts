import { Injectable } from "@angular/core"
import { BehaviorSubject } from "rxjs"
import type { CartItem } from "../models/cart-item.model"
import type { Drug } from "../models/drug.model"
import { AuthService } from "./auth.service"

@Injectable({
  providedIn: "root",
})
export class CartService {
  private cartItemsSubject = new BehaviorSubject<CartItem[]>([])
  public cartItems$ = this.cartItemsSubject.asObservable()
  
  constructor(private authService: AuthService) {
    // Load cart from localStorage on service initialization
    const UserId=this.authService.getUserIdFromToken()!;
    this.loadCart(UserId)
    this.updateCartCount();
    
  }

  
  private getusercartkey(UserId:string | null){
    return `cart_${UserId}`;
  }
  

  private cartCountSubject = new BehaviorSubject<number>(0);
  cartCount$ = this.cartCountSubject.asObservable();

  private loadCart(UserId:string): void {
    const savedCart = localStorage.getItem(this.getusercartkey(UserId))
    if (savedCart) {
      this.cartItemsSubject.next(JSON.parse(savedCart))
      this.updateCartCount(); 
    }
  }

  private updateCartCount(): void {
    const newCount = this.getCartCount();
    this.cartCountSubject.next(newCount);
  }
  private saveCart(UserId:string): void {
    localStorage.setItem(this.getusercartkey(UserId), JSON.stringify(this.cartItemsSubject.value))
    this.updateCartCount();
  }

  getCartItems(): CartItem[] {
    return this.cartItemsSubject.value
  }

  addToCart(drug: Drug, quantity = 1 , UserId : string): void {
    //const UserId=this.authService.getUserIdFromToken()!;
    const currentCart = this.cartItemsSubject.value
    const existingItemIndex = currentCart.findIndex((item) => item.drug.drugId === drug.drugId)

    if (existingItemIndex !== -1) {
      // Item already exists, update quantity
      const updatedCart = [...currentCart]
      updatedCart[existingItemIndex].quantity += quantity
      this.cartItemsSubject.next(updatedCart)
    } else {
      // Add new item
      this.cartItemsSubject.next([...currentCart, { drug, quantity }])
    }
    this.updateCartCount();
    this.saveCart(UserId);
  }

  updateQuantity(drugId: string, quantity: number , UserId : string): void {
    
    const currentCart = this.cartItemsSubject.value
    const updatedCart = currentCart.map((item) => (item.drug.drugId === drugId ? { ...item, quantity } : item))

    this.cartItemsSubject.next(updatedCart);
    this.updateCartCount();
    this.saveCart(UserId);
  }

  removeFromCart(drugId: string , UserId : string): void {
    const currentCart = this.cartItemsSubject.value
    const updatedCart = currentCart.filter((item) => item.drug.drugId !== drugId)

    this.cartItemsSubject.next(updatedCart);
    this.updateCartCount();
    this.saveCart(UserId);
  }

  // clearCart(): void {
  //   this.cartItemsSubject.next([])
  //   localStorage.removeItem("cart")
  //   this.updateCartCount();
  // }
  clearCart(UserId:string): void {
    this.cartItemsSubject.next([])
    localStorage.removeItem(this.getusercartkey(UserId))
  }

  getCartTotal(): number {
    return this.cartItemsSubject.value.reduce((total, item) => total + item.drug.price * item.quantity, 0)
  }

  getCartCount(): number {
    return this.cartItemsSubject.value.reduce((count, item) => count + item.quantity, 0)
  }
}
