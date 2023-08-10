import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Payment } from '../shared/models/payment';
import { backendUrl } from '../shared/models/app.constants';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
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
    deletePayment(id: number): Observable<void> {
      const url = `${this.baseurl}/api/payment/delete/${id}`;
      return this.http.delete<void>(url).pipe(
        retry(0),
        catchError(this.handleError)
      );
    }

     // POST
     Search(filters: any): Observable<[Payment]> {
      return this.http.post<[Payment]>(
        this.baseurl + '/api/payment/search',
        filters
      ).pipe(retry(0), catchError(this.handleError));
    }

    // POST
  Import(xlsFile: File): Observable<any> {
    const formData = new FormData();
    if (xlsFile) {
      formData.append('file', xlsFile);
    }
    return this.http.post<any>(
      this.baseurl + '/api/payment/import',
      formData
    ).pipe(retry(0), catchError(this.handleError));
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
