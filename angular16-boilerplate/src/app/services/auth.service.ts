import { Injectable } from '@angular/core';
import { backendUrl } from '../shared/models/app.constants';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, of, retry, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // Base url
  baseurl = backendUrl;
  // Http Headers
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);
  isAuthenticated$: Observable<boolean> = this.isAuthenticatedSubject.asObservable();

  constructor(private http: HttpClient) {
    const jwtToken = sessionStorage.getItem('jwt');
    this.isAuthenticatedSubject.next(!!jwtToken);
  }

  updateAuthenticationStatus(isAuthenticated: boolean): void {
    this.isAuthenticatedSubject.next(isAuthenticated);
  }

  Login(data: any): Observable<any> {
    return this.http.post<any>(
      `${this.baseurl}/api/auth/login`,
      JSON.stringify(data),
      this.httpOptions
    ).pipe(
      retry(0),
      catchError(this.handleError),
      tap(() => {
        // Update the authentication status upon successful login
        this.updateAuthenticationStatus(true);
      })
    );
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
