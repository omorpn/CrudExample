using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContract.DTO
{
    public class PersonAddRequest
    {
        [DisplayName("First Name")]
        [Required(ErrorMessage ="{0} can't be null or empty")]
        [RegularExpression("^(?=.{3,40}$)[A-Za-z]+(?: [a-zA-Z]+)*$")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "{0} can't be null or empty")]
        [RegularExpression("^(?=.{3,40}$)[A-Za-z]+(?: [a-zA-Z]+)*$")]
       
        [DisplayName("Last Name")]
        public string? LastName { get; set; }
        [DisplayName("Emmail Address")]

        [Required(ErrorMessage = "{0} can't be null or empty")]
        [EmailAddress(ErrorMessage ="{0} must be of type email")]
        public string? Email { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        public GenderOption Gender { get; set; }

        public Guid? CountryId { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetter { get; set; }
        public Person ToPerson()
        {
           
            string PersonName = FirstName + " " + LastName;

            return new Person(PersonName,Email,DateOfBirth,Gender,CountryId,Country,Address,ReceiveNewsLetter);
        }
    }
}
