
using System.Diagnostics.Metrics;
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

        //When you supply proper country it should insert (add) the country to existing list of country
        [Fact]
        public void AddCountry_ProperCountry()
        {
            //Arrange
            CountryAddRequest? countryRequest = new() { CountryName = "Japan" };

            //Acts

            CountryResponse countryResponse = _countryService.AddCountry(countryRequest);

            List<CountryResponse> expected_country_list= _countryService.GetAllCountries();
            //Assert
            Assert.True(countryResponse.CountryId != Guid.Empty);
            Assert.Contains(countryResponse,expected_country_list);


        }

        #endregion
        #region Get All Counties
        [Fact]
        public void GetAllCountries_EmptyList()
        {
            //Arrange 
            //Act
            //Assert
            Assert.Empty(_countryService.GetAllCountries());

        }
        [Fact]
        public void GetAllCountries_ListAllCounties()
        {
            //Arrange 
           List<CountryAddRequest> add_request = new()
           { new(){CountryName="UK" },
               new(){CountryName="Nigeria" },
               new(){CountryName="India" },
               new(){CountryName="USa" },
               new(){CountryName="Japan" },
               new(){CountryName="Ghana" },
               new(){CountryName="Benin" }};
            //Act
            List<CountryResponse> list_country_from_add_country = new List<CountryResponse>();

            foreach(var country in add_request)
            {
               list_country_from_add_country.Add(_countryService.AddCountry(country));
            }
            List<CountryResponse> list_country_from_get_all_country = _countryService.GetAllCountries();
            //Assert
            foreach (var expected_country in list_country_from_add_country)
            {
                
            Assert.Contains(expected_country,list_country_from_get_all_country);
            }

        }
        #endregion

        #region Get Country by Id
        //When you supply null country id it should return null
        [Fact]
        public void GetCountryById_EmptyGuid()
        {
            //Arrage
            Guid? countryId = null;
            //Acts
            CountryResponse? autual_country = _countryService.GetCountryById(countryId);


            //Assert
            Assert.Null(autual_country);

        }

        [Fact]
        public void GetCountryById_valid_country_id()
        {
            //Arrage
            CountryAddRequest request_country = new() { CountryName = "Uk" };
            
            //Acts
            CountryResponse expected_country = _countryService.AddCountry(request_country);
            CountryResponse autual_country = _countryService.GetCountryById(expected_country.CountryId);

            //Assert
            Assert.Equal(expected_country, autual_country);

        }

        #endregion

    }
}
