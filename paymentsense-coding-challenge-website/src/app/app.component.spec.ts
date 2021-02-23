import { TestBed, async } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { CountriesComponent } from './countries/countries.component';
import { CountryDetailComponent } from './country-detail/country-detail.component';
import { CountriesApiService, PaymentsenseCodingChallengeApiService } from './services';
import { MockPaymentsenseCodingChallengeApiService } from './testing/mock-paymentsense-coding-challenge-api.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HttpClientModule } from '@angular/common/http';
import { MockCountriesApiService } from './testing/mock-countries-api.service';

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        FontAwesomeModule,
        HttpClientModule
      ],
      declarations: [
        AppComponent,
        CountriesComponent,
        CountryDetailComponent
      ],
      providers: [
        { provide: PaymentsenseCodingChallengeApiService, useClass: MockPaymentsenseCodingChallengeApiService },
        { provide: CountriesApiService, useClass: MockCountriesApiService }
      ]
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'Paymentsense Coding Challenge'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual('Paymentsense Coding Challenge!');
  });

  it('should render title in a h1 tag', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('h1').textContent).toContain('Paymentsense Coding Challenge!');
  });
});
