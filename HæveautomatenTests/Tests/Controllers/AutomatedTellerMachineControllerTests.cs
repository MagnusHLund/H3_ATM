using Moq;
using Hæveautomaten.Entities;
using HæveautomatenTests.Factories;
using Hæveautomaten.Interfaces.Controllers;
namespace HæveautomatenTests.Tests.Controllers
{
    [TestClass]
    public class AutomatedTellerMachineControllerTests
    {
        private Mock<IAutomatedTellerMachineController> _atmControllerMock;

        [TestInitialize]
        public void Setup()
        {
            _atmControllerMock = new Mock<IAutomatedTellerMachineController>();
        }

        [TestMethod]
        public void HandleAutomatedTellerMachineMenu_WithValidInput_ExecutesSuccessfully()
        {
            // Arrange
            _atmControllerMock.Setup(atm => atm.GetAllAutomatedTellerMachines()).Returns(new List<AutomatedTellerMachineEntity>());

            // Act
            _atmControllerMock.Object.HandleAutomatedTellerMachineMenu();

            // Assert
            _atmControllerMock.Verify(atm => atm.GetAllAutomatedTellerMachines(), Times.Once);
        }

        [TestMethod]
        public void HandleAutomatedTellerMachineMenu_WithInvalidInput_ReturnsToMainMenu()
        {
        }

        [TestMethod]
        public void CreateAutomatedTellerMachine_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            _atmControllerMock.Setup(atmController => atmController.CreateAutomatedTellerMachine(atm)).Returns(true);

            // Act
            bool result = _atmControllerMock.Object.CreateAutomatedTellerMachine(atm);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateAutomatedTellerMachine_WithInvalidData_ThrowsException()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            _atmControllerMock.Setup(atmController => atmController.CreateAutomatedTellerMachine(atm)).Throws(new Exception("Invalid data"));

            // Act & Assert
            Assert.ThrowsException<Exception>(() => _atmControllerMock.Object.CreateAutomatedTellerMachine(atm));
        }

        [TestMethod]
        public void DeleteAutomatedTellerMachine_WithExistingATM_DeletesSuccessfully()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            _atmControllerMock.Setup(atmController => atmController.DeleteAutomatedTellerMachine(atm)).Returns(true);

            // Act
            bool result = _atmControllerMock.Object.DeleteAutomatedTellerMachine(atm);

            // Assert
            Assert.IsTrue(result);
            _atmControllerMock.Verify(atmController => atmController.DeleteAutomatedTellerMachine(atm), Times.Once);
        }

        [TestMethod]
        public void DeleteAutomatedTellerMachine_WithNonExistingATM_ThrowsException()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            _atmControllerMock.Setup(atmController => atmController.DeleteAutomatedTellerMachine(atm)).Throws(new Exception("ATM not found"));

            // Act & Assert
            Assert.ThrowsException<Exception>(() => _atmControllerMock.Object.DeleteAutomatedTellerMachine(atm));
        }

        [TestMethod]
        public void UseAutomatedTellerMachine_WithValidATM_ExecutesSuccessfully()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();

            // Act
            _atmControllerMock.Object.UseAutomatedTellerMachine(atm);

            // Assert
            _atmControllerMock.Verify(atmController => atmController.UseAutomatedTellerMachine(atm), Times.Once);
        }

        [TestMethod]
        public void UseAutomatedTellerMachine_WithNullATM_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _atmControllerMock.Object.UseAutomatedTellerMachine(null));
        }

        [TestMethod]
        public void SwitchAutomatedTellerMachine_WithMultipleATMs_ReturnsSelectedATM()
        {
            // Arrange
            List<AutomatedTellerMachineEntity> atms = new List<AutomatedTellerMachineEntity>
            {
                AutomatedTellerMachineFactory.CreateAutomatedTellerMachine(),
                AutomatedTellerMachineFactory.CreateAutomatedTellerMachine()
            };
            _atmControllerMock.Setup(atmController => atmController.SwitchAutomatedTellerMachine(atms)).Returns(atms[1]);

            // Act
            AutomatedTellerMachineEntity selectedATM = _atmControllerMock.Object.SwitchAutomatedTellerMachine(atms);

            // Assert
            Assert.AreEqual(atms[1], selectedATM);
            _atmControllerMock.Verify(atmController => atmController.SwitchAutomatedTellerMachine(atms), Times.Once);
        }

        [TestMethod]
        public void SwitchAutomatedTellerMachine_WithEmptyATMList_ThrowsException()
        {
            // Arrange
            List<AutomatedTellerMachineEntity> atms = new List<AutomatedTellerMachineEntity>();
            _atmControllerMock.Setup(atmController => atmController.SwitchAutomatedTellerMachine(atms)).Throws(new Exception("No ATMs available"));

            // Act & Assert
            Assert.ThrowsException<Exception>(() => _atmControllerMock.Object.SwitchAutomatedTellerMachine(atms));
        }

        [TestMethod]
        public void GetAllAutomatedTellerMachines_WhenCalled_ReturnsAllATMs()
        {
            // Arrange
            List<AutomatedTellerMachineEntity> atms = new List<AutomatedTellerMachineEntity>
            {
                AutomatedTellerMachineFactory.CreateAutomatedTellerMachine(),
                AutomatedTellerMachineFactory.CreateAutomatedTellerMachine()
            };
            _atmControllerMock.Setup(atmController => atmController.GetAllAutomatedTellerMachines()).Returns(atms);

            // Act
            List<AutomatedTellerMachineEntity> result = _atmControllerMock.Object.GetAllAutomatedTellerMachines();

            // Assert
            Assert.AreEqual(atms.Count, result.Count);
            _atmControllerMock.Verify(atmController => atmController.GetAllAutomatedTellerMachines(), Times.Once);
        }

        [TestMethod]
        public void DepositMoney_WithValidATMAndCard_UpdatesBalance()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();

            // Act
            _atmControllerMock.Object.DepositMoney(atm, creditCard);

            // Assert
            _atmControllerMock.Verify(atmController => atmController.DepositMoney(atm, creditCard), Times.Once);
        }

        [TestMethod]
        public void DepositMoney_WithNullATM_ThrowsArgumentNullException()
        {
            // Arrange
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _atmControllerMock.Object.DepositMoney(null, creditCard));
        }

        [TestMethod]
        public void DepositMoney_WithNullCreditCard_ThrowsArgumentNullException()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _atmControllerMock.Object.DepositMoney(atm, null));
        }

        [TestMethod]
        public void WithdrawMoney_WithValidATMAndCard_UpdatesBalance()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();

            // Act
            _atmControllerMock.Object.WithdrawMoney(atm, creditCard);

            // Assert
            _atmControllerMock.Verify(atmController => atmController.WithdrawMoney(atm, creditCard), Times.Once);
        }

        [TestMethod]
        public void WithdrawMoney_WithInsufficientFunds_ThrowsException()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();
            _atmControllerMock.Setup(atmController => atmController.WithdrawMoney(atm, creditCard)).Throws(new Exception("Insufficient funds"));

            // Act & Assert
            Assert.ThrowsException<Exception>(() => _atmControllerMock.Object.WithdrawMoney(atm, creditCard));
        }

        [TestMethod]
        public void WithdrawMoney_WithNullATM_ThrowsArgumentNullException()
        {
            // Arrange
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _atmControllerMock.Object.WithdrawMoney(null, creditCard));
        }

        [TestMethod]
        public void WithdrawMoney_WithNullCreditCard_ThrowsArgumentNullException()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _atmControllerMock.Object.WithdrawMoney(atm, null));
        }
    }
}