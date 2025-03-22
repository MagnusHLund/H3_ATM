using Hæveautomaten.Entities;
using HæveautomatenTests.Views;
using Hæveautomaten.Controllers;
using HæveautomatenTests.Factories;
using HæveautomatenTests.Repositories;

namespace HæveautomatenTests.Tests.Features
{
    [TestClass]
    public class PersonFeatureTests
    {
        private InMemoryPersonRepository _personRepository;
        private FakeBaseView _baseView;
        private MainController _mainController;
        private AdminController _adminController;

        [TestInitialize]
        public void Setup()
        {
            _personRepository = new InMemoryPersonRepository();
            _baseView = new FakeBaseView();

            PersonController personController = new PersonController(_personRepository, _baseView);

            _adminController = new AdminController(
                null,
                personController,
                null,
                null,
                null,
                _baseView
            );

            _mainController = new MainController(_adminController, null, _baseView);
        }

        [TestMethod]
        public void CreatePersonWithMiddleNameThroughMainMenu_FeatureTest()
        {
            // Arrange
            string firstName = "John";
            string middleName = "James";
            string lastName = "Doe";

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "1", // Create Person
                firstName, // First name
                lastName, // Last name
                middleName // Middle name
            });

            // Act
            _mainController.HandleMainMenuDisplay();

            // Assert
            List<PersonEntity> persons = _personRepository.GetAllPeople();
            Assert.AreEqual(1, persons.Count);
            Assert.AreEqual(firstName, persons[0].FirstName);
            Assert.AreEqual(lastName, persons[0].LastName);
        }

        [TestMethod]
        public void CreatePersonWithoutMiddleNameThroughMainMenu_FeatureTest()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "1", // Create Person
                firstName, // First name
                lastName, // Last name
                "" // Middle name
            });

            // Act
            _mainController.HandleMainMenuDisplay();

            // Assert
            List<PersonEntity> persons = _personRepository.GetAllPeople();
            Assert.AreEqual(1, persons.Count);
            Assert.AreEqual(firstName, persons[0].FirstName);
            Assert.AreEqual(lastName, persons[0].LastName);
        }

        [TestMethod]
        public void DeletePersonThroughMainMenu_FeatureTest()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson();
            _personRepository.CreatePerson(person);

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "2", // Delete Person
                "1" // Select person
            });

            // Act
            _mainController.HandleMainMenuDisplay();

            // Assert
            List<PersonEntity> persons = _personRepository.GetAllPeople();
            Assert.AreEqual(0, persons.Count);
        }

        [TestMethod]
        public void CreatePersonWithEmptyFirstName_FeatureTest()
        {
            // Arrange
            string firstName = "";
            string lastName = "Doe";

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "1", // Create Person
                firstName, // Empty first name
                lastName // Last name
            });

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void CreatePersonWithEmptyLastName_FeatureTest()
        {
            // Arrange
            string firstName = "John";
            string lastName = "";

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "1", // Create Person
                firstName, // First name
                lastName // Empty last name
            });

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void DeletePersonWithNonExistingPerson_FeatureTest()
        {
            // Arrange
            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "2", // Delete Person
                "1" // Non-existing person
            });

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void CreatePersonWithDuplicateName_FeatureTest()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson("John", "Doe");
            _personRepository.CreatePerson(person);

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "1", // Create Person
                "John", // Duplicate first name
                "Doe" // Duplicate last name
            });

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void CreatePersonWithInvalidInput_FeatureTest()
        {
            // Arrange
            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "1", // Create Person
                "123", // Invalid first name
                "Doe" // Valid last name
            });

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _mainController.HandleMainMenuDisplay());
        }
    }
}