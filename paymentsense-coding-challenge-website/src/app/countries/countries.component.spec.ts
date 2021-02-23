import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CountryDetailComponent } from '../country-detail/country-detail.component';

import { CountriesApiService } from '../services';
import { MockCountriesApiService } from '../testing/mock-countries-api.service';
import { CountriesComponent } from './countries.component';

describe('CountriesComponent', () => {
  let component: CountriesComponent;
  let fixture: ComponentFixture<CountriesComponent>;
  let element: HTMLElement;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CountriesComponent, CountryDetailComponent],
      providers: [{ provide: CountriesApiService, useClass: MockCountriesApiService }]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CountriesComponent);
    component = fixture.componentInstance;
    element = fixture.nativeElement;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have list of countries', () => {
    let items = element.querySelectorAll('.countries-list li');
    expect(items.length).toBeGreaterThan(0); 
  });

  it('should have 2 pages of countries, with 20 on page 1', () => {
    let items = element.querySelectorAll('.countries-list li');
    expect(items.length).toBe(20); // pageSize is 20 and we have 26 dummy countries

    let pagination = element.querySelectorAll('.pagination li');
    expect(pagination.length).toBe(6); // 2 pages + first, previous, next and last
  });

  it('clicking on a country should call selectCountry and pass in the clicked country', () => {
    spyOn(component, 'selectCountry');

    let items = element.querySelectorAll('.countries-list li');
    var first = items.item(0).querySelector('div.selectable') as HTMLElement;

    first.click();
    fixture.detectChanges();

    expect(component.selectCountry).toHaveBeenCalled();
    let firstCountry = component.countries[0];
    expect(component.selectCountry).toHaveBeenCalledWith(firstCountry);
  });
});
