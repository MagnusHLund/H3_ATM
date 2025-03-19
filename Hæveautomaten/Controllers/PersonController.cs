using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces;

namespace Hæveautomaten.Controllers
{
    public class PersonController : IPersonController
    {
        public void CreatePerson()
        {
            // Create a new person
        }

        public void DeletePerson()
        {
            // Get all people and store them in a variable
            // Select a person from the list and delete it
        }

        public List<PersonEntity> GetAllPeople()
        {
            // Return all people
            throw new NotImplementedException();
        }
    }
}