using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Controllers
{
    public interface IPersonController
    {
        bool CreatePerson(PersonEntity person);
        bool DeletePerson(PersonEntity person);
        List<PersonEntity> GetAllPeople();
    }
}