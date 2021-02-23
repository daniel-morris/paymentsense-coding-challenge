import { TestBed } from '@angular/core/testing';
import { HttpClient, HttpClientModule } from '@angular/common/http';

import { CountriesApiService } from './countries-api.service';

describe('CountriesApiService', () => {
  beforeEach(() => TestBed.configureTestingModule(
    {
      imports: [HttpClientModule],
      providers: [{ provide: HttpClient, useClass: HttpClientModule }]
    }
  ));

  it('should be created', () => {
    const service: CountriesApiService = TestBed.get(CountriesApiService);
    expect(service).toBeTruthy();
  });

});
