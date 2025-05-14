using ServiceContract.DTO;

namespace ServiceContract
{
    public interface ICountryService
    {
        CountryResponse AddCountry(CountryAddRequest countryAddRequest);
    }
}
