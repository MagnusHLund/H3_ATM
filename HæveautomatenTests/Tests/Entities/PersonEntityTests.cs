using Hæveautomaten.Entities;
using HæveautomatenTests.Factories;

namespace HæveautomatenTests.Tests.Entities
{
    [TestClass]
    public class PersonEntityTests
    {
        [TestMethod]
        public void GetFullName_WithMiddleName_ReturnsFullName()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson(firstName: "John", middleName: "James", lastName: "Doe");

            // Act
            string fullName = person.ToString();

            // Assert
            Assert.AreEqual("John James Doe", fullName);
        }

        [TestMethod]
        public void GetFullName_WithoutMiddleName_ReturnsFullName()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson(firstName: "John", middleName: null, lastName: "Doe");

            // Act
            string fullName = person.ToString();

            // Assert
            Assert.AreEqual("John Doe", fullName);
        }

        [TestMethod]
        public void GetFullName_WithNullFirstName_ThrowsNullReferenceException()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson(firstName: null, lastName: "Doe");

            // Act & Assert
            Assert.ThrowsException<NullReferenceException>(() => person.ToString());
        }

        [TestMethod]
        public void GetFullName_WithNullLastName_ThrowsNullReferenceException()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson(firstName: "John", lastName: null);

            // Act & Assert
            Assert.ThrowsException<NullReferenceException>(() => person.ToString());
        }

        [TestMethod]
        public void GetFullName_WithEmptyStringFirstName_ThrowsArgumentException()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson(firstName: "", lastName: "Doe");

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => person.ToString());
        }

        [TestMethod]
        public void GetFullName_WithEmptyStringLastName_ThrowsArgumentException()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson(firstName: "John", lastName: "");

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => person.ToString());
        }

        [TestMethod]
        public void Constructor_WithValidParameters_CreatesPersonEntity()
        {
            // Arrange
            string firstName = "John";
            string middleName = "James";
            string lastName = "Doe";

            // Act
            PersonEntity person = PersonFactory.CreatePerson(firstName: firstName, middleName: middleName, lastName: lastName);

            // Assert
            Assert.AreEqual(firstName, person.FirstName);
            Assert.AreEqual(middleName, person.MiddleName);
            Assert.AreEqual(lastName, person.LastName);
            Assert.IsNotNull(person.Accounts);
            Assert.AreEqual(0, person.Accounts.Count);
        }
    }
}