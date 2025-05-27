import { Injectable } from '@angular/core';
import { appConfig } from '../config/app.config';
import { HttpClient } from '@angular/common/http';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { Users } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private url = `${appConfig.apiUrl}/User/ViewAll`;

  constructor(
    private http: HttpClient,
    private apiService: ApiService,
  ) {  }

  getAllUsers(): Observable<any> {
    return this.http.get<any>(this.url);
    
  }

  // updateUsers(userId: string): Observable<any> {
  //   const url = `${appConfig.apiUrl}/User/Edit-User/${userId}`;
  //   return this.http.put(url, userId);
  // }
  updateUsers(user: Users): Observable<any> {
    return this.http.put(`${appConfig.apiUrl}/User/Edit-User/${user.userId}`, user);
  }

  deleteUser(userId: string): Observable<any> {
    return this.http.delete(`${appConfig.apiUrl}/User/Delete-User/${userId}`);
  }

  addUser(user: Users): Observable<any> {
    return this.http.post(`${appConfig.apiUrl}/User/Create-User`, user);
  }
  
}
