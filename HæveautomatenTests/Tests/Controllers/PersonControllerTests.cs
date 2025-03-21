using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Hæveautomaten.Entities;
using Hæveautomaten.Controllers;
using Hæveautomaten.Interfaces.Repositories;
using HæveautomatenTests.Factories;
using Hæveautomaten.Interfaces.Views;
using System.Collections.Generic;

namespace HæveautomatenTests.Tests.Controllers
{
    [TestClass]
    public class PersonControllerTests
    {
        private Mock<IPersonRepository> _personRepositoryMock;
        private Mock<IBaseView> _baseViewMock;
        private PersonController _personController;

        [TestInitialize]
        public void Setup()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _baseViewMock = new Mock<IBaseView>();

            _personController = new PersonController(
                _personRepositoryMock.Object,
                _baseViewMock.Object
            );
        }

        [TestMethod]
        public void CreatePerson_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            string middleName = "Michael";

            _baseViewMock.Setup(view => view.GetUserInputWithTitle("Enter the first name: ")).Returns(firstName);
            _baseViewMock.Setup(view => view.GetUserInputWithTitle("Enter the last name: ")).Returns(lastName);
            _baseViewMock.Setup(view => view.GetUserInputWithTitle("Enter the middle name: ")).Returns(middleName);
            _personRepositoryMock.Setup(r => r.CreatePerson(It.IsAny<PersonEntity>())).Returns(true);

            // Act
            bool result = _personController.CreatePerson();

            // Assert
            Assert.IsTrue(result);
            _personRepositoryMock.Verify(r => r.CreatePerson(It.Is<PersonEntity>(p =>
                p.FirstName == firstName &&
                p.LastName == lastName &&
                p.MiddleName == middleName
            )), Times.Once);
        }

        [TestMethod]
        public void DeletePerson_WithExistingPerson_DeletesSuccessfully()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson();
            _personRepositoryMock.Setup(r => r.DeletePerson(person.PersonId)).Returns(true);
            _personRepositoryMock.Setup(r => r.GetAllPeople()).Returns(new List<PersonEntity> { person });
            _baseViewMock.Setup(view => view.CustomMenu(It.IsAny<string[]>(), It.IsAny<string>()));
            _baseViewMock.Setup(view => view.GetUserInput()).Returns("1");

            // Act
            bool result = _personController.DeletePerson();

            // Assert
            Assert.IsTrue(result);
            _personRepositoryMock.Verify(r => r.DeletePerson(person.PersonId), Times.Once);
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
            _baseViewMock.Setup(view => view.CustomMenu(It.IsAny<string[]>(), It.IsAny<string>()));
            _baseViewMock.Setup(view => view.GetUserInput()).Returns("1");

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
            _baseViewMock.Setup(view => view.CustomMenu(It.IsAny<string[]>(), It.IsAny<string>()));
            _baseViewMock.Setup(view => view.GetUserInput()).Returns("invalid");

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
            _baseViewMock.Setup(view => view.CustomMenu(It.IsAny<string[]>(), It.IsAny<string>()));
            _baseViewMock.Setup(view => view.GetUserInput()).Returns("3"); // Out of range input

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _personController.SelectPerson());
        }
    }
}