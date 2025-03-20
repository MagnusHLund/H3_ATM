using Moq;
using Hæveautomaten.Controllers;
using Hæveautomaten.Interfaces.Controllers;

namespace HæveautomatenTests.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTests
    {
        private AdminController _adminController;
        private Mock<IAccountController> _accountControllerMock;
        private Mock<IPersonController> _personControllerMock;
        private Mock<ICreditCardController> _creditCardControllerMock;
        private Mock<IBankController> _bankControllerMock;
        private Mock<IAutomatedTellerMachineController> _atmControllerMock;

        [TestInitialize]
        public void Setup()
        {
            _accountControllerMock = new Mock<IAccountController>();
            _personControllerMock = new Mock<IPersonController>();
            _creditCardControllerMock = new Mock<ICreditCardController>();
            _bankControllerMock = new Mock<IBankController>();
            _atmControllerMock = new Mock<IAutomatedTellerMachineController>();

            _adminController = new AdminController(
                _accountControllerMock.Object,
                _personControllerMock.Object,
                _creditCardControllerMock.Object,
                _bankControllerMock.Object,
                _atmControllerMock.Object
            );
        }

        [TestMethod]
        public void HandleAdminMenu_WithValidInput_CallsCorrectMethod()
        {
            // Arrange
            string input = "1";

            // Act
            _adminController.HandleAdminMenuInput(input);

            // Assert
            _personControllerMock.Verify(p => p.CreatePerson(), Times.Once);
        }

        [TestMethod]
        public void HandleAdminMenu_WithInvalidInput_ThrowsInvalidOperationException()
        {
            // Arrange
            string input = "invalid";

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _adminController.HandleAdminMenuInput(input));
        }

        [TestMethod]
        public void CreatePerson_CallsPersonControllerCreatePerson()
        {
            // Act
            _adminController.HandleAdminMenuInput("1");

            // Assert
            _personControllerMock.Verify(p => p.CreatePerson(), Times.Once);
        }

        [TestMethod]
        public void DeletePerson_CallsPersonControllerDeletePerson()
        {
            // Act
            _adminController.HandleAdminMenuInput("2");

            // Assert
            _personControllerMock.Verify(p => p.DeletePerson(), Times.Once);
        }

        [TestMethod]
        public void CreateAccount_CallsAccountControllerCreateAccount()
        {
            // Act
            _adminController.HandleAdminMenuInput("5");

            // Assert
            _accountControllerMock.Verify(a => a.CreateAccount(), Times.Once);
        }

        [TestMethod]
        public void DeleteAccount_CallsAccountControllerDeleteAccount()
        {
            // Act
            _adminController.HandleAdminMenuInput("6");

            // Assert
            _accountControllerMock.Verify(a => a.DeleteAccount(), Times.Once);
        }

        [TestMethod]
        public void CreateBank_CallsBankControllerCreateBank()
        {
            // Act
            _adminController.HandleAdminMenuInput("7");

            // Assert
            _bankControllerMock.Verify(b => b.CreateBank(), Times.Once);
        }

        [TestMethod]
        public void DeleteBank_CallsBankControllerDeleteBank()
        {
            // Act
            _adminController.HandleAdminMenuInput("8");

            // Assert
            _bankControllerMock.Verify(b => b.DeleteBank(), Times.Once);
        }

        [TestMethod]
        public void CreateAutomatedTellerMachine_CallsATMControllerCreateAutomatedTellerMachine()
        {
            // Act
            _adminController.HandleAdminMenuInput("9");

            // Assert
            _atmControllerMock.Verify(atm => atm.CreateAutomatedTellerMachine(), Times.Once);
        }

        [TestMethod]
        public void DeleteAutomatedTellerMachine_CallsATMControllerDeleteAutomatedTellerMachine()
        {
            // Act
            _adminController.HandleAdminMenuInput("10");

            // Assert
            _atmControllerMock.Verify(atm => atm.DeleteAutomatedTellerMachine(), Times.Once);
        }

        [TestMethod]
        public void HandleAdminMenu_WithExitInput_DoesNotThrowException()
        {
            // Arrange
            string input = "0";

            // Act & Assert
            _adminController.HandleAdminMenuInput(input);
        }
    }
}