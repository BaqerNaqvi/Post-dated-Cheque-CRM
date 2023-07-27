import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Agreement } from '../shared/models/agreement';
@Injectable({
  providedIn: 'root'
})
export class AgreementService {
  // Base url
  baseurl = 'https://localhost:7183';
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
    return this.http.get<Agreement[]>(this.baseurl + '/api/agreement/' + compId);
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