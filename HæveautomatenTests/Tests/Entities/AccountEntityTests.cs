using Hæveautomaten.Entities;
using HæveautomatenTests.Factories;

namespace HæveautomatenTests.Tests.Entities
{
    [TestClass]
    public class AccountEntityTests
    {
        [TestMethod]
        public void Constructor_WithValidParameters_CreatesAccountEntity()
        {
            // Arrange
            long balanceInMinorUnits = 10000;
            BankEntity bank = BankFactory.CreateBank();
            PersonEntity accountOwner = PersonFactory.CreatePerson();
            List<CreditCardEntity> creditCards = new List<CreditCardEntity>
            {
                CreditCardFactory.CreateCreditCard()
            };

            // Act
            AccountEntity account = new AccountEntity(
                balanceInMinorUnits: balanceInMinorUnits,
                bank: bank,
                accountOwner: accountOwner,
                creditCards: creditCards
            );

            // Assert
            Assert.AreEqual(balanceInMinorUnits, account.BalanceInMinorUnits);
            Assert.AreEqual(bank, account.Bank);
            Assert.AreEqual(accountOwner, account.AccountOwner);
            Assert.AreEqual(creditCards, account.CreditCards);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectStringRepresentation()
        {
            // Arrange
            long balanceInMinorUnits = 10000;
            AccountEntity account = new AccountEntity(balanceInMinorUnits: balanceInMinorUnits);

            // Act
            string result = account.ToString();

            // Assert
            Assert.AreEqual("Account ID: 0 - Balance: 100 DKK", result);
        }
    }
}