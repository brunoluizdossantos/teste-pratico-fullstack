import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { LoginDto } from '../models/login.dto';
import { Token } from '../models/token';
import { RegisterDto } from '../models/register.dto';
import { UserPassword } from '../models/user.password';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl: string = environment.apiUrl;
  authenticated: boolean = false;

  constructor(
    private http: HttpClient,
    private routerService: Router
  ) { }

  login(dto: LoginDto): Observable<Token> {
    return this.http.post<Token>(`${this.baseUrl}/auth/login`, {
      email: dto.email,
      password: dto.password
    }).pipe(
      map((response: Token) => {
        if (response && response.token) {
          localStorage.setItem('token', response.token);
        }
        return response;
      })
    );
  }

  register(dto: RegisterDto): Observable<UserPassword> {
    return this.http.post<UserPassword>(`${this.baseUrl}/register/user`, {
      name: dto.name,
      email: dto.email
    }).pipe(
      map((response: UserPassword) => {
        return response;
      })
    );
  }

  logout() {
    localStorage.clear();
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  redirectToLogin() {
    this.logout();
    this.routerService.navigate(['/login']);
  }
}
