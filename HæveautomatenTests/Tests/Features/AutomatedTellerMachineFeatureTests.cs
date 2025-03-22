using Hæveautomaten.Entities;
using HæveautomatenTests.Views;
using Hæveautomaten.Controllers;
using HæveautomatenTests.Factories;
using HæveautomatenTests.Repositories;
using Hæveautomaten.Interfaces.Controllers;

namespace HæveautomatenTests.Tests.Features
{
    [TestClass]
    public class AutomatedTellerMachineFeatureTests
    {
        private InMemoryAutomatedTellerMachineRepository _atmRepository;
        private InMemoryBankRepository _bankRepository;
        private FakeBaseView _baseView;
        private MainController _mainController;
        private AdminController _adminController;

        [TestInitialize]
        public void Setup()
        {
            _atmRepository = new InMemoryAutomatedTellerMachineRepository();
            _bankRepository = new InMemoryBankRepository();
            _baseView = new FakeBaseView();

            BankController bankController = new BankController(_bankRepository, _baseView);
            IAccountController accountController = new AccountController(null, bankController, null, _baseView);
            AutomatedTellerMachineController atmController = new AutomatedTellerMachineController(_atmRepository, bankController, null, accountController, _baseView);

            _adminController = new AdminController(
                null,
                null,
                null,
                bankController,
                atmController,
                _baseView
            );

            _mainController = new MainController(_adminController, null, _baseView);

            BankEntity bank = BankFactory.CreateBank("Test Bank");
            _bankRepository.CreateBank(bank);
        }

        [TestMethod]
        public void CreateATMThroughMainMenu_FeatureTest()
        {
            // Arrange
            uint minimumExchangeAmount = 100;

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "9", // Create ATM
                "1", // Select bank
                minimumExchangeAmount.ToString(), // Minimum exchange amount
            });

            // Act
            _mainController.HandleMainMenuDisplay();

            // Assert
            List<AutomatedTellerMachineEntity> atms = _atmRepository.GetAllAutomatedTellerMachines();
            Assert.AreEqual(1, atms.Count);
            Assert.AreEqual(minimumExchangeAmount, atms[0].MinimumExchangeAmount);
            Assert.AreEqual("Test Bank", atms[0].Bank.ToString());
        }

        [TestMethod]
        public void DeleteATMThroughMainMenu_FeatureTest()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            _atmRepository.CreateAutomatedTellerMachine(atm);

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "10", // Delete ATM
                "1" // Select ATM
            });

            // Act
            _mainController.HandleMainMenuDisplay();

            // Assert
            List<AutomatedTellerMachineEntity> atms = _atmRepository.GetAllAutomatedTellerMachines();
            Assert.AreEqual(0, atms.Count);
        }

        [TestMethod]
        public void CreateATMWithInvalidMinimumExchangeAmount_FeatureTest()
        {
            // Arrange
            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "9", // Create ATM
                "invalid", // Invalid minimum exchange amount
                "1" // Select bank
            });

            // Act & Assert
            Assert.ThrowsException<FormatException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void CreateATMWithNegativeMinimumExchangeAmount_FeatureTest()
        {
            // Arrange
            int negativeExchangeAmount = -100;

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "9", // Create ATM
                negativeExchangeAmount.ToString(), // Negative minimum exchange amount
                "1" // Select bank
            });

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void CreateATMWithNonExistingBank_FeatureTest()
        {
            // Arrange
            uint minimumExchangeAmount = 100;

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "9", // Create ATM
                minimumExchangeAmount.ToString(), // Minimum exchange amount
                "2" // Non-existing bank
            });

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void DeleteATMWithNonExistingATM_FeatureTest()
        {
            // Arrange
            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "10", // Delete ATM
                "1" // Non-existing ATM
            });

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void CreateATMWithInvalidBankInput_FeatureTest()
        {
            // Arrange
            uint minimumExchangeAmount = 100;

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "9", // Create ATM
                minimumExchangeAmount.ToString(), // Minimum exchange amount
                "invalid" // Invalid bank input
            });

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _mainController.HandleMainMenuDisplay());
        }
    }
}