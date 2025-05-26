import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { appConfig } from '../config/app.config'; // Update path if needed
import { RegisterModel } from '../models/register.model';
import { jwtDecode, JwtPayload } from 'jwt-decode'; // Ensure you have jwt-decode installed
import { User } from '../models/user.model';

interface CustomJwtPayload extends JwtPayload {
  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'?: string;
  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'?: string;
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'?: string;
  name?: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private currentUserSubject = new BehaviorSubject<User | null>(null)
  public currentUser$ = this.currentUserSubject.asObservable()
  private apiUrl = `${appConfig.apiUrl}/Authorize`;
  private isAuthenticated = false;

  constructor(private http: HttpClient) {
    this.checkAuthState();
  }
  

  public get currentUserValue(): User | null {
    return this.currentUserSubject.value
  }

  // login(email: string, password: string): Observable<{ token: string }> {
  //   this.isAuthenticated = true;
  //   localStorage.setItem('isAuthenticated', 'true');
    
  //   return this.http.post<{ token: string }>(`${this.apiUrl}/Login`, {
  //     email,
  //     password,
  //   });

  // }

  login(email: string, password: string): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/Login`, { email, password }).pipe(
      tap(response => {
        const user = this.createUserFromToken(response.token);
        this.currentUserSubject.next(user);
        localStorage.setItem('currentUser', JSON.stringify(user));
        localStorage.setItem('token', response.token);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }


  private createUserFromToken(token: string): User {
    const decoded = jwtDecode<CustomJwtPayload>(token);
    return {
      id: decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] || null,
      name: decoded.name || '',
      email: decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'] || '',
      role: decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || 'User',
      token: token
    };
  }

  
  register(data: RegisterModel): Observable<any> {
    console.log('Payload being sent:', JSON.stringify(data, null, 2));
    return this.http.post(`${this.apiUrl}/Sign_up`, data ,  { responseType: 'text' });
  }

  

  // logouts(): void {
  //   // Remove user from local storage and set current user to null
  //   this.isAuthenticated = false;
  //   localStorage.removeItem('isAuthenticated');
  //   localStorage.removeItem("currentUser")
  //   this.currentUserSubject.next(null)
  // }
  
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

  private checkAuthState(): void {
    const token = this.getToken();
    if (token) {
      try {
        const user = this.createUserFromToken(token);
        this.currentUserSubject.next(user);
      } catch (e) {
        this.logout();
      }
    }
  } 

  isLoggedIn(): boolean {
    return !!this.currentUserValue;
  }
  
  private decodeToken(token: string): any {
    const parts = token.split('.');
    const payload = atob(parts[1]);
    return JSON.parse(payload);
  }
  
  
  getUserIdFromToken(): string | null {
    const token = this.getToken();
    if (!token) return null;
  
    try {
      const decoded = jwtDecode<{ 
        [key: string]: any;
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"?: string 
      }>(token);
      
      return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"] || null;
    } catch (err) {
      return null;
    }
  }
  

  saveToken(token: string): void {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // logout(): void {
  //   localStorage.removeItem('token');
  // }
}


