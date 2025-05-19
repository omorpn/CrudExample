using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContract;
using ServiceContract.DTO;
using ServiceContract.Enums;

namespace CrudExample.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonsController : Controller
    {
        private readonly IpersonSevice _personSevice;
        public PersonsController(IpersonSevice personSevice)
        {
            _personSevice = personSevice;
        }

        [Route("[action]")]
        [HttpGet("/")]
        public IActionResult Index(string? searchBy, string? searchString,string sortBy = nameof(PersonResponse.PersonName),SortOrderOption sortOrder=SortOrderOption.ASC)
        {
            List<PersonResponse> persons = _personSevice.Filter(searchBy, searchString);
            ViewBag.SearchBy = searchBy;
            ViewBag.SearchString = searchString;

            List<PersonResponse> personsSorted = _personSevice.SortedPerson(persons, sortOrder, sortBy);
            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder.ToString();
            ViewBag.PersonSorted = personsSorted;

            // Corrected the dictionary initialization
            ViewBag.SelectOption = new Dictionary<string, string>()
                {
                    { nameof(PersonResponse.PersonName), "Person Name" },
                    { nameof(PersonResponse.Email), "Email" },
                    { nameof(PersonResponse.Age), "Age" },
                    { nameof(PersonResponse.Gender), "Gender" },
                    { nameof(PersonResponse.Address), "Address" },
                    { nameof(PersonResponse.ReceiveNewsLetter), "Receive New Letter" }
                    
                };


            return View(personsSorted);
        }
    }
}
