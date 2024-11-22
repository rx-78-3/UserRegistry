import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginResponse } from '../models/identity/login-response.model';
import { RegisterResponse } from '../models/identity/register-response.model';
import { environment } from '../../environments/environment';
import { LoginRequest } from '../models/identity/login-request.model';
import { RegisterUserRequest } from '../models/identity/register-user-request.model';

@Injectable({
  providedIn: 'root',
})
export class IdentityApiService {
  private baseUrl = environment.userRegistryApiBaseUrl;

  constructor(private http: HttpClient) {}

  registerUser(data: RegisterUserRequest): Observable<RegisterResponse> {
    return this.http.post<RegisterResponse>(`${this.baseUrl}/usermanagement-service/users`, data);
  }

  login(data: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.baseUrl}/identity-service/login`, data);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('jwt');
  }

  logout(): void {
    localStorage.removeItem('jwt');
  }
}