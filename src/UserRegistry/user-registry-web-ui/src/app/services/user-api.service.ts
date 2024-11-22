import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetUsersResponse } from '../models/user/get-users-response.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UserApiService {
  private baseUrl = environment.userRegistryApiBaseUrl;

  constructor(private http: HttpClient) {}
  
  getUsers(pageIndex: number, pageSize: number): Observable<GetUsersResponse> {
    return this.http.get<GetUsersResponse>(`${this.baseUrl}/usermanagement-service/users?pageIndex=${pageIndex}&pagesize=${pageSize}`);
  }
}