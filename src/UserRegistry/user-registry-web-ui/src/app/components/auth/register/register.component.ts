import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { IdentityApiService } from '../../../services/identity-api.service';
import { LocationApiService } from '../../../services/location-api.service';
import { Observable, Subscription } from 'rxjs';
import { GetCountriesResponse } from '../../../models/location/get-countries-response.model';
import { GetProvincesResponse } from '../../../models/location/get-provinces-response.model';
import { ValidationConstants } from '../../../core/constants/validation-constants';

@Component({
  standalone: false,
  selector: 'app-registration',
  templateUrl: './register.component.html',
})
export class RegisterComponent implements OnInit, OnDestroy {
  step = 1;

  registrationForm = {
    email: '',
    password: '',
    confirmPassword: '',
    agreement: false,
    countryId: '',
    provinceId: ''
  };

  errors: string[] = [];

  getCountriesResponse$: Observable<GetCountriesResponse> | undefined;
  getProvincesResponse$: Observable<GetProvincesResponse> | undefined;
  private registerSubscription: Subscription | undefined;

  constructor(
    private identityApiService: IdentityApiService,
    private locationApiService: LocationApiService,
    private router: Router) {}

  ngOnInit(): void {
    this.getCountriesResponse$ = this.locationApiService.getCountries();
  }

  ngOnDestroy(): void {
    if (this.registerSubscription) {
      this.registerSubscription.unsubscribe();
    }
  }

  goToStep2(): void {
    if (this.isStep1Valid()) {
      this.step = 2;
    }
  }

  goToStep1(): void {
    this.step = 1;
  }

  onCountryChange(): void {
    const { countryId } = this.registrationForm;
    this.getProvincesResponse$ = this.locationApiService.getProvinces(countryId);
  }

  register(): void {
    if (this.isStep2valid()) {
      const { email, password, countryId, provinceId } = this.registrationForm;
      const data = { user: { email, password, countryId, provinceId } };
      this.registerSubscription = this.identityApiService.registerUser(data).subscribe({
        next: () => {
          alert('Registration successful!');
          this.router.navigate(['/login']);
        },
        error: (err) => {
            console.error(err);
            this.errors.push(err.error.detail);
        }
      });
    }
  }

  private isStep1Valid(): boolean {
    const { email, password, confirmPassword, agreement } = this.registrationForm;
    this.errors = [];

    if (!ValidationConstants.emailPattern.test(email)) {
      this.errors.push('Invalid email format.');
    }
    if (!password) {
      this.errors.push('Password is required.');
    }
    if (password !== confirmPassword) {
      this.errors.push('Passwords do not match.');
    }
    if (!agreement) {
      this.errors.push('You must agree to the terms.');
    }

    return this.errors.length === 0;
  }

  private isStep2valid(): boolean {
    const { countryId, provinceId } = this.registrationForm;
    this.errors = [];

    if (!countryId) {
      this.errors.push('Country is required.');
    }
    if (!provinceId) {
      this.errors.push('Province is required.');
    }

    return this.errors.length === 0;
  }
}