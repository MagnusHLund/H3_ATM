using Moq;
using Hæveautomaten.Controllers;
using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Entities;
using HæveautomatenTests.Factories;

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
            _adminController.HandleAdminMenu(input);

            // Assert
            _personControllerMock.Verify(p => p.CreatePerson(It.IsAny<PersonEntity>()), Times.Once);
        }

        [TestMethod]
        public void HandleAdminMenu_WithInvalidInput_ThrowsArgumentException()
        {
            // Arrange
            string input = "invalid";

            // Act & assert
            Assert.ThrowsException<ArgumentException>(() => _adminController.HandleAdminMenu(input));

        }

        [TestMethod]
        public void CreatePerson_CallsPersonControllerCreatePerson()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson();

            // Act
            _adminController.CreatePerson();

            // Assert
            _personControllerMock.Verify(p => p.CreatePerson(It.IsAny<PersonEntity>()), Times.Once);
        }

        [TestMethod]
        public void DeletePerson_CallsPersonControllerDeletePerson()
        {
            // Act
            _adminController.DeletePerson();

            // Assert
            _personControllerMock.Verify(p => p.DeletePerson(It.IsAny<PersonEntity>()), Times.Once);
        }

        [TestMethod]
        public void CreateAccount_CallsAccountControllerCreateAccount()
        {
            // Act
            _adminController.CreateAccount();

            // Assert
            _accountControllerMock.Verify(a => a.CreateAccount(It.IsAny<AccountEntity>()), Times.Once);
        }

        [TestMethod]
        public void DeleteAccount_CallsAccountControllerDeleteAccount()
        {
            // Act
            _adminController.DeleteAccount();

            // Assert
            _accountControllerMock.Verify(a => a.DeleteAccount(It.IsAny<AccountEntity>()), Times.Once);
        }

        [TestMethod]
        public void CreateBank_CallsBankControllerCreateBank()
        {
            // Act
            _adminController.CreateBank();

            // Assert
            _bankControllerMock.Verify(b => b.CreateBank(It.IsAny<BankEntity>()), Times.Once);
        }

        [TestMethod]
        public void DeleteBank_CallsBankControllerDeleteBank()
        {
            // Act
            _adminController.DeleteBank();

            // Assert
            _bankControllerMock.Verify(b => b.DeleteBank(It.IsAny<BankEntity>()), Times.Once);
        }

        [TestMethod]
        public void CreateAutomatedTellerMachine_CallsATMControllerCreateATM()
        {
            // Act
            _adminController.CreateAutomatedTellerMachine();

            // Assert
            _atmControllerMock.Verify(atm => atm.CreateAutomatedTellerMachine(It.IsAny<AutomatedTellerMachineEntity>()), Times.Once);
        }

        [TestMethod]
        public void DeleteAutomatedTellerMachine_CallsATMControllerDeleteATM()
        {
            // Act
            _adminController.DeleteAutomatedTellerMachine();

            // Assert
            _atmControllerMock.Verify(atm => atm.DeleteAutomatedTellerMachine(It.IsAny<AutomatedTellerMachineEntity>()), Times.Once);
        }
    }
}