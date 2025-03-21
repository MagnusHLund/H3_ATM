using Hæveautomaten.Entities;

namespace HæveautomatenTests.Tests.Entities
{
    [TestClass]
    public class AutomatedTellerMachineEntityTests
    {
        [TestMethod]
        public void Constructor_WithValidParameters_CreatesAutomatedTellerMachineEntity()
        {
            // Arrange
            uint minimumExchangeAmount = 10;
            BankEntity bank = new BankEntity("Test Bank");

            // Act
            AutomatedTellerMachineEntity atm = new AutomatedTellerMachineEntity(
                minimumExchangeAmount: minimumExchangeAmount,
                bank: bank
            );

            // Assert
            Assert.AreEqual(minimumExchangeAmount, atm.MinimumExchangeAmount);
            Assert.AreEqual(bank, atm.Bank);
            Assert.AreEqual(0, atm.AutomatedTellerMachineId);
        }

        [TestMethod]
        public void Constructor_WithNullBank_CreatesAutomatedTellerMachineEntity()
        {
            // Arrange
            uint minimumExchangeAmount = 100;

            // Act
            AutomatedTellerMachineEntity atm = new AutomatedTellerMachineEntity(
                minimumExchangeAmount: minimumExchangeAmount,
                bank: null
            );

            // Assert
            Assert.AreEqual(minimumExchangeAmount, atm.MinimumExchangeAmount);
            Assert.IsNull(atm.Bank);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectStringRepresentation()
        {
            // Arrange
            uint minimumExchangeAmount = 100;
            AutomatedTellerMachineEntity atm = new AutomatedTellerMachineEntity(
                minimumExchangeAmount: minimumExchangeAmount,
                bank: null
            );

            // Act
            string result = atm.ToString();

            // Assert
            Assert.AreEqual("ATM ID: 0 - Minimum exchange amount: 100 DKK", result);
        }
    }
}