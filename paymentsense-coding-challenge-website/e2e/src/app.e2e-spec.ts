import { AppPage } from './app.po';
import { browser, logging } from 'protractor';

describe('Paymentsense-coding-challenge App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getTitleText()).toEqual('Paymentsense Coding Challenge!');
  });

  it('should display countries title', () => {
    page.navigateTo();
    expect(page.getCountriesTitleText()).toEqual('Countries');
  });

  it('should display a list of countries, with first country = Afghanistan', async () => {
    page.navigateTo();        
    let countriesList = page.getCountriesList();
    expect(countriesList.count()).toBeGreaterThan(0);
    expect(countriesList.first().getText()).toBe('Afghanistan');
  });

  it('should display country details, when country clicked', async () => {
    page.navigateTo();
    let countriesList = page.getCountriesList();

    let firstItem = countriesList.first();
    firstItem.click();

    let countryDetailsTitle = page.getCountryDetailsTitlText();
    expect(countryDetailsTitle).toContain('Afghanistan');
  });

  it('should navigate to second page, when page 2 is clicked, and be able to select a country', async () => {
    page.navigateTo();
    let pagination = page.getPagination();

    let secondPage = pagination.get(3); // 3rd index contains 2nd page, (first and previous before the numbers)
    secondPage.click(); // navigate to 2nd page

    let countriesList = page.getCountriesList();
    let firstItem = countriesList.first();
    firstItem.click(); // select the first country on page 2

    let countryDetailsTitle = page.getCountryDetailsTitlText();

    expect(firstItem.getText()).toBe('Belarus');
    expect(countryDetailsTitle).toContain('Belarus');
  });

  afterEach(async () => {
    // Assert that there are no errors emitted from the browser
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining({
      level: logging.Level.SEVERE,
    } as logging.Entry));
  });
});
