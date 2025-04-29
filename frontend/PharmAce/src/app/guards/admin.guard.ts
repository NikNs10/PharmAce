import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {
    const role = this.authService.getUserRole();

    if (role === 'Admin') {
      return true;
    }

    alert('Access denied. Admins only.');
    this.router.navigate(['/login']);
    return false;
  }
}
