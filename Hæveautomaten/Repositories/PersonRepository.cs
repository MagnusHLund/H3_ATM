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
            try
            {
                _context.Persons.Add(person);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating person: {ex.Message}");
                return false;
            }
        }

        public bool DeletePerson(uint personId)
        {
            try
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
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Error deleting person: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting person: {ex.Message}");
                return false;
            }
        }

        public List<PersonEntity> GetAllPeople()
        {
            try
            {
                return _context.Persons
                    .Include(p => p.Accounts)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving people: {ex.Message}");
                return new List<PersonEntity>();
            }
        }
    }
}