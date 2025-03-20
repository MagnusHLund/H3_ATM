using Hæveautomaten.Entities;

namespace Hæveautomaten.Interfaces.Repositories
{
    public interface IPersonRepository
    {
        bool CreatePerson(PersonEntity person);
        bool DeletePerson(uint personId);
    }
}