using System;
using Entities;

namespace ServiceContract.DTO
{
    /// <summary>
    /// Represent Dto class that used as return type as most of personService
    /// </summary>
    public class PersonResponse
    {
        public Guid PersonId { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOption? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public int? Age { get; set; }
        public bool ReceiveNewsLetter { get; set; }


        public override bool Equals(object? obj)
        {
            if(obj == null)
            {
                return false;
            }

            if(obj.GetType() != typeof(PersonResponse))
            {
                return false;
            }
            PersonResponse objPerson = (PersonResponse)obj;


            return PersonId == objPerson.PersonId && PersonName == objPerson.PersonName && Email == objPerson.Email
                && DateOfBirth == objPerson.DateOfBirth && Gender == objPerson.Gender && CountryId == objPerson.CountryId
                &&Country == objPerson.Country && Address ==objPerson.Address  && Age ==objPerson.Age && ReceiveNewsLetter ==objPerson.ReceiveNewsLetter ;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

    public static class PersonExtension
    {
        /// <summary>
        /// An extension method to convert object of person class to object of personResponse class
        /// </summary>
        /// <param name="person">The person object to convert</param>
        /// <returns>Returns object of personResponse class Converted</returns>
        public static PersonResponse ToPersonResponse(this Person person)
        {
            

            return new PersonResponse() { PersonId = person.PersonId, PersonName = person.PersonName, Email = person.Email, DateOfBirth = person.DateOfBirth, Gender = person.Gender, CountryId = person.CountryId,Country=person.Country, Address = person.Address, Age = (person.DateOfBirth !=null)?(int)Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25):null, ReceiveNewsLetter = person.ReceiveNewsLetter };
        }
    }
}
