using Paymentsense.Coding.Challenge.Api.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace Paymentsense.Coding.Challenge.Api.Tests.Data
{
    public class DummyCountriesData
    {
        private List<Country> countries;
        private List<Country> borders;
        private CountryDetails countryDetails;

        public DummyCountriesData()
        {
            countries = new List<Country> {
                new Country {  Name = "Country A", Code = "AAA", Flag = "Flag-A"},
                new Country {  Name = "Country B", Code = "BBB", Flag = "Flag-B"},
                new Country {  Name = "Country C", Code = "CCC", Flag = "Flag-C"},
                new Country {  Name = "Country D", Code = "DDD", Flag = "Flag-D"},
                new Country {  Name = "Country E", Code = "EEE", Flag = "Flag-E"}
            };

            countryDetails = new CountryDetails
            {
                Name = "Country A",
                Code = "AAA",
                Flag = "Flag-A",
                Capital = "Capital A",
                Population = 20000,
                Region = "Region A",
                Subregion = "Subregion A",
                Borders = new string[2] { "CCC", "DDD" },
                Timezones = new string[0]
            };

            borders = new List<Country>
            {
                new Country {  Name = "Country C", Code = "CCC", Flag = "Flag-C"},
                new Country {  Name = "Country D", Code = "DDD", Flag = "Flag-D"}
            };
        }

        public List<Country> Countries { get { return countries; } }
        public List<Country> Borders { get { return borders; } }
        public CountryDetails CountryDetails { get { return countryDetails; } }

        public string CountriesJson
        {
            get
            {
                return JsonSerializer.Serialize(Countries);
            }
        }
        public string BordersJson
        {
            get
            {
                return JsonSerializer.Serialize(Borders);
            }
        }
        public string CountryDetailsJson
        {
            get
            {
                return JsonSerializer.Serialize(CountryDetails);
            }
        }
    }
}