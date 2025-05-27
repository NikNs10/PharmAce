// import { Component } from '@angular/core';
// import { FormsModule } from '@angular/forms';

// @Component({
//   selector: 'app-login',
//   imports: [FormsModule],
//   templateUrl: './login.component.html',
//   styleUrl: './login.component.scss'
// })
// export class LoginComponent {
//   email: string = '';
//   password: string = '';
//   role: string = '';

//   onLoginSubmit() {
//     console.log('Login Info:', { email: this.email, password: this.password, role: this.role });
//     // Add your login service logic here
//   }
// }


import { Component } from '@angular/core';

import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [ FormsModule , RouterLink],
})
export class LoginComponent {
role: any;
onLoginSubmit() {
throw new Error('Method not implemented.');
}
  email: string = '';
  password: string = '';

  constructor(private router: Router, private authService: AuthService , private snackbar : MatSnackBar) {}

  onLogin() {
    if (this.email && this.password) {
      this.authService.login(this.email, this.password).subscribe({
        next: (res) => {
          this.authService.saveToken(res.token);
          //alert('Login successful!');
          this.snackbar.open('Login successful!', 'Close', { duration: 3000 });
          console.log('Login successful!');
  
          const role = this.authService.getUserRole();
          console.log('User role after login:', role);
          
          if (role === 'Supplier') {
            this.router.navigate(['/supplier/dashboard']);
          } else if (role === 'Admin') {
            this.router.navigate(['/admin/dashboard']); // default: Admin/home
          } else if(role === 'Doctor') {
            this.router.navigate(['/']); // default: Doctor/home
          }


        },
        error: (err) => {
          console.error(err);
          // alert('Invalid credentials');
          this.snackbar.open('Invalid credentials. Please try again.', 'Close', { duration: 3000 });    
          this.email = '';
          this.password = '';
        }
      });
    } else {
      this.snackbar.open('Please enter email and password.', 'Close', { duration: 3000 });
      this.email = '';
      this.password = '';
    }
  }
  

  navigateToSignup() {
    this.router.navigate(['/signup']); 
  }
}