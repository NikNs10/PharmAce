import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router , private snackbar : MatSnackBar) {}

  canActivate(): boolean {
    const role = this.authService.getUserRole();

    if (role === 'Admin') {
      return true;
    }

    //alert('Access denied. Admins only.');
    this.snackbar.open('Access denied. Admins only.', 'Close', { duration: 3000 });
    this.router.navigate(['/login']);
    return false;
  }
}
