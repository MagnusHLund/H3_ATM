using Hæveautomaten.Entities;
using HæveautomatenTests.Factories;

namespace HæveautomatenTests.Tests.Entities
{
    [TestClass]
    public class AutomatedTellerMachineEntityTests
    {
        [TestMethod]
        public void Constructor_WithValidParameters_CreatesAutomatedTellerMachineEntity()
        {
            // Arrange
            uint minimumExchangeAmount = 100;
            BankEntity bank = BankFactory.CreateBank();

            // Act
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();

            // Assert
            Assert.AreEqual(minimumExchangeAmount, atm.MinimumExchangeAmount);
            Assert.AreEqual(bank, atm.Bank);
            Assert.AreEqual<uint>(0, atm.AutomatedTellerMachineId);
        }

        [TestMethod]
        public void Constructor_WithNullBank_CreatesAutomatedTellerMachineEntity()
        {
            // Arrange
            uint minimumExchangeAmount = 100;

            // Act
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine(associatedBank: null);

            // Assert
            Assert.AreEqual(minimumExchangeAmount, atm.MinimumExchangeAmount);
            Assert.IsNull(atm.Bank);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectStringRepresentation()
        {
            // Arrange
            uint minimumExchangeAmount = 100;
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine(minimumExchangeAmount: minimumExchangeAmount);

            // Act
            string result = atm.ToString();

            // Assert
            Assert.AreEqual("ATM ID: 0 - Minimum exchange amount: 100 DKK", result);
        }
    }
}