import { Injectable } from '@angular/core';
import { appConfig } from '../config/app.config';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { CategoryDto } from '../models/categoryDto.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = appConfig.apiUrl;
  constructor(private http : HttpClient , private authService : AuthService) { }

  // GET
  get<T>(endpoint : string) : Observable<T> {
    return this.http.get<T>(`${this.apiUrl}/${endpoint}`);
  }

  post<T>(endpoint: string, data: any): Observable<T> {
    return this.http.post<T>(`${this.apiUrl}/${endpoint}`, data);
  }

  // Generic PUT request
  put<T>(endpoint: string, data: any): Observable<T> {
    return this.http.put<T>(`${this.apiUrl}/${endpoint}`, data);
  }

  // Generic DELETE request
  delete<T>(endpoint: string): Observable<T> {
    return this.http.delete<T>(`${this.apiUrl}/${endpoint}`);
  }

  // Delete drug by Id
  deleteDrug(id: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/Drug/Delete-Drug/${id}`);
  }

  // Delete by name
  deleteDrugByName(name: string): Observable<any> {
    return this.http.delete<any>(
      `${this.apiUrl}/Drug/Delete_drug/${encodeURIComponent(name)}`
    );
  }
  
  GetuserId(): string | null {
    const userId = this.authService.getUserIdFromToken();
    return userId;
  }

  addDrug(drug: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Drug/Add-drugs`, drug);
  }

  addDrugInInventory(drug: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Inventory`, drug);
  }

  // Update existing drug
  // updateDrug( drug: any): Observable<any> {
  //   return this.http.put<any>(`${this.apiUrl}/Drug/Edit_drug`, drug);
  // }
  

  getCategories() {
    return this.http.get<CategoryDto[]>(`${this.apiUrl}/Category/Get-Category`);
  }


  getFilteredDrugs(searchTerm: string = '', page: number = 1, pageSize: number = 10): Observable<any> {
    const url = `${this.apiUrl}/Drug/filter?searchTerm=${searchTerm}&page=${page}&pageSize=${pageSize}`;
    return this.http.get<any>(url);
  }

  updateDrug(drug: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/Drug/edit-drug`, drug);
  }

  updateDrugInInventory(drug: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/Inventory`, drug);
  }
  getDrugs(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/Drug/View-drugs`);
  }

  
  getOrderCount(): Observable<number> {
    return this.http
      .get<number>(`${this.apiUrl}/Order/count`);
  }

  getUsers(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/User/ViewAll`);
  }
}
