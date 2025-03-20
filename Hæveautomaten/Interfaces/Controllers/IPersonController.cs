using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Controllers
{
    public interface IPersonController
    {
        bool CreatePerson();
        bool DeletePerson();
        PersonEntity SelectPerson();
        List<PersonEntity> GetAllPeople();
    }
}