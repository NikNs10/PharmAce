import { Component,   OnInit } from "@angular/core"
import   { Router, RouterLink } from "@angular/router"
import   { Observable } from "rxjs"
import   { AuthService } from "../../services/auth.service"
import   { CartService } from "../../services/cart.service"
import   { User } from "../../models/user.model"

@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.scss"],
  standalone: true,
  imports: [CommonModule , MaterialModule , RouterLink],
})
export class HeaderComponent implements OnInit {
  currentUser$: Observable<User | null>
  cartItemCount = 0
  isMobileMenuOpen = false

  constructor(
    private authService: AuthService,
    private cartService: CartService,
    private router: Router,
  ) {
    this.currentUser$ = this.authService.currentUser$
  }

  ngOnInit(): void {
    this.cartService.cartItems$.subscribe(() => {
      this.cartItemCount = this.cartService.getCartCount()
    })
  }

  logout(): void {
    this.authService.logout()
    this.router.navigate(["/"])
  }

  toggleMobileMenu(): void {
    this.isMobileMenuOpen = !this.isMobileMenuOpen
  }
}
import { CommonModule } from "@angular/common"
import { MaterialModule } from "../../material/material.module"
