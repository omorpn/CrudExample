using ServiceContract;
using ServiceContract.Helpers;
using ServiceContract.DTO;
using System.ComponentModel.DataAnnotations;
using Entities;

namespace Service
{
    public class CountryService : ICountrysService
    {
        private readonly List<Country> _countries;
        public CountryService()
        {
            _countries =new List<Country>();
        }
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            //check if the countryAddRequest is null
            if (countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }
            // check if the countryAddRequest Name is null
            if (countryAddRequest.CountryName == null)
            {

                throw new ArgumentException(nameof(countryAddRequest));

            }
            //check if countryAddRequest is Duplicate
            bool isTrue = _countries.Where(country => country.CountryName == countryAddRequest.CountryName).Count() > 0;
            if (isTrue)
            {
                throw new ArgumentException(nameof(countryAddRequest));
            }
 
            //convert countryAddrequst into country type
             Country? country = countryAddRequest.ToCountry();

            //Generate CountryId
            country.CountryId = Guid.NewGuid();

            //Add to country list 
            _countries.Add(country);


            //convert to Countryresponse
           return country.ToCountryResponse();


        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.ToCountryResponse())
                .ToList();
        }

        public CountryResponse? GetCountryById(Guid? countryId)
        {
            if(_countries == null)
            {
                return null;
            }

            return _countries.Where(country => country.CountryId == countryId)
                .Select(country => country.ToCountryResponse())
                .FirstOrDefault();
        }
    }
}
