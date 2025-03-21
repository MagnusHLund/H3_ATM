using Hæveautomaten.Entities;
using HæveautomatenTests.Factories;

namespace HæveautomatenTests.Tests.Entities
{
    [TestClass]
    public class BankEntityTests
    {
        [TestMethod]
        public void Constructor_WithValidParameters_CreatesBankEntity()
        {
            // Arrange
            string bankName = "Test Bank";

            // Act
            BankEntity bank = new BankEntity(bankName);

            // Assert
            Assert.AreEqual(bankName, bank.BankName);
            Assert.IsNotNull(bank.Accounts);
            Assert.AreEqual(0, bank.Accounts.Count);
            Assert.IsNotNull(bank.AutomatedTellerMachines);
            Assert.AreEqual(0, bank.AutomatedTellerMachines.Count);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectStringRepresentation()
        {
            // Arrange
            string bankName = "Test Bank";
            BankEntity bank = new BankEntity(bankName);

            // Act
            string result = bank.ToString();

            // Assert
            Assert.AreEqual(bankName, result);
        }

        [TestMethod]
        public void AccountsProperty_CanAddAccounts()
        {
            // Arrange
            string bankName = "Test Bank";
            BankEntity bank = new BankEntity(bankName);
            AccountEntity account = AccountFactory.CreateAccount();

            // Act
            bank.Accounts.Add(account);

            // Assert
            Assert.AreEqual(1, bank.Accounts.Count);
            Assert.AreEqual(account, bank.Accounts[0]);
        }

        [TestMethod]
        public void AutomatedTellerMachinesProperty_CanAddATMs()
        {
            // Arrange
            string bankName = "Test Bank";
            BankEntity bank = new BankEntity(bankName);
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();

            // Act
            bank.AutomatedTellerMachines.Add(atm);

            // Assert
            Assert.AreEqual(1, bank.AutomatedTellerMachines.Count);
            Assert.AreEqual(atm, bank.AutomatedTellerMachines[0]);
        }
    }
}