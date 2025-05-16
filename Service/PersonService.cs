using System;
using Entities;
using ServiceContract;
using ServiceContract.DTO;
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
                switch (searchBy)
                {
                    case nameof(Person.PersonId):
                        matchingPerson= allPerson.Where(temp => (!string.IsNullOrEmpty(temp.PersonName)?
                        temp.PersonName.Contains(searchBy,StringComparison.OrdinalIgnoreCase):true)).ToList();
                            break;
                    case nameof(Person.Email):
                        matchingPerson = allPerson.Where(temp => (!string.IsNullOrEmpty(temp.Email) ?
                        temp.Email.Contains(searchBy, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                        break;
                    case nameof(Person.CountryId):
                        matchingPerson = allPerson.Where(temp => (!string.IsNullOrEmpty(temp.Country) ?
                        temp.Country.Contains(searchBy, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                        break;
                    case nameof(Person.Address):
                        matchingPerson = allPerson.Where(temp => (!string.IsNullOrEmpty(temp.Address) ?
                        temp.Address.Contains(searchBy, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                        break;
                    case nameof(Person.Gender):
                        matchingPerson =allPerson.Where(temp=>(temp.Gender != null)?
                        temp.Gender.Contains(searchBy, StringComparison.OrdinalIgnoreCase) : true).ToList();
                        break;
                    case nameof(Person.DateOfBirth):
                        matchingPerson= allPerson.Where(temp=>(temp.DateOfBirth != null)?
                        temp.DateOfBirth.Value.ToString("dd MM yyyy").Contains(searchBy,  StringComparison.OrdinalIgnoreCase) : true).ToList();
                        break;
                    default:
                        matchingPerson = allPerson;
                        break;
                }
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
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
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
                return null;
            }
            return _persons.Select(person=> ToPersonResponse(person)).ToList();
          
        }
    }
}
