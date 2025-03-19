using Moq;
using Hæveautomaten.Models;
using Hæveautomaten.Factories;
using Hæveautomaten.Interfaces;
using HæveautomatenTests.Factories;


namespace Hæveautomaten.Tests
{
    [TestClass]
    public class CreditCardTests
    {
        private Mock<ICardController> _cardController;

        [TestInitialize]
        public void Setup()
        {
            _cardController = new Mock<ICardController>();
        }

        [TestMethod]
        public void IsCreditCardValid_WithValidCard_ReturnsTrue()
        {
            // Arrange
            Bank bank = BankFactory.CreateBank();
            Person person = PersonFactory.CreatePerson();
            Account account = AccountFactory.CreateAccount(person, bank);
            CreditCard creditCard = CreditCardFactory.CreateValidCreditCard(person, account);

            _cardController.Setup(x => x.IsCreditCardValid(creditCard)).Returns(true);

            // Act
            bool isCreditCardValid = _cardController.Object.IsCreditCardValid(creditCard);

            // Assert
            Assert.IsTrue(isCreditCardValid);
        }

        [TestMethod]
        public void IsCreditCardValid_WithBlockedCard_ReturnsFalse()
        {
            // Arrange
            Bank bank = BankFactory.CreateBank();
            Person person = PersonFactory.CreatePerson();
            Account account = AccountFactory.CreateAccount(person, bank);
            CreditCard creditCard = CreditCardFactory.CreateBlockedCreditCard(person, account);

            _cardController.Setup(x => x.IsCreditCardValid(creditCard)).Returns(false);

            // Act
            bool isCreditCardValid = _cardController.Object.IsCreditCardValid(creditCard);

            // Assert
            Assert.IsFalse(isCreditCardValid);
        }

        [TestMethod]
        public void IsCreditCardValid_WithInvalidCardNumber_ReturnsFalse()
        {
            // Arrange
            Bank bank = BankFactory.CreateBank();
            Person person = PersonFactory.CreatePerson();
            Account account = AccountFactory.CreateAccount(person, bank);
            CreditCard creditCard = CreditCardFactory.CreateValidCreditCard(person, account);

            _cardController.Setup(x => x.IsCreditCardValid(creditCard)).Returns(false);

            // Act
            bool isCreditCardValid = _cardController.Object.IsCreditCardValid(creditCard);

            // Assert
            Assert.IsFalse(isCreditCardValid);
        }
    }
}