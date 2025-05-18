using System;
using System.Collections.Generic;


namespace Entities
{
    public class Person
    {
        public Guid PersonId { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetter { get; set; }
        

        public Person( string? personName, string email, DateTime? dateOfBirth, string? gender, Guid? countryId, string? address, bool receiveNewLetter) =>
           (PersonName, Email, DateOfBirth, Gender, CountryId, Address, ReceiveNewsLetter) = ( personName, email, dateOfBirth, gender, countryId, address, receiveNewLetter);

      
    }
}
