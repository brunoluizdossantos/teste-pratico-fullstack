import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoadingService } from '../services/loading.service';
import { catchError, delay, finalize, Observable, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  token: string | null = null;
  
  constructor(
    private loadingService: LoadingService,
    private authService: AuthService
  ) {}

  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.loadingService.unavailable();

    this.token = this.authService.getToken();
    
    if (this.token) {
      req = req.clone({
        setHeaders: { Authorization: `Bearer ${this.token}` },
      });
    }
    
    return next.handle(req).pipe(
      delay(500),
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          this.authService.redirectToLogin();
        }
        return throwError(() => error);
      }),
      finalize(() => {
        this.loadingService.available();
      })
    );
  }
}