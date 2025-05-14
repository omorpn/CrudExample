
using Entities;
using Service;
using ServiceContract;
using ServiceContract.DTO;

namespace CrudTest
{
    public class CountryTest
    {
        private readonly ICountrysService _countryService;

        public CountryTest()
        {
            _countryService = new CountryService();
        }
        #region Add Country
        //When the Country is null, it should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest? countryRequest = null;


            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _countryService.AddCountry(countryRequest);
            });


        }
        //When the CountryName is null, it should throw ArgumentException
        [Fact]
        public void AddCountry_NullCountryName()
        {
            //Arrange
            CountryAddRequest? countryRequest = new CountryAddRequest() { CountryName = null };
            ;


            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countryService.AddCountry(countryRequest);
            });


        }
        //When the CountryNme is duplicate, it should throw ArgumentException
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            //Arrange
            CountryAddRequest? request1 = new() { CountryName = "USA" };
            CountryAddRequest? request2 = new() { CountryName = "USA" };

            //Assert 
            Assert.Throws<ArgumentException>(() =>
            {
                _countryService.AddCountry(request1);
                _countryService.AddCountry(request2);
            });
        }

        //When you supply proper country it should insert (insert) the country to existing list of country
        [Fact]
        public void AddCountry_ProperCountry()
        {
            //Arrange
            CountryAddRequest? countryRequest = new() { CountryName = "Japan" };

            //Acts

            CountryResponse countryResponse = _countryService.AddCountry(countryRequest);
            //Assert
            Assert.True(countryResponse.CountryId != Guid.Empty);


        }

        #endregion
        #region Get All Counties
        public void GetAllCountries_EmptyList()
        {

        }

        #endregion

    }
}
