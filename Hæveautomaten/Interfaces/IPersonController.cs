using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces
{
    public interface IPersonController
    {
        void CreatePerson();
        void DeletePerson();
        List<PersonEntity> GetAllPeople();
    }
}