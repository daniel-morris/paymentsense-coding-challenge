import { browser, by, element, WebElementPromise } from 'protractor';

export class AppPage {
  navigateTo() {
    return browser.get(browser.baseUrl) as Promise<any>;
  }

  getTitleText() {
    return element(by.css('app-root h1')).getText() as Promise<string>;
  }

  getCountriesTitleText() {
    return element(by.css('app-countries h2')).getText() as Promise<string>;
  }

  getCountriesList() {
    return element.all(by.css('app-countries .countries-list li'));
  }

  getCountryDetailsTitlText() {
    return element(by.css('app-countries app-country-detail h2')).getText() as Promise<string>;
  }

  getPagination() {
    return element.all(by.css('app-countries .pagination li'));
  }
}
