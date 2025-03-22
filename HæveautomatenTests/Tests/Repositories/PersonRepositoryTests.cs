using Hæveautomaten.Data;
using Hæveautomaten.Entities;
using Hæveautomaten.Repositories;
using HæveautomatenTests.Factories;
using Microsoft.EntityFrameworkCore;

namespace HæveautomatenTests.Tests.Repositories
{
    [TestClass]
    public class PersonRepositoryTests
    {
        private HæveautomatenDbContext _dbContext;
        private PersonRepository _personRepository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HæveautomatenDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new HæveautomatenDbContext(options);
            _personRepository = new PersonRepository(_dbContext);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [TestMethod]
        public void CreatePerson_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson();

            // Act
            bool result = _personRepository.CreatePerson(person);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _dbContext.Persons.CountAsync().Result);
        }

        [TestMethod]
        public void CreatePerson_WithNullPerson_ThrowsNullReferenceException()
        {
            // Act & Assert
            Assert.ThrowsException<NullReferenceException>(() => _personRepository.CreatePerson(null));
        }

        [TestMethod]
        public void DeletePerson_WithExistingPerson_DeletesSuccessfully()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson();
            _dbContext.Persons.Add(person);
            _dbContext.SaveChanges();

            // Act
            bool result = _personRepository.DeletePerson(person.PersonId);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _dbContext.Persons.CountAsync().Result);
        }

        [TestMethod]
        public void DeletePerson_WithNonExistingPerson_ThrowsKeyNotFoundException()
        {
            // Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => _personRepository.DeletePerson(999));
        }

        [TestMethod]
        public void GetAllPeople_ReturnsAllPeople()
        {
            // Arrange
            List<PersonEntity> people = new List<PersonEntity>
            {
                PersonFactory.CreatePerson(),
                PersonFactory.CreatePerson()
            };
            _dbContext.Persons.AddRange(people);
            _dbContext.SaveChanges();

            // Act
            List<PersonEntity> result = _personRepository.GetAllPeople();

            // Assert
            Assert.AreEqual(people.Count, result.Count);
        }

        [TestMethod]
        public void GetAllPeople_WithNoPeople_ReturnsEmptyList()
        {
            // Act
            List<PersonEntity> result = _personRepository.GetAllPeople();

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void CreatePerson_WithDuplicatePerson_ThrowsArgumentException()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson();
            _dbContext.Persons.Add(person);
            _dbContext.SaveChanges();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _personRepository.CreatePerson(person));
        }
    }
}