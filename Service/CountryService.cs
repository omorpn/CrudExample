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
        public CountryService(bool initialize = true)
        {
            _countries = [];
            if (initialize) {
                _countries.AddRange(new List<Country>() {
                    new() { CountryId =Guid.Parse("6700B1C7-A1E6-44C1-AFBF-CCDE61A90EFB"),CountryName="Nigeria"} ,
                    new() { CountryId =Guid.Parse("3B336ADE-B6E1-4227-B678-CF32755C91FC"),CountryName="USA"} ,
                    new() { CountryId =Guid.Parse("8CFF6C07-5797-4843-8C91-38F4CE1DA075"),CountryName="Uk"} ,
                    new() { CountryId =Guid.Parse("8A79CB90-F98B-4904-8DE8-AC48FA3DDBDE"),CountryName="Ghana"} ,
                    new() { CountryId =Guid.Parse("A9597B00-30D6-4EB7-AD19-7EA03F99620B"),CountryName="South korea"} ,
                    new() { CountryId =Guid.Parse("A46F7160-9BB9-40B0-A9D2-01E405410085"),CountryName="Seniga"} 
                
                
                
                });
            
            }
        }
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            //check if the countryAddRequest is null
            ArgumentNullException.ThrowIfNull(countryAddRequest);
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
            return [.._countries.Select(country => country.ToCountryResponse())];
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
