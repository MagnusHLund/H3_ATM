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
            AccountEntity account = AccountFactory.CreateAccount(
                balanceInMinorUnits: balanceInMinorUnits,
                associatedBank: bank,
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
        public void Constructor_WithDefaultParameters_CreatesAccountEntityWithDefaults()
        {
            // Act
            AccountEntity account = AccountFactory.CreateAccount();

            // Assert
            Assert.AreEqual(0, account.BalanceInMinorUnits);
            Assert.IsNull(account.Bank);
            Assert.IsNull(account.AccountOwner);
            Assert.IsNotNull(account.CreditCards);
            Assert.AreEqual(0, account.CreditCards.Count);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectStringRepresentation()
        {
            // Arrange
            long balanceInMinorUnits = 10000;
            AccountEntity account = AccountFactory.CreateAccount(balanceInMinorUnits: balanceInMinorUnits);

            // Act
            string result = account.ToString();

            // Assert
            Assert.AreEqual("Account ID: 0 - Balance: 100 DKK", result);
        }
    }
}