import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetCountriesResponse } from '../models/location/get-countries-response.model';
import { GetProvincesResponse } from '../models/location/get-provinces-response.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class LocationApiService {
  private baseUrl = environment.userRegistryApiBaseUrl;

  constructor(private http: HttpClient) {}

  getCountries(): Observable<GetCountriesResponse> {
    return this.http.get<GetCountriesResponse>(`${this.baseUrl}/location-service/countries`);
  }

  getProvinces(countryId: string): Observable<GetProvincesResponse> {
    return this.http.get<GetProvincesResponse>(`${this.baseUrl}/location-service/countries/${countryId}/provinces`);
  }
}