import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Company } from '../shared/models/company';
import { backendUrl } from '../shared/constants/app-constants';
@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  // Base url
  baseurl = backendUrl;
  // Http Headers
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(private http: HttpClient) { }


  GetAll() {
    return this.http.get<Company[]>(this.baseurl + '/api/company');
  }

  GetById(id: number) {
    return this.http.get<Company[]>(this.baseurl + '/api/company/' + id);
  }

  // POST
  Create(data: any): Observable<any> {
    console.log("company-payload:", data);
    return this.http.post<any>
      (
        this.baseurl + '/api/company/add',
        JSON.stringify(data),
        this.httpOptions
      )
      .pipe(retry(0), catchError(this.handleError));
  }

  // POST
  Search(filters: any): Observable<[Company]> {
    return this.http.post<[Company]>(
      this.baseurl + '/api/company/search',
      filters
    ).pipe(retry(1), catchError(this.handleError));
  }
  private handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(() => {
      return errorMessage;
    });
  }
}
