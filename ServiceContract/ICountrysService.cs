using ServiceContract.DTO;

namespace ServiceContract
{
    public interface ICountrysService
    {
        /// <summary>
        /// Adds country 
        /// </summary>
        /// <param name="countryAddRequest"></param>
        /// <returns>countryResponse</returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

        /// <summary>
        /// Get all countries
        /// </summary>
        /// <returns>returns list of countries</returns>
        List<CountryResponse> GetAllCountries();

        CountryResponse? GetCountryById(Guid? countryId);
    }
}
