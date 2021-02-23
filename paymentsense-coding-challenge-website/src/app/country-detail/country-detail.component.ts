import { Component, OnInit, OnChanges, SimpleChanges, Input } from '@angular/core';
import { Country } from '../models/country';
import { CountryDetails } from '../models/country-details';
import { CountriesApiService } from '../services/countries-api.service';

@Component({
  selector: 'app-country-detail',
  templateUrl: './country-detail.component.html',
  styleUrls: ['./country-detail.component.scss']
})
export class CountryDetailComponent implements OnInit, OnChanges {

  @Input() public countryCode: string;

  constructor(private countriesApiService: CountriesApiService) { }

  countryDetails: CountryDetails;
  borders: Country[];
  userMessage: string;
  loadingCountry: boolean;
  loadingBorders: boolean;

  ngOnInit() {
    
  }

  ngOnChanges(changes: SimpleChanges) {
    let code = changes.countryCode.currentValue;
    if (!code) {
      return;
    }
    this.loadingCountry = true;
    this.borders = [];
    this.countriesApiService.getByCode(code).subscribe(
      result => {
        if (result.success) {
          this.countryDetails = result.data;
          this.loadingCountry = false;
          this.populateBorders();
        }
        else {
          this.userMessage = result.message;
          this.loadingCountry = false;
        }
      }
    );
  }

  populateBorders() {
    if (this.countryDetails && this.countryDetails.borders.length) {
      this.loadingBorders = true;
      this.countriesApiService.getByCodes(this.countryDetails.borders).subscribe(
        result => {
          if (result.success) {            
            this.borders = result.data;
          }
          this.loadingBorders = false;
        }
      );
    }
  }

}
