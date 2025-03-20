using Moq;
using Hæveautomaten.Entities;
using HæveautomatenTests.Factories;
using Hæveautomaten.Interfaces.Controllers;

namespace HæveautomatenTests.Tests.Controllers
{
    [TestClass]
    public class PersonControllerTests
    {
        private Mock<IPersonController> _personControllerMock;

        [TestInitialize]
        public void Setup()
        {
            _personControllerMock = new Mock<IPersonController>();
        }

        [TestMethod]
        public void CreatePerson_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson();
            _personControllerMock.Setup(p => p.CreatePerson(person)).Returns(true);

            // Act
            bool result = _personControllerMock.Object.CreatePerson(person);

            // Assert
            Assert.IsTrue(result);
            _personControllerMock.Verify(p => p.CreatePerson(person), Times.Once);
        }

        [TestMethod]
        public void CreatePerson_WithInvalidData_ThrowsException()
        {
            // Arrange
            PersonEntity person = new PersonEntity(null, null, null);
            _personControllerMock.Setup(p => p.CreatePerson(person)).Throws(new ArgumentException("Invalid person data"));

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _personControllerMock.Object.CreatePerson(person));
        }

        [TestMethod]
        public void DeletePerson_WithExistingPerson_DeletesSuccessfully()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson();
            _personControllerMock.Setup(p => p.DeletePerson(person)).Returns(true);

            // Act
            bool result = _personControllerMock.Object.DeletePerson(person);

            // Assert
            Assert.IsTrue(result);
            _personControllerMock.Verify(p => p.DeletePerson(person), Times.Once);
        }

        [TestMethod]
        public void DeletePerson_WithNonExistingPerson_ThrowsException()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson();
            _personControllerMock.Setup(p => p.DeletePerson(person)).Throws(new Exception("Person not found"));

            // Act & Assert
            Assert.ThrowsException<Exception>(() => _personControllerMock.Object.DeletePerson(person));
        }

        [TestMethod]
        public void GetAllPersons_WhenCalled_ReturnsAllPersons()
        {
            // Arrange
            List<PersonEntity> persons = new List<PersonEntity>
            {
                PersonFactory.CreatePerson(),
                PersonFactory.CreatePerson()
            };
            _personControllerMock.Setup(p => p.GetAllPeople()).Returns(persons);

            // Act
            List<PersonEntity> result = _personControllerMock.Object.GetAllPeople();

            // Assert
            Assert.AreEqual(persons.Count, result.Count);
            _personControllerMock.Verify(p => p.GetAllPeople(), Times.Once);
        }
    }
}