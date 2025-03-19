using Hæveautomaten.Entities;
using HæveautomatenTests.Factories;

namespace HæveautomatenTests.Tests
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
            BankEntity bank = BankFactory.CreateBank();
            PersonEntity person = PersonFactory.CreatePerson();
            AccountEntity account = AccountFactory.CreateAccount(person, bank);
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine(bank);

            // Act
            atm.DepositMoney(account, 100);

            // Assert
            Assert.AreEqual(1100, account.BalanceInMinorUnits);
        }

        [TestMethod]
        public void DepositMoney_WithLessThanMinimumExchangeAmount_ThrowsInvalidOperationException()
        {
            // Assert
            BankEntity bank = BankFactory.CreateBank();
            PersonEntity person = PersonFactory.CreatePerson();
            AccountEntity account = AccountFactory.CreateAccount(person, bank);
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine(bank);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => atm.DepositMoney(account, 4));
        }
    }
}