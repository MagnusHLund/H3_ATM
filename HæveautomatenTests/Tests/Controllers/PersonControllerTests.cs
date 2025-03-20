using Moq;
using Hæveautomaten.Entities;
using Hæveautomaten.Controllers;
using Hæveautomaten.Interfaces.Repositories;
using HæveautomatenTests.Factories;

namespace HæveautomatenTests.Tests.Controllers
{
    [TestClass]
    public class PersonControllerTests
    {
        private Mock<IPersonRepository> _personRepositoryMock;
        private PersonController _personController;

        [TestInitialize]
        public void Setup()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _personController = new PersonController(_personRepositoryMock.Object);
        }

        [TestMethod]
        public void CreatePerson_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson();
            _personRepositoryMock.Setup(r => r.CreatePerson(person)).Returns(true);

            // Act
            bool result = _personController.CreatePerson();

            // Assert
            Assert.IsTrue(result);
            _personRepositoryMock.Verify(r => r.CreatePerson(It.IsAny<PersonEntity>()), Times.Once);
        }

        [TestMethod]
        public void CreatePerson_WithInvalidData_ThrowsArgumentException()
        {
            // Arrange
            _personRepositoryMock.Setup(r => r.CreatePerson(It.IsAny<PersonEntity>())).Throws(new ArgumentException("Invalid person data"));

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _personController.CreatePerson());
        }

        [TestMethod]
        public void DeletePerson_WithExistingPerson_DeletesSuccessfully()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson();
            _personRepositoryMock.Setup(r => r.DeletePerson(person.PersonId)).Returns(true);

            // Act
            bool result = _personController.DeletePerson();

            // Assert
            Assert.IsTrue(result);
            _personRepositoryMock.Verify(r => r.DeletePerson(It.IsAny<uint>()), Times.Once);
        }

        [TestMethod]
        public void DeletePerson_WithNonExistingPerson_ThrowsKeyNotFoundException()
        {
            // Arrange
            _personRepositoryMock.Setup(r => r.DeletePerson(It.IsAny<uint>())).Throws(new KeyNotFoundException("Person not found"));

            // Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => _personController.DeletePerson());
        }

        [TestMethod]
        public void GetAllPeople_WhenCalled_ReturnsAllPeople()
        {
            // Arrange
            List<PersonEntity> people = new List<PersonEntity>
            {
                PersonFactory.CreatePerson(),
                PersonFactory.CreatePerson()
            };
            _personRepositoryMock.Setup(r => r.GetAllPeople()).Returns(people);

            // Act
            List<PersonEntity> result = _personController.GetAllPeople();

            // Assert
            Assert.AreEqual(people.Count, result.Count);
            CollectionAssert.AreEqual(people, result);
            _personRepositoryMock.Verify(r => r.GetAllPeople(), Times.Once);
        }

        [TestMethod]
        public void SelectPerson_WithValidInput_ReturnsSelectedPerson()
        {
            // Arrange
            List<PersonEntity> people = new List<PersonEntity>
            {
                PersonFactory.CreatePerson(),
                PersonFactory.CreatePerson(middleName: null)
            };
            _personRepositoryMock.Setup(r => r.GetAllPeople()).Returns(people);

            // Act
            PersonEntity selectedPerson = _personController.SelectPerson();

            // Assert
            Assert.IsNotNull(selectedPerson);
            Assert.AreEqual(people[0], selectedPerson);
        }

        [TestMethod]
        public void SelectPerson_WithInvalidInput_ThrowsFormatException()
        {
            // Arrange
            List<PersonEntity> people = new List<PersonEntity>
            {
                PersonFactory.CreatePerson(),
                PersonFactory.CreatePerson(middleName: null)
            };
            _personRepositoryMock.Setup(r => r.GetAllPeople()).Returns(people);

            // Act & Assert
            Assert.ThrowsException<FormatException>(() => _personController.SelectPerson());
        }

        [TestMethod]
        public void SelectPerson_WithOutOfRangeInput_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            List<PersonEntity> people = new List<PersonEntity>
            {
                PersonFactory.CreatePerson(),
                PersonFactory.CreatePerson(middleName: null)
            };
            _personRepositoryMock.Setup(r => r.GetAllPeople()).Returns(people);

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _personController.SelectPerson());
        }
    }
}