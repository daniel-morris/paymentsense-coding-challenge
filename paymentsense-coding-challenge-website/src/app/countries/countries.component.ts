import { Component, OnInit } from '@angular/core';
import { Country } from '../models/country';
import { CountriesApiService } from '../services';

@Component({
  selector: 'app-countries',
  templateUrl: './countries.component.html',
  styleUrls: ['./countries.component.scss']
})
export class CountriesComponent implements OnInit {

  constructor(private countriesApiService:CountriesApiService) { }

  public countries: Country[];
  public selectedCountryCode: string;
  userMessage = '';
  page = 1;
  pageSize = 20;  

  ngOnInit() {
    this.getAllCountries();
  }

  getAllCountries() {
    this.countriesApiService.getAll().subscribe(
      result => { result.success ? this.countries = result.data : this.userMessage = result.message }
    );
  }

  selectCountry(country) {
    this.selectedCountryCode = country.alpha3Code;
  }

  previous() {
    if (this.page > 1) {
      this.page -= 1;
    }
  }
  next() {
    if (this.page < this.numPages) {
      this.page += 1;
    }
  }

  get paginatedCountries() {
    let firstItem = (this.page - 1) * this.pageSize;
    let numItems = firstItem + this.pageSize;
    let count = this.countries.length;
    if (numItems > count) {
      numItems = count;
    }
    return this.countries.slice(firstItem, numItems);
  }

  get totalItems() {
    return (this.countries || []).length;
  }
  get numPages() {
    return Math.ceil(this.totalItems / this.pageSize);
  }
  get pages() {

    let paging = [];
    let start = 1;
    let range = 5; // max number of pages to display on screen
    let numPagesToDisplay = range;
    let halfRange = range / 2;

    if (this.numPages > range) {
      if (this.page < (halfRange + 1)) {
        start = 1;
      } else if (this.page >= (this.numPages - halfRange)) {
        start = Math.floor(this.numPages - range + 1);
      } else {
        start = (this.page - Math.floor(halfRange));
      }
    } else {
      numPagesToDisplay = this.numPages;
    }

    for (let i = start; i <= ((start + numPagesToDisplay) - 1); i++) {
      paging.push(i);
    }

    return paging;
  }
}
