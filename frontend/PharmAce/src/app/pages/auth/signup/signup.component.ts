import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-signup',
  imports: [FormsModule , RouterLink],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss'
})
export class SignupComponent {
  name: string = '';
  email: string = '';
  password: string = '';
  phone: string = '';
  role: string = '';

  constructor(private router: Router, private authService: AuthService) {} 
  onSignupSubmit() {
    console.log('Signup Info:', { name: this.name, email: this.email, password: this.password, phone: this.phone, role: this.role });
    // Add your signup service logic here
    if (this.name && this.email && this.password && this.role) {
      // if (this.password !== this.ConfirmPassword) {
      //   alert('Passwords do not match!');
      //   return;
      // }

      this.authService.register({
        name: this.name,
        email: this.email,
        password: this.password,
        phoneNumber : this.phone,
        //confirmPassword: this.ConfirmPassword,
        role: this.role
      }).subscribe({
        next: (response) => {
          alert(response);
          this.router.navigate(['/login']);
        },
        error: (err) => {
          console.error("Signup error:", err);
          if (err.error && typeof err.error === 'object') {
            const messages = Object.values(err.error).flat();
            alert('Signup failed:\n' + messages.join('\n'));
          } else {
            alert('Signup failed: ' + (err.error || 'Something went wrong'));
          }
        }
      });
    } else {
      alert('Please fill in all fields');
    }
  }
}
