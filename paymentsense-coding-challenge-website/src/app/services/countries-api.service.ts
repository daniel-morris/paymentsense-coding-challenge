import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError, retry } from 'rxjs/operators';

import { Country } from '../models/country';
import { CountryDetails } from '../models/country-details';
import { OperationResult } from '../models/operation-result';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CountriesApiService {
  private serviceUrl = environment.countries.apiUrlBase;
  constructor(private httpClient: HttpClient) {}

  public getAll(): Observable<OperationResult<Country[]>> {
    var response = this.httpClient.get<OperationResult<Country[]>>(
      this.serviceUrl.concat(environment.countries.endpointList),
      { responseType: 'json' }).pipe(retry(3),
        catchError(() => { return of({} as OperationResult<Country[]>); }));
    return response;
  }

  public getByCode(code:string): Observable<OperationResult<CountryDetails>> {
    var response = this.httpClient.get<OperationResult<CountryDetails>>(
      this.serviceUrl.concat(environment.countries.endpointDetails, code),
      { responseType: 'json' }).pipe(catchError(() => { return of({} as OperationResult<CountryDetails>); }));
    return response;
  }

  public getByCodes(codes:string[]): Observable<OperationResult<Country[]>> {
    var response = this.httpClient.get<OperationResult<Country[]>>(
      this.serviceUrl.concat(environment.countries.endpointListByCodes, codes.join(';')),
      { responseType: 'json' }).pipe(retry(3),
        catchError(() => { return of({} as OperationResult<Country[]>); }));
    return response;
  }

}
