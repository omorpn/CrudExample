using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContract.DTO
{
    public class CountryAddRequest
    {
        [Required(ErrorMessage = "{0} can't be null or empty.")]
        public string? CountryName { get; set; }

        public Country? ToCountry()
        {
            if(CountryName == null)
            {
                return null;
            }
            return new Country { CountryName = CountryName };
        }
    }
}
