import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { IdentityApiService } from '../../../services/identity-api.service';
import { ValidationConstants } from '../../../core/constants/validation-constants';
import { Subscription } from 'rxjs';

@Component({
  standalone: false,
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnDestroy {
  email = '';
  password = '';

  errors: string[] = [];

  private loginSubscription: Subscription | undefined;

  constructor(private identityApi: IdentityApiService, private router: Router) {}

  ngOnDestroy(): void {
    if (this.loginSubscription) {
      this.loginSubscription.unsubscribe();
    }
  }

  isValid(): boolean {
    this.errors = [];

    if (!ValidationConstants.emailPattern.test(this.email)) {
      this.errors.push('Invalid email address');
    }
    if (!this.password) {
      this.errors.push('Password is required');
    }

    return Object.keys(this.errors).length === 0;
  }

  onLogin(): void {
    if (!this.isValid()) {
      return;
    }

    this.loginSubscription = this.identityApi.login({ email: this.email, password: this.password }).subscribe({
      next: (response) => {
        if (response?.token) {
          localStorage.setItem('jwt', response.token);
          this.router.navigate(['/usersList']);
        }
      },
      error: () => alert('Login failed.'),
    });
  }
}