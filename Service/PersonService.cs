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
        public PersonService(ICountrysService countrysService,bool initialize = true)
        {
            _countrysService = countrysService;
            _persons = [];

            if (initialize)
            {
                _persons.AddRange(new List<Person> {
                    new() { PersonId = Guid.Parse("772902ED-2F5F-4CA6-8C0C-ABEE2A040E57"), PersonName = "Wisdom grace", Email = "Wisdomgrace@gmail.com", DateOfBirth = DateTime.Parse("04/12/1999"), Gender = "Male", CountryId = Guid.Parse("A46F7160-9BB9-40B0-A9D2-01E405410085"), Address = "Lagos", ReceiveNewsLetter = true },
                    new() { PersonId = Guid.Parse("4295D0A8-F026-46FF-A150-0DC3FAE06491"), PersonName = "Mike Ruth", Email = "MikeRuth@gmail.com", DateOfBirth = DateTime.Parse("01/01/2000"), Gender = "Female", CountryId = Guid.Parse("A9597B00-30D6-4EB7-AD19-7EA03F99620B"), Address = "Auchi", ReceiveNewsLetter = false },
                    new() { PersonId = Guid.Parse("F47962E7-14F4-4473-B9B7-D6C77578DF6A"), PersonName = "Gabriel marthins", Email = "Gabrielmarthins@gmail.com", DateOfBirth = DateTime.Parse("10/04/1995"), Gender = "Male", CountryId = Guid.Parse("8A79CB90-F98B-4904-8DE8-AC48FA3DDBDE"), Address = "Abuja", ReceiveNewsLetter = true },
                    new() { PersonId = Guid.Parse("BA7F1970-02DE-43D3-940C-ACA0FCE70A96"), PersonName = "Blessing Benand", Email = "BlessingBenand@gmail.com", DateOfBirth = DateTime.Parse("10/04/2005"), Gender = "FeMale", CountryId = Guid.Parse("A46F7160-9BB9-40B0-A9D2-01E405410085"), Address = "Ekpoma", ReceiveNewsLetter = false },
                    new() { PersonId = Guid.Parse("1B8DCFCC-B66B-43C7-825B-B3309E000FCA"), PersonName = "God'swill David", Email = "GodswillDavid@gmail.com", DateOfBirth = DateTime.Parse("10/09/2015"), Gender = "Male", CountryId = Guid.Parse("3B336ADE-B6E1-4227-B678-CF32755C91FC"), Address = "Jattu", ReceiveNewsLetter = true },
                    new() { PersonId = Guid.Parse("97C310C0-1F6F-4D12-868A-37A9C68B3556"), PersonName = "isacc Blessing", Email = "isaccBlessing@gmail.com", DateOfBirth = DateTime.Parse("10/04/2010"), Gender = "FeMale", CountryId = Guid.Parse("6700B1C7-A1E6-44C1-AFBF-CCDE61A90EFB"), Address = "Lagos", ReceiveNewsLetter = false },
                    new() { PersonId = Guid.Parse("97C310C0-1F6F-4D12-868A-37A9C68B3556"), PersonName = "isacc Blessing", Email = "isaccBlessing@gmail.com", CountryId = Guid.Parse("6700B1C7-A1E6-44C1-AFBF-CCDE61A90EFB"), Address = "Lagos", ReceiveNewsLetter = false }
                
                });
            }
        }

        public List<PersonResponse> Filter(string? searchBy, string searchString)
        {
            List<PersonResponse> allPerson = GetAllPerson();
            List<PersonResponse> matchingPerson = allPerson;

            if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(searchBy))
            {
                matchingPerson = searchBy  switch
                {
                    nameof(PersonResponse.PersonName) => allPerson.Where(temp => (!string.IsNullOrEmpty(temp.PersonName) ?
                                            temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList(),
                    nameof(PersonResponse.Email) => [..allPerson.Where(temp => temp.Email== null ||
                        temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))], 
                    nameof(PersonResponse.ReceiveNewsLetter) => [..allPerson.Where(temp => 
                        temp.ReceiveNewsLetter.ToString().Contains(searchString,StringComparison.OrdinalIgnoreCase))],
                    nameof(PersonResponse.Country) => [..allPerson.Where(temp => temp.Country ==null ||
                        temp.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase) )],
                    nameof(PersonResponse.Address) => [..allPerson.Where(temp => temp.Address ==null ||
                        temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase) )],
                    nameof(PersonResponse.Gender) => [.. allPerson.Where(temp => temp.Gender == null || temp.Gender.Equals(searchBy, StringComparison.OrdinalIgnoreCase))],
                    nameof(PersonResponse.DateOfBirth) => [..allPerson.Where(temp=> temp.DateOfBirth == null||
                        temp.DateOfBirth.Value.ToString("dd MM yyyy").Contains(searchString,  StringComparison.OrdinalIgnoreCase))],
                    nameof(PersonResponse.Age) => [..allPerson.Where(temp=> temp.Age == null||
                        temp.Age.Value.ToString().ToLower() == searchString.ToLower())],
                    _ => allPerson,
                };
            }
            return matchingPerson;
        }

        public PersonResponse? GetPersonByEmail(string? email)
        {
            if (email == null)
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
        /// Converting the person to per sonResponse type
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

            if (_persons == null)
            {
                return [];
            }
            return [.. _persons.Select(person => ToPersonResponse(person))];

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
                => [.. allPersons.OrderBy(temp => temp.DateOfBirth)],
                (nameof(PersonResponse.DateOfBirth), SortOrderOption.DESC)
                => [.. allPersons.OrderByDescending(temp => temp.DateOfBirth)],
                (nameof(PersonResponse.Gender), SortOrderOption.ASC)
                => [.. allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase)],
                (nameof(PersonResponse.Gender), SortOrderOption.DESC)
                => [.. allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase)],
                (nameof(PersonResponse.Country), SortOrderOption.ASC)
               => [.. allPersons.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase)],
                (nameof(PersonResponse.Country), SortOrderOption.DESC)
                => [.. allPersons.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase)],
                (nameof(PersonResponse.ReceiveNewsLetter), SortOrderOption.ASC)
               => [.. allPersons.OrderBy(temp => temp.ReceiveNewsLetter)],
                (nameof(PersonResponse.ReceiveNewsLetter), SortOrderOption.DESC)
                => [.. allPersons.OrderByDescending(temp => temp.ReceiveNewsLetter)],
                _ => allPersons

            };
            return sortedPersons;
        }
    }
}
