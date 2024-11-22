import { Component, OnInit } from '@angular/core';

import { PageEvent } from '@angular/material/paginator';
import { UserApiService } from '../../../services/user-api.service';
import { GetUsersResponse } from '../../../models/user/get-users-response.model';
import { Observable } from 'rxjs';

@Component({
  standalone: false,
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent  implements OnInit {
  displayedColumns: string[] = ['email', 'country', 'province'];
  users: any[] = [];;
  pageSize = 10;
  pageIndex = 0;

  getUsersResponse$: Observable<GetUsersResponse> | undefined;

  constructor(private userApi: UserApiService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.getUsersResponse$ = this.userApi.getUsers(this.pageIndex, this.pageSize);
  }

  onPageChange(event: PageEvent): void {
    this.pageSize = event.pageSize;
    this.pageIndex = event.pageIndex;
    this.loadUsers();
  }
}
