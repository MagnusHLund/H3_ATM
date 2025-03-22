using Hæveautomaten.Entities;
using HæveautomatenTests.Factories;

namespace HæveautomatenTests.Tests.Entities
{
    [TestClass]
    public class CreditCardEntityTests
    {
        [TestMethod]
        public void Constructor_WithValidParameters_CreatesCreditCardEntity()
        {
            // Arrange
            string cardNumber = "1234123412341234";
            string cardHolderName = "John Doe";
            ushort cvv = 123;
            DateTime expirationDate = DateTime.Now.AddYears(3);
            ushort pinCode = 1234;
            bool isBlocked = false;
            AccountEntity account = AccountFactory.CreateAccount();

            // Act
            CreditCardEntity creditCard = new CreditCardEntity(
                cardNumber: cardNumber,
                cardHolderName: cardHolderName,
                account: account,
                expirationDate: expirationDate,
                cvv: cvv,
                pinCode: pinCode,
                isBlocked: isBlocked
            );

            // Assert
            Assert.AreEqual(cardNumber, creditCard.CardNumber);
            Assert.AreEqual(cardHolderName, creditCard.CardHolderName);
            Assert.AreEqual(cvv, creditCard.Cvv);
            Assert.AreEqual(expirationDate, creditCard.ExpirationDate);
            Assert.AreEqual(pinCode, creditCard.PinCode);
            Assert.AreEqual(isBlocked, creditCard.IsBlocked);
            Assert.AreEqual(account, creditCard.Account);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectStringRepresentation()
        {
            // Arrange
            string cardNumber = "1234123412341234";
            string cardHolderName = "John Doe";
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard(
                cardNumber: cardNumber,
                cardHolder: PersonFactory.CreatePerson(firstName: "John", lastName: "Doe", middleName: null)
            );

            // Act
            string result = creditCard.ToString();

            // Assert
            Assert.AreEqual($"{cardHolderName} - {cardNumber}", result);
        }

        [TestMethod]
        public void IsBlockedProperty_CanBeSetAndRetrieved()
        {
            // Arrange
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard(isBlocked: true);

            // Act
            bool isBlocked = creditCard.IsBlocked;

            // Assert
            Assert.IsTrue(isBlocked);
        }

        [TestMethod]
        public void ExpirationDateProperty_CanBeSetAndRetrieved()
        {
            // Arrange
            DateTime expirationDate = DateTime.Now.AddYears(5);
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard(expirationDate: expirationDate);

            // Act
            DateTime result = creditCard.ExpirationDate;

            // Assert
            Assert.AreEqual(expirationDate, result);
        }
    }
}