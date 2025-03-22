using Hæveautomaten.Entities;
using HæveautomatenTests.Views;
using Hæveautomaten.Controllers;
using HæveautomatenTests.Factories;
using HæveautomatenTests.Repositories;

namespace HæveautomatenTests.Tests.Features
{
    [TestClass]
    public class BankFeatureTests
    {
        private InMemoryBankRepository _bankRepository;
        private FakeBaseView _baseView;
        private MainController _mainController;
        private AdminController _adminController;

        [TestInitialize]
        public void Setup()
        {
            _bankRepository = new InMemoryBankRepository();
            _baseView = new FakeBaseView();

            BankController bankController = new BankController(_bankRepository, _baseView);

            _adminController = new AdminController(
                null,
                null,
                null,
                bankController,
                null,
                _baseView
            );

            _mainController = new MainController(_adminController, null, _baseView);
        }

        [TestMethod]
        public void CreateBankThroughMainMenu_FeatureTest()
        {
            // Arrange
            string bankName = "New Bank";

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "7", // Create Bank
                bankName // Bank name
            });

            // Act
            _mainController.HandleMainMenuDisplay();

            // Assert
            List<BankEntity> banks = _bankRepository.GetAllBanks();
            Assert.AreEqual(1, banks.Count);
            Assert.AreEqual(bankName, banks[0].ToString());
        }

        [TestMethod]
        public void DeleteBankThroughMainMenu_FeatureTest()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank("Test Bank");
            _bankRepository.CreateBank(bank);

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "8", // Delete Bank
                "1" // Select bank
            });

            // Act
            _mainController.HandleMainMenuDisplay();

            // Assert
            List<BankEntity> banks = _bankRepository.GetAllBanks();
            Assert.AreEqual(0, banks.Count);
        }

        [TestMethod]
        public void CreateBankWithEmptyName_FeatureTest()
        {
            // Arrange
            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "7", // Create Bank
                "" // Empty bank name
            });

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void CreateBankWithDuplicateName_FeatureTest()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank("Duplicate Bank");
            _bankRepository.CreateBank(bank);

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "7", // Create Bank
                "Duplicate Bank" // Duplicate bank name
            });

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void DeleteBankWithNonExistingBank_FeatureTest()
        {
            // Arrange
            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "8", // Delete Bank
                "1" // Non-existing bank
            });

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void CreateBankWithInvalidInput_FeatureTest()
        {
            // Arrange
            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "7", // Create Bank
                "invalid" // Invalid input for bank name
            });

            // Act
            _mainController.HandleMainMenuDisplay();

            // Assert
            List<BankEntity> banks = _bankRepository.GetAllBanks();
            Assert.AreEqual(1, banks.Count); 
        }

        [TestMethod]
        public void DeleteBankWithInvalidInput_FeatureTest()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank("Test Bank");
            _bankRepository.CreateBank(bank);

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "8", // Delete Bank
                "invalid" // Invalid input for bank selection
            });

            // Act & Assert
            Assert.ThrowsException<FormatException>(() => _mainController.HandleMainMenuDisplay());
        }
    }
}