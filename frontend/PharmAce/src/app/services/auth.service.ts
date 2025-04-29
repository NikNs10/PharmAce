import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { appConfig } from '../config/app.config'; // Update path if needed
import { RegisterModel } from '../models/register.model';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = `${appConfig.apiUrl}/Authorize`;

  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/Login`, {
      email,
      password,
    });
  }
  
  register(data: RegisterModel): Observable<any> {
    console.log('Payload being sent:', JSON.stringify(data, null, 2));
    return this.http.post(`${this.apiUrl}/Sign_up`, data ,  { responseType: 'text' });
  }
  
  getUserRole(): string | null {
    const token = localStorage.getItem('token');
    if (!token) return null;
  
    try {
      const decodedToken = this.decodeToken(token);
      const currentTime = Date.now() / 1000; // in seconds
  
      // Check if token is expired
      if (decodedToken.exp < currentTime) {
        this.logout(); // Automatically logout if token is expired
        return null;
      }
  
      console.log('Decoded token payload:', decodedToken);
      return decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || null;
    } catch (e) {
      console.error('Error decoding token:', e);
      return null;
    }
  }
  
  private decodeToken(token: string): any {
    const parts = token.split('.');
    const payload = atob(parts[1]);
    return JSON.parse(payload);
  }
  
  
  
  

  saveToken(token: string): void {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  logout(): void {
    localStorage.removeItem('token');
  }
}