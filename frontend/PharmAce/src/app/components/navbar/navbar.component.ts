import { Component, EventEmitter, Output, OnInit, OnDestroy } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { CartService } from '../../services/cart.service';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { filter, Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit, OnDestroy {
  cartCount: number = 0;
  searchQuery: string = '';
  isLoginPage: boolean = false;
  isLoggedIn: boolean = false;
  UserId: string = '';
  private cartSubscription: Subscription | undefined;
  private routerSubscription: Subscription | undefined;

  @Output() searchPerformed = new EventEmitter<string>();

  
  constructor(
    private router: Router,
    private cartService: CartService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    // Subscribe to cart count changes
    this.UserId = this.authService.getUserIdFromToken()!;
    this.cartSubscription = this.cartService.cartCount$.subscribe(count => {
      this.cartCount = count;
    });

    // Watch for route changes
    this.routerSubscription = this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: any) => {
        this.isLoginPage = event.url.includes('/login');
        this.checkLoginStatus();
      });

    // Initial check
    this.checkLoginStatus();
  }

  ngOnDestroy(): void {
    // Clean up subscriptions
    if (this.cartSubscription) {
      this.cartSubscription.unsubscribe();
    }
    if (this.routerSubscription) {
      this.routerSubscription.unsubscribe();
    }
  }

  checkLoginStatus(): void {
    this.isLoggedIn = this.authService.isLoggedIn();
  }

  onLogin(): void {
    this.router.navigate(['/login']);
  }

  goHome(): void {
    this.router.navigate(['/']);
  }

  logout(): void {
    
    this.authService.logout();
    this.cartService.clearCart(this.UserId); // Clear cart on logout
    this.router.navigate(['/login']);
  }

  onSearch(): void {
    if (this.searchQuery.trim()) {
      this.searchPerformed.emit(this.searchQuery.trim());
      this.searchQuery = ''; // Optional: clear search after performing
    }
  }

  viewCart(): void {
    if (this.cartCount > 0) {
      this.router.navigate(['/cart']);
    }
  }

  viewOrders(): void {
    this.router.navigate(['/orders']);
  }
}