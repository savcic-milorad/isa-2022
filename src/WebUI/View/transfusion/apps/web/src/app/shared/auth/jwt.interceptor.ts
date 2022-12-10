import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthFacade } from './auth.facade.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private authFacade: AuthFacade) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    request = request.clone({
      headers: request.headers
        .append('Authorization', `Bearer ${this.authFacade.getAccessToken()}`)
    });

    return next.handle(request);
  }
}
