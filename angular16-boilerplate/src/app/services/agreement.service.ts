import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Agreement } from '../shared/models/agreement';
import { backendUrl } from '../shared/models/app.constants';
@Injectable({
  providedIn: 'root'
})
export class AgreementService {
  // Base url
  baseurl = backendUrl;
  // Http Headers
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(private http: HttpClient) { }


  // POST
  Search(filters: any): Observable<[Agreement]> {
    return this.http.post<[Agreement]>(
      this.baseurl + '/api/agreement/search',
      filters
    ).pipe(retry(1), catchError(this.handleError));
  }

  GetByCompanyId(compId: number) {
    return this.http.get<Agreement[]>(this.baseurl + '/api/agreement/company/' + compId);
  }

  GetById(id: number) {
    return this.http.get<Agreement[]>(this.baseurl + '/api/agreement/' + id);
  }

  GetAll() {
    return this.http.get<Agreement[]>(this.baseurl + '/api/agreement');
  }

  // POST
  Create(data: any): Observable<any> {
    return this.http.post<any>
      (
        this.baseurl + '/api/agreement/add',
        JSON.stringify(data),
        this.httpOptions
      )
      .pipe(retry(0), catchError(this.handleError));
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
