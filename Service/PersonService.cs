using System;
using Entities;
using ServiceContract;
using ServiceContract.DTO;
using ServiceContract.Helpers;

namespace Service
{

    public class PersonService : IpersonSevice
    {
        private readonly List<Person> _persons;
        public PersonService() {
            _persons = [];
        }

        public PersonResponse GetPersonByEmail(string? email)
        {
            throw new NotImplementedException();
        }

        public PersonResponse GetPersonById(Guid? personId)
        {
            throw new NotImplementedException();
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

            return person.ToPersonResponse();
            
        }

        List<PersonResponse>? IpersonSevice.GetAllPerson()
        {

            if(_persons == null)
            {
                return null;
            }
            return _persons.Select(country=>country.ToPersonResponse()).ToList();
          
        }
    }
}
