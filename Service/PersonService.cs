using System;
using System.Linq;
using Entities;
using ServiceContract;
using ServiceContract.DTO;
using ServiceContract.Enums;
using ServiceContract.Helpers;

namespace Service
{

    public class PersonService : IpersonSevice
    {
        private readonly ICountrysService _countrysService;
        private readonly List<Person> _persons;
        public PersonService() {
            _countrysService = new CountryService();
            _persons = [];
        }

        public List<PersonResponse> Filter(string? searchBy, string searchString)
        {
            List<PersonResponse> allPerson = GetAllPerson();
            List<PersonResponse> matchingPerson = allPerson;

           if(!string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(searchBy))
            {
                matchingPerson = searchBy switch
                {
                    nameof(Person.PersonId) => allPerson.Where(temp => (!string.IsNullOrEmpty(temp.PersonName) ?
                                            temp.PersonName.Contains(searchBy, StringComparison.OrdinalIgnoreCase) : true)).ToList(),
                    nameof(Person.Email) => [..allPerson.Where(temp => temp.Email== null ||
                        temp.Email.Contains(searchBy, StringComparison.OrdinalIgnoreCase))],
                    nameof(Person.CountryId) => [..allPerson.Where(temp => temp.Country ==null ||
                        temp.Country.Contains(searchBy, StringComparison.OrdinalIgnoreCase) )],
                    nameof(Person.Address) => [..allPerson.Where(temp => temp.Address ==null ||
                        temp.Address.Contains(searchBy, StringComparison.OrdinalIgnoreCase) )],
                    nameof(Person.Gender) => [.. allPerson.Where(temp => temp.Gender == null || temp.Gender.Contains(searchBy, StringComparison.OrdinalIgnoreCase))],
                    nameof(Person.DateOfBirth) => [..allPerson.Where(temp=> temp.DateOfBirth == null||
                        temp.DateOfBirth.Value.ToString("dd MM yyyy").Contains(searchBy,  StringComparison.OrdinalIgnoreCase))],
                    _ => allPerson,
                };
            }
            return matchingPerson;
        }

        public PersonResponse? GetPersonByEmail(string? email)
        {
            if(email == null)
            {
                return null;
            }
            return _persons.Where(person => person.Email == email).Select(person => ToPersonResponse(person)).FirstOrDefault();
        }

        public PersonResponse? GetPersonById(Guid? personId)
        {
            if (personId == null)
            {
                return null;
            }
            return _persons.Where(person => person.PersonId == personId).Select(person => ToPersonResponse(person)).FirstOrDefault();

        }
        /// <summary>
        /// Converting the person to personResponse type
        /// </summary>
        /// <param name="person">the person to be converted</param>
        /// <returns>converted object of personResponse</returns>
        private PersonResponse ToPersonResponse(Person person)
        {
            PersonResponse response = person.ToPersonResponse();
            response.Country = _countrysService.GetCountryById(response.CountryId)?.CountryName;
            return response;
        }

        PersonResponse IpersonSevice.AddPerson(PersonAddRequest request)
        {
            //check the Personrequest object if null
             ArgumentNullException.ThrowIfNull(nameof(request));
        
            //validate personrequest object
             ValidationHelpers.ModelValidation(request);

            //convert PersonAddRequest type to person type
            Person person = request.ToPerson();
            person.PersonId = Guid.NewGuid();
            
            //add person to list of countries
            _persons.Add(person);


            return ToPersonResponse(person);


        }

        public List<PersonResponse> GetAllPerson()
        {

            if(_persons == null)
            {
                return [ ] ;
            }
            return [.. _persons.Select(person=> ToPersonResponse(person))];
          
        }

        public List<PersonResponse> SortedPerson(List<PersonResponse> allPersons, SortOrderOption sortOrder, string sortby)
        {
            if (string.IsNullOrEmpty(sortby))
            {
               return allPersons;
            }

            List<PersonResponse> sortedPersons = (sortby, sortOrder)
                switch
            {
                (nameof(PersonResponse.PersonName), SortOrderOption.ASC)
                => [.. allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase)],
                (nameof(PersonResponse.PersonName), SortOrderOption.DESC)
                => [.. allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase)],
                (nameof(PersonResponse.Email), SortOrderOption.ASC)
                => [.. allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase)],
                (nameof(PersonResponse.Email), SortOrderOption.DESC)
                => [.. allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase)],
                (nameof(PersonResponse.Age), SortOrderOption.ASC)
                => [.. allPersons.OrderBy(temp => temp.Age)],
                (nameof(PersonResponse.Age), SortOrderOption.DESC)
                => [.. allPersons.OrderByDescending(temp => temp.Age)],
                (nameof(PersonResponse.Address), SortOrderOption.ASC)
                => [.. allPersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase)],
                (nameof(PersonResponse.Address), SortOrderOption.DESC)
                => [.. allPersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase)],
                (nameof(PersonResponse.DateOfBirth), SortOrderOption.ASC)
                =>[.. allPersons.OrderBy(temp => temp.DateOfBirth)],
                (nameof(PersonResponse.DateOfBirth), SortOrderOption.DESC)
                => [.. allPersons.OrderByDescending(temp => temp.DateOfBirth)],
                (nameof(PersonResponse.Gender), SortOrderOption.ASC)
                => [..allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase)],
                (nameof(PersonResponse.Gender), SortOrderOption.DESC)
                => [..allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase)],
                (nameof(PersonResponse.Country), SortOrderOption.ASC)
               => [.. allPersons.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase)],
                (nameof(PersonResponse.Country), SortOrderOption.DESC)
                => [.. allPersons.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase)],
                _=>allPersons

            };
            return sortedPersons;
        }
    }
}
