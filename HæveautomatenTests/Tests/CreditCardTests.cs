using Hæveautomaten.Entities;
using HæveautomatenTests.Factories;


namespace HæveautomatenTests.Tests
{
    [TestClass]
    public class CreditCardTests
    {

        [TestMethod]
        public void IsCreditCardValid_WithValidCard_ReturnsTrue()
        {
            // Arrange
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();

            // Act

            // Assert
        }

        [TestMethod]
        public void IsCreditCardValid_WithBlockedCard_ReturnsFalse()
        {
            // Arrange
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard(
                isBlocked: true
            );

            // Act


            // Assert
        }

        [TestMethod]
        public void IsCreditCardValid_WithInvalidCardNumber_ReturnsFalse()
        {
            // Arrange
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard(
                cardNumber: 1234567890123456
            );

            // Act


            // Assert
        }

        [TestMethod]
        public void IsCreditCardValid_WithExpiredCreditCard_ReturnsFalse()
        {
        }

        [TestMethod]
        public void IsCreditCardValid_WithInvalidCvv_ReturnsFalse()
        {
        }

        [TestMethod]
        public void IsCreditCardValid_WithIncorrectPin_ReturnsFalse()
        {
        }

        [TestMethod]
        public void IsCreditCardValid_WithAccountNotFound_ReturnsFalse()
        {
        }
    }
}