using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Country
    {
        public Guid CountryId { get; set; }
 
        public required string CountryName { get; set; }

    }
}
