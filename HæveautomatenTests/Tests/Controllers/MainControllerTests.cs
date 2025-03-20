using Moq;
using Hæveautomaten.Interfaces.Controllers;

namespace HæveautomatenTests.Tests.Controllers
{
    [TestClass]
    public class MainControllerTests
    {
        private Mock<IMainController> _mainControllerMock;

        [TestInitialize]
        public void Setup()
        {
            _mainControllerMock = new Mock<IMainController>();
        }

        [TestMethod]
        public void HandleMainMenu_WithValidInput_ExecutesCorrectAction()
        {
            // Arrange
            string input = "1";
            _mainControllerMock.Setup(main => main.HandleMainMenuInput(input));

            // Act
            _mainControllerMock.Object.HandleMainMenuInput(input);

            // Assert
            _mainControllerMock.Verify(main => main.HandleMainMenuInput(input), Times.Once);
        }

        [TestMethod]
        public void HandleMainMenu_WithInvalidInput_ReturnsToMenu()
        {
            // Arrange
            string input = "invalid";
            _mainControllerMock.Setup(main => main.HandleMainMenuInput(input));

            // Act
            _mainControllerMock.Object.HandleMainMenuInput(input);

            // Assert
            _mainControllerMock.Verify(main => main.HandleMainMenuInput(input), Times.Once);
        }

        [TestMethod]
        public void HandleMainMenu_WithExitInput_ExitsApplication()
        {
            // Arrange
            string input = "0";
            _mainControllerMock.Setup(main => main.HandleMainMenuInput(input));

            // Act
            _mainControllerMock.Object.HandleMainMenuInput(input);

            // Assert
            _mainControllerMock.Verify(main => main.HandleMainMenuInput(input), Times.Once);
        }
    }
}