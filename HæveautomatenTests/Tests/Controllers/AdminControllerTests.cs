using Moq;
using Hæveautomaten.Controllers;
using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Interfaces.Views;

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
        private Mock<IBaseView> _baseViewMock;

        [TestInitialize]
        public void Setup()
        {
            _accountControllerMock = new Mock<IAccountController>();
            _personControllerMock = new Mock<IPersonController>();
            _creditCardControllerMock = new Mock<ICreditCardController>();
            _bankControllerMock = new Mock<IBankController>();
            _atmControllerMock = new Mock<IAutomatedTellerMachineController>();
            _baseViewMock = new Mock<IBaseView>();

            _adminController = new AdminController(
                _accountControllerMock.Object,
                _personControllerMock.Object,
                _creditCardControllerMock.Object,
                _bankControllerMock.Object,
                _atmControllerMock.Object,
                _baseViewMock.Object
            );
        }

        [TestMethod]
        public void HandleAdminMenu_WithValidInput_CallsCorrectMethod()
        {
            // Arrange
            string input = "1";
            _baseViewMock.Setup(view => view.GetUserInput()).Returns(input);

            // Act
            _adminController.HandleAdminMenuDisplay();

            // Assert
            _personControllerMock.Verify(p => p.CreatePerson(), Times.Once);
        }

        [TestMethod]
        public void HandleAdminMenu_WithInvalidInput_ThrowsInvalidOperationException()
        {
            // Arrange
            string input = "invalid";
            _baseViewMock.Setup(view => view.GetUserInput()).Returns(input);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _adminController.HandleAdminMenuDisplay());
        }

        [TestMethod]
        public void CreatePerson_CallsPersonControllerCreatePerson()
        {
            // Arrange
            string input = "1";
            _baseViewMock.Setup(view => view.GetUserInput()).Returns(input);

            // Act
            _adminController.HandleAdminMenuDisplay();

            // Assert
            _personControllerMock.Verify(p => p.CreatePerson(), Times.Once);
        }

        [TestMethod]
        public void DeletePerson_CallsPersonControllerDeletePerson()
        {
            // Arrange
            string input = "2";
            _baseViewMock.Setup(view => view.GetUserInput()).Returns(input);

            // Act
            _adminController.HandleAdminMenuDisplay();

            // Assert
            _personControllerMock.Verify(p => p.DeletePerson(), Times.Once);
        }

        [TestMethod]
        public void CreateAccount_CallsAccountControllerCreateAccount()
        {
            // Arrange
            string input = "5";
            _baseViewMock.Setup(view => view.GetUserInput()).Returns(input);

            // Act
            _adminController.HandleAdminMenuDisplay();

            // Assert
            _accountControllerMock.Verify(a => a.CreateAccount(), Times.Once);
        }

        [TestMethod]
        public void DeleteAccount_CallsAccountControllerDeleteAccount()
        {
            // Arrange
            string input = "6";
            _baseViewMock.Setup(view => view.GetUserInput()).Returns(input);

            // Act
            _adminController.HandleAdminMenuDisplay();

            // Assert
            _accountControllerMock.Verify(a => a.DeleteAccount(), Times.Once);
        }

        [TestMethod]
        public void CreateBank_CallsBankControllerCreateBank()
        {
            // Arrange
            string input = "7";
            _baseViewMock.Setup(view => view.GetUserInput()).Returns(input);

            // Act
            _adminController.HandleAdminMenuDisplay();

            // Assert
            _bankControllerMock.Verify(b => b.CreateBank(), Times.Once);
        }

        [TestMethod]
        public void DeleteBank_CallsBankControllerDeleteBank()
        {
            // Arrange
            string input = "8";
            _baseViewMock.Setup(view => view.GetUserInput()).Returns(input);

            // Act
            _adminController.HandleAdminMenuDisplay();

            // Assert
            _bankControllerMock.Verify(b => b.DeleteBank(), Times.Once);
        }

        [TestMethod]
        public void CreateAutomatedTellerMachine_CallsATMControllerCreateAutomatedTellerMachine()
        {
            // Arrange
            string input = "9";
            _baseViewMock.Setup(view => view.GetUserInput()).Returns(input);

            // Act
            _adminController.HandleAdminMenuDisplay();

            // Assert
            _atmControllerMock.Verify(atm => atm.CreateAutomatedTellerMachine(), Times.Once);
        }

        [TestMethod]
        public void DeleteAutomatedTellerMachine_CallsATMControllerDeleteAutomatedTellerMachine()
        {
            // Arrange
            string input = "10";
            _baseViewMock.Setup(view => view.GetUserInput()).Returns(input);

            // Act
            _adminController.HandleAdminMenuDisplay();

            // Assert
            _atmControllerMock.Verify(atm => atm.DeleteAutomatedTellerMachine(), Times.Once);
        }

        [TestMethod]
        public void HandleAdminMenu_WithExitInput_DoesNotThrowException()
        {
            // Arrange
            string input = "0";
            _baseViewMock.Setup(view => view.GetUserInput()).Returns(input);

            // Act & Assert
            _adminController.HandleAdminMenuDisplay();
        }
    }
}