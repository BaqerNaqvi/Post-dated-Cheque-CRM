import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class RequestInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const authorizedRequest = request.clone({
      setHeaders: {
        Authorization: 'Bearer '+sessionStorage.getItem("jwt")
      }
    });

    // Pass the modified request to the next interceptor or to the HTTP handler
    return next.handle(authorizedRequest);
  }
}
