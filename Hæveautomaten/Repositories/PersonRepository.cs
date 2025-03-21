using Hæveautomaten.Data;
using Hæveautomaten.Entities;
using Microsoft.EntityFrameworkCore;
using Hæveautomaten.Interfaces.Repositories;

namespace Hæveautomaten.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly HæveautomatenDbContext _context;

        public PersonRepository(HæveautomatenDbContext context)
        {
            _context = context;
        }

        public bool CreatePerson(PersonEntity person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
            return true;
        }

        public bool DeletePerson(int personId)
        {

            PersonEntity person = _context.Persons.Find(personId);

            if (person == null)
            {
                throw new KeyNotFoundException($"Person with ID {personId} not found.");
            }

            _context.Persons.Remove(person);
            _context.SaveChanges();
            return true;
        }

        public List<PersonEntity> GetAllPeople()
        {
            List<PersonEntity> people = _context.Persons
                .Include(p => p.Accounts)
                .ToList();

            return people;
        }
    }
}