import { Injectable } from '@angular/core';
import { of, Observable } from 'rxjs';

import { Country } from '../models/country';
import { CountryDetails } from '../models/country-details';
import { OperationResult } from '../models/operation-result';

@Injectable({
  providedIn: 'root'
})
export class MockCountriesApiService {
  public getAll(): Observable<OperationResult<Country[]>> {
    let countries: Country[] = [
      this.createDummyCountry('A'),
      this.createDummyCountry('B'),
      this.createDummyCountry('C'),
      this.createDummyCountry('D'),
      this.createDummyCountry('E'),
      this.createDummyCountry('F'),
      this.createDummyCountry('G'),
      this.createDummyCountry('H'),
      this.createDummyCountry('I'),
      this.createDummyCountry('J'),
      this.createDummyCountry('K'),
      this.createDummyCountry('L'),
      this.createDummyCountry('M'),
      this.createDummyCountry('N'),
      this.createDummyCountry('O'),
      this.createDummyCountry('P'),
      this.createDummyCountry('Q'),
      this.createDummyCountry('R'),
      this.createDummyCountry('S'),
      this.createDummyCountry('T'),
      this.createDummyCountry('U'),
      this.createDummyCountry('V'),
      this.createDummyCountry('W'),
      this.createDummyCountry('X'),
      this.createDummyCountry('Y'),
      this.createDummyCountry('Z')
    ];    
    return of<OperationResult<Country[]>>(this.createDummySuccessOperationResult(countries));
  }

  public getByCode(code: string): Observable<OperationResult<CountryDetails>> {
    return of<OperationResult<CountryDetails>>(
      this.createDummySuccessOperationResult(this.createDummyCountryDetails('A', ['CCC','FFF']))
    );
  }

  public getByCodes(codes: string[]): Observable<OperationResult<Country[]>> {
    let borders = [
      this.createDummyCountry('C'),
      this.createDummyCountry('F')
    ];
    return of<OperationResult<Country[]>>(this.createDummySuccessOperationResult(borders));
  }

  private createDummyCountry(char: string): Country {
    return { name: 'Country '.concat(char), alpha3Code: ''.concat(char,char,char), flag: 'flag-'.concat(char) };
  }

  private createDummyCountryDetails(char: string, borders: string[]): CountryDetails {
    return {
      name: 'Country '.concat(char),
      alpha3Code: ''.concat(char, char, char),
      flag: 'flag-'.concat(char),
      capital: 'Capital '.concat(char),
      borders: borders,
      population: 200000,
      region: 'Region '.concat(char),
      subregion: 'Subregion '.concat(char),
      timezones: []
    };
  }

  private createDummySuccessOperationResult<T>(data: T) {
    return { success: true, message: '', data: data };
  }
}
