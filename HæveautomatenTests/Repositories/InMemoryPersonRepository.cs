using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Repositories;

namespace HæveautomatenTests.Repositories
{
    public class InMemoryPersonRepository : IPersonRepository
    {
        private readonly List<PersonEntity> _persons = new List<PersonEntity>();

        public bool CreatePerson(PersonEntity person)
        {
            _persons.Add(person);
            return true;
        }

        public bool DeletePerson(int personId)
        {
            PersonEntity person = _persons.FirstOrDefault(p => p.PersonId == personId);
            if (person == null)
            {
                throw new KeyNotFoundException($"Person with ID {personId} not found.");
            }

            _persons.Remove(person);
            return true;
        }

        public List<PersonEntity> GetAllPeople()
        {
            return new List<PersonEntity>(_persons);
        }
    }
}