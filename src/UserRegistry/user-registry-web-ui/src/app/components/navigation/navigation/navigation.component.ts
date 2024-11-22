import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { IdentityApiService } from '../../../services/identity-api.service';

@Component({
  standalone: false,
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
})
export class NavigationComponent {
  constructor(public identityService: IdentityApiService, private router: Router) {}

  logout(): void {
    this.identityService.logout();
    this.router.navigate(['/login']); 
  }
}
