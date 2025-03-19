using Moq;
using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces;
using HæveautomatenTests.Factories;

namespace HæveautomatenTests.Tests
{
    [TestClass]
    public class CreditCardControllerTests
    {
        private Mock<ICreditCardController> _creditCardController;

        [TestInitialize]
        public void Setup()
        {
            _creditCardController = new Mock<ICreditCardController>();
        }

        [TestMethod]
        public void IsCreditCardValid_WithValidCard_ReturnsTrue()
        {
            // Arrange
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();

            // Act
            bool isCreditCardValid = _creditCardController.Object.IsCreditCardValid(creditCard);

            // Assert
            Assert.IsTrue(isCreditCardValid);
        }

        [TestMethod]
        public void IsCreditCardValid_WithBlockedCard_ReturnsFalse()
        {
            // Arrange
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard(
                isBlocked: true
            );

            // Act
            bool isCreditCardValid = _creditCardController.Object.IsCreditCardValid(creditCard);

            // Assert
            Assert.IsFalse(isCreditCardValid);
        }

        [TestMethod]
        public void IsCreditCardValid_WithInvalidCardNumber_ReturnsFalse()
        {
            // Arrange
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard(
                cardNumber: 1234567890123456
            );

            // Act
            bool isCreditCardValid = _creditCardController.Object.IsCreditCardValid(creditCard);

            // Assert
            Assert.IsFalse(isCreditCardValid);
        }

        [TestMethod]
        public void IsCreditCardValid_WithExpiredCard_ReturnsFalse()
        {

        }

        [TestMethod]
        public void IsCreditCardValid_WithInvalidCvv_ReturnsFalse()
        {

        }

        [TestMethod]
        public void IsCreditCardValid_WithInvalidPin_ReturnsFalse()
        {

        }

        [TestMethod]
        public void IsCreditCardValid_WithAccountNotFound_ReturnsFalse()
        {

        }
    }
}