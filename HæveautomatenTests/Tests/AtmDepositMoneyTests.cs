using Hæveautomaten.Factories;
using Hæveautomaten.Models;
using HæveautomatenTests.Factories;

namespace Hæveautomaten.Tests
{
    [TestClass]
    public class AtmDepositMoneyTests
    {
        [TestInitialize]
        public void Setup()
        {

        }

        [TestMethod]
        public void DepositMoney_WithValidParameters_ReturnsUpdatedAccountBalance()
        {
            // Assert
            Bank bank = BankFactory.CreateBank();
            Person person = PersonFactory.CreatePerson();
            Account account = AccountFactory.CreateAccount(person, bank);
            AutomatedTellerMachine atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine(bank);

            // Act
            atm.DepositMoney(account, 100);

            // Assert
            Assert.AreEqual(1100, account.Balance);
        }

        [TestMethod]
        public void DepositMoney_WithLessThanMinimumExchangeAmount_ThrowsInvalidOperationException()
        {
            // Assert
            Bank bank = BankFactory.CreateBank();
            Person person = PersonFactory.CreatePerson();
            Account account = AccountFactory.CreateAccount(person, bank);
            AutomatedTellerMachine atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine(bank);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => atm.DepositMoney(account, 4));
        }
    }
}