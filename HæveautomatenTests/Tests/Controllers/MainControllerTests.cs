using Moq;
using Hæveautomaten.Controllers;
using Hæveautomaten.Interfaces.Views;
using Hæveautomaten.Interfaces.Controllers;

namespace HæveautomatenTests.Tests.Controllers
{
    [TestClass]
    public class MainControllerTests
    {
        private Mock<IAdminController> _adminControllerMock;
        private Mock<IAutomatedTellerMachineController> _atmControllerMock;
        private Mock<IBaseView> _baseViewMock;
        private MainController _mainController;

        [TestInitialize]
        public void Setup()
        {
            _atmControllerMock = new Mock<IAutomatedTellerMachineController>();
            _adminControllerMock = new Mock<IAdminController>();
            _baseViewMock = new Mock<IBaseView>();

            _mainController = new MainController(
                _adminControllerMock.Object,
                _atmControllerMock.Object,
                _baseViewMock.Object
            );
        }

        [TestMethod]
        public void HandleMainMenu_WithValidInput_ExecutesCorrectAction()
        {
            // Arrange
            string input = "1"; // Simulate valid input for Admin menu
            _baseViewMock.Setup(view => view.GetUserInput()).Returns(input);

            // Act
            _mainController.HandleMainMenuDisplay();

            // Assert
            _adminControllerMock.Verify(admin => admin.HandleAdminMenuDisplay(), Times.Once);
        }

        [TestMethod]
        public void HandleMainMenu_WithATMMenuInput_CallsATMMenu()
        {
            // Arrange
            string input = "2"; // Simulate valid input for ATM menu
            _baseViewMock.Setup(view => view.GetUserInput()).Returns(input);

            // Act
            _mainController.HandleMainMenuDisplay();

            // Assert
            _atmControllerMock.Verify(atm => atm.HandleAutomatedTellerMachineMenu(), Times.Once);
        }

        [TestMethod]
        public void HandleMainMenu_WithInvalidInput_ThrowsInvalidOperationException()
        {
            // Arrange
            string input = "invalid"; // Simulate invalid input
            _baseViewMock.Setup(view => view.GetUserInput()).Returns(input);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _mainController.HandleMainMenuDisplay());
        }
    }
}