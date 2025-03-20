using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Controllers;

namespace Hæveautomaten.Controllers
{
    public class PersonController : IPersonController
    {
        public bool CreatePerson(PersonEntity person)
        {
            // Create a new person

            throw new NotImplementedException();
        }

        public bool DeletePerson(PersonEntity person)
        {
            // Delete the person from the database

            throw new NotImplementedException();
        }

        public List<PersonEntity> GetAllPeople()
        {
            // Return all people
            throw new NotImplementedException();
        }
    }
}