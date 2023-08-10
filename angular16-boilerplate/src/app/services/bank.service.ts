import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Bank } from '../shared/models/bank';
import { backendUrl } from '../shared/models/app.constants';

@Injectable({
  providedIn: 'root'
})
export class BankService {
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
  return this.http.get<Bank[]>(this.baseurl + '/api/bank');
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
