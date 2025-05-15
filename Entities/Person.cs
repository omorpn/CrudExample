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
        public GenderOption? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetter { get; set; }

        public Person(Guid personId,string? personName,string email,DateTime? dateOfBirth,GenderOption gender,Guid? countryId,string? country,string? address,bool receiveNewLetter) =>
            (PersonId,PersonName,Email,DateOfBirth,Gender,CountryId,Country,Address,ReceiveNewsLetter) = (personId,personName,email,dateOfBirth,gender,countryId,country,address,receiveNewLetter);

        public Person( string? personName, string email, DateTime? dateOfBirth, GenderOption gender, Guid? countryId, string? country, string? address, bool receiveNewLetter) =>
           (PersonName, Email, DateOfBirth, Gender, CountryId, Country, Address, ReceiveNewsLetter) = ( personName, email, dateOfBirth, gender, countryId, country, address, receiveNewLetter);


    }
}
