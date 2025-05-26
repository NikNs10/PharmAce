  import { Injectable } from "@angular/core"
  import {  HttpClient, HttpParams } from "@angular/common/http"
  import type { Observable } from "rxjs"
  import type { Drug } from "../models/drug.model"
  import { appConfig } from "../config/app.config";



  @Injectable({
    providedIn: "root",
  })
  export class DrugService {
    private apiUrl = `${appConfig.apiUrl}/Drug/filter`;

    constructor(private http: HttpClient) {}

    getDrugs(
      searchTerm = "",
      page = 1,
      pageSize = 10,
      sortBy = "Name",
      ascending = true,
      categoryId?: string,
    ): Observable<{ items: Drug[]; totalCount: number }> {
      let params = new HttpParams()
        .set("searchTerm", searchTerm)
        .set("page", page.toString())
        .set("pageSize", pageSize.toString())
        .set("sortBy", sortBy)
        .set("ascending", ascending.toString())

      if (categoryId) {
        params = params.set("categoryId", categoryId)
      }

      return this.http.get<{ items: Drug[]; totalCount: number }>(this.apiUrl, { params })
    }

    getDrugById(id: string): Observable<Drug> {
      return this.http.get<Drug>(`${this.apiUrl}/${id}`)
    }

  
  }
