using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContract.DTO;
using ServiceContract.Enums;

namespace ServiceContract
{
    public interface IpersonSevice
    {
        /// <summary>
        /// Adds person object to list of persons
        /// </summary>
        /// <param name="request">the personRequest to add</param>
        /// <returns> object of personResponse</returns>
        PersonResponse? AddPerson(PersonAddRequest request);
        /// <summary>
        /// Get the list of personResponse object
        /// </summary>
        /// <returns>list of person objects</returns>
        List<PersonResponse> GetAllPerson();
        /// <summary>
        /// Get personResponse object by person id 
        /// </summary>
        /// <param name="personId">the Guid person id</param>
        /// <returns>the object of personResponse</returns>
        PersonResponse? GetPersonById(Guid? personId);
        
        /// <summary>
        /// Get personResponse object by person email 
        /// </summary>
        /// <param name="personId">the email </param>
        /// <returns>the object of personResponse</returns>
        PersonResponse? GetPersonByEmail(string? email);
        /// <summary>
        /// Get the Person Response By search with sortBy and 
        /// </summary>
        /// <param name="sortBy"> </param>
        /// <param name="searchBy"></param>
        /// <returns> list of sorted personResponse object</returns>
        List<PersonResponse> Filter(string? sortBy, string searchBy);
        /// <summary>
        /// Get the persponResponse sorted 
        /// </summary>
        /// <param name="persons">List of PersonResponse</param>
        /// <param name="sortBy">sortiing order</param>
        /// <param name="searchstring">sorting String</param>
        /// <returns></returns>
        List<PersonResponse> SortedPerson(List<PersonResponse> allPersons,SortOrderOption sortOrder, string sortby);
    }
}
