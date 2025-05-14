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
        /// <returns>returs list of country</returns>
        List<CountryResponse> GetAllCountries();
    }
}
