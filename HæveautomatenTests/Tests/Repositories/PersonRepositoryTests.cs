using Hæveautomaten.Data;
using Hæveautomaten.Entities;
using Hæveautomaten.Repositories;
using HæveautomatenTests.Factories;
using HæveautomatenTests.Utils;
using Moq;

namespace HæveautomatenTests.Tests.Repositories
{
    [TestClass]
    public class PersonRepositoryTests
    {
        private Mock<HæveautomatenDbContext> _dbContextMock;
        private PersonRepository _personRepository;

        [TestInitialize]
        public void Setup()
        {
            _dbContextMock = new Mock<HæveautomatenDbContext>();
            _personRepository = new PersonRepository(_dbContextMock.Object);
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
        }

        [TestMethod]
        public void DeletePerson_WithExistingPerson_DeletesSuccessfully()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson();
            _dbContextMock.Setup(db => db.Persons.Find(person.PersonId)).Returns(person);

            // Act
            bool result = _personRepository.DeletePerson(person.PersonId);

            // Assert
            Assert.IsTrue(result);
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
            _dbContextMock.Setup(db => db.Persons).Returns(MockUtils.CreateMockDbSet(people).Object);

            // Act
            List<PersonEntity> result = _personRepository.GetAllPeople();

            // Assert
            Assert.AreEqual(people.Count, result.Count);
        }
    }
}