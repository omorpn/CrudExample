using System;
using System.Collections.Generic;
using Entities;
using Service;
using ServiceContract;
using ServiceContract.DTO;


namespace CrudTest
{
    public class PersonTest
    {
        private readonly IpersonSevice _personSevice;
        private readonly ICountrysService _countryService;
        public PersonTest()
        {
            _personSevice = new PersonService(false);
            _countryService = new CountryService(false);
        }
        #region Add Person
        //When the person object is null it returns null
        [Fact]
        public void AddPerson_Null()
        {
            //Arrange
            PersonAddRequest? person_from_reques = null;


            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _personSevice.AddPerson(person_from_reques);

            });
        }
        //When the person Name is null it returns null
        [Fact]

        public void AddPerson_Null_Name()
        {
            //Arrange
            PersonAddRequest person_from_add_person = new() { FirstName = null, LastName = null, Email = "damiel@gmail.com", DateOfBirth = DateTime.Parse("1923/12/04"), Gender = GenderOption.Male };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _personSevice.AddPerson(person_from_add_person);

            });



        }
        //When the person object is null it returns null
        [Fact]

        public void AddPerson_null_email()
        {
            //Arrange
            PersonAddRequest person_from_add_person = new() { FirstName = "Tunde", LastName = "Daniel", Email = null, DateOfBirth = DateTime.Parse("1923/12/04"), Gender = GenderOption.Male };


            //Assert
            Assert.Throws<ArgumentException>(() =>
            {         //Act
                _personSevice.AddPerson(person_from_add_person);
            });

        }
        //When the person object is null it returns null
        [Fact]

        public void AddPerson_proper_person()
        {
            //Arrange
            PersonAddRequest person_from_add_person = new() { FirstName = "Tunde", LastName = "Daniel", Email = "damiel@gmail.com", DateOfBirth = DateTime.Parse("1923/12/04"), Gender = GenderOption.Male };
            CountryAddRequest? countryRequest = new() { CountryName = "Japan" };

            //Act
            CountryResponse countryResponse = _countryService.AddCountry(countryRequest);
            person_from_add_person.CountryId=countryResponse.CountryId;
            PersonResponse? expected_person = _personSevice.AddPerson(person_from_add_person);
            List<PersonResponse> list_of_person_responses = _personSevice.GetAllPerson();

            //Assert
            Assert.True(expected_person.PersonId != Guid.Empty);
            Assert.Contains(expected_person, list_of_person_responses);

        } //When the person object is null it returns null

        #endregion
        #region Get List Of Persons
        //Get an empty list of person response object
        [Fact]
        public void GetAllPersons_empty_list()
        {
            //Act 
            List<PersonResponse> expected_list = _personSevice.GetAllPerson();
            //Assert
            Assert.Empty(expected_list);
        }

        [Fact]
        public void GetAllPersons_list_of_country()
        {
            //Arrange
            List<PersonAddRequest> add_person = new()
            {
                new(){ FirstName ="David",LastName = "Mike",Email ="davidmaike@gmail.com"},
                new(){ FirstName ="wisdom",LastName = "james",Email ="jwisdom@gmail.com"},
                new(){ FirstName ="precious",LastName = "Mike",Email ="davidmaike@gmail.com"}
            };
            //Act 

            List<PersonResponse> autual_list = new();

            foreach (var person_from_add in add_person)
            {
                autual_list.Add(_personSevice.AddPerson(person_from_add));
            }
            List<PersonResponse> expected_list_of_person = _personSevice.GetAllPerson();
            //Assert
            foreach (var person_from_autual in autual_list)
            {
                Assert.Contains(person_from_autual, expected_list_of_person);

            }
        }



        #endregion
        #region Get Person By Person Id
        //Get the null value of personResponse
        [Fact]
        public void GetPersonById_null_id()
        {
            //Arrange
            Guid? user_id = null;
            //Act
            PersonResponse? autual_person = _personSevice.GetPersonById(user_id);
            //Assert
            Assert.Null(autual_person);
        }
        //Get personResponse object 
        [Fact]
        public void GetPersonById_user_id()
        {
            //Arragne
            PersonAddRequest person_from_add_person = new() { FirstName = "Tunde", LastName = "Daniel", Email = "damiel@gmail.com", DateOfBirth = DateTime.Parse("1923/12/04"), Gender = GenderOption.Male };
            //Act
            PersonResponse? person = _personSevice.AddPerson(person_from_add_person);
            PersonResponse? autual_person = _personSevice.GetPersonById(person.PersonId);
            List<PersonResponse> expected_list = _personSevice.GetAllPerson();
            //Assert
            Assert.Contains(autual_person, expected_list);
        }



        #endregion
        #region Get Person By Email

        //Get the null value of personResponse
        [Fact]
        public void GetPersonById_null_email()
        {
            //Arrange
            string? user_email = null;
            //Act
            PersonResponse? autual_person = _personSevice.GetPersonByEmail(user_email);
            //Assert
            Assert.Null(autual_person);
        }
        //Get personResponse object 
        [Fact]
        public void GetPersonById_user_id_email()
        {
            //Arragne
            PersonAddRequest person_from_add_person = new() { FirstName = "Tunde", LastName = "Daniel", Email = "damiel@gmail.com", DateOfBirth = DateTime.Parse("1923/12/04"), Gender = GenderOption.Male };
            //Act
            PersonResponse? person = _personSevice.AddPerson(person_from_add_person);
            PersonResponse? autual_person = _personSevice.GetPersonByEmail(person.Email);
            List<PersonResponse> expected_list = _personSevice.GetAllPerson();
            //Assert
            Assert.Contains(autual_person, expected_list);
        }
        #endregion


    }
}
