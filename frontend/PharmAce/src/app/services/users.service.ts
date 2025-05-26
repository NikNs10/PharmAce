import { Injectable } from '@angular/core';
import { appConfig } from '../config/app.config';
import { HttpClient } from '@angular/common/http';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';

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
}
