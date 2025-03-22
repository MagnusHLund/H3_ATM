using Hæveautomaten.Entities;
using HæveautomatenTests.Views;
using Hæveautomaten.Controllers;
using HæveautomatenTests.Factories;
using HæveautomatenTests.Repositories;

namespace HæveautomatenTests.Tests.Features
{
    [TestClass]
    public class AccountFeatureTests
    {
        private InMemoryAccountRepository _accountRepository;
        private InMemoryBankRepository _bankRepository;
        private InMemoryPersonRepository _personRepository;
        private MainController _mainController;
        private AdminController _adminController;
        private FakeBaseView _baseView;

        [TestInitialize]
        public void Setup()
        {
            // In-memory repositories for testing
            _accountRepository = new InMemoryAccountRepository();
            _bankRepository = new InMemoryBankRepository();
            _personRepository = new InMemoryPersonRepository();

            // Fake view for simulating user input
            _baseView = new FakeBaseView();

            // Controllers
            var bankController = new BankController(_bankRepository, _baseView);
            var personController = new PersonController(_personRepository, _baseView);
            var accountController = new AccountController(_accountRepository, bankController, personController, _baseView);

            _adminController = new AdminController(
                accountController,
                personController,
                null,
                bankController,
                null,
                _baseView
            );

            _mainController = new MainController(_adminController, null, _baseView);

            BankEntity bank = BankFactory.CreateBank("Test bank");
            _bankRepository.CreateBank(bank);

            PersonEntity person = PersonFactory.CreatePerson();
            _personRepository.CreatePerson(person);
        }

        [TestMethod]
        public void CreateAccountThroughMainMenu_FeatureTest()
        {
            // Arrange
            long balance = 5000;

            PersonEntity accountOwner = _personRepository.GetAllPeople()[0];

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "5", // Create Account
                balance.ToString(), // Balance
                "1", // Select bank
                "1", // Select person
            });

            // Act
            _mainController.HandleMainMenuDisplay();

            // Assert
            List<AccountEntity> accounts = _accountRepository.GetAllAccounts();
            Assert.AreEqual(1, accounts.Count);
            Assert.AreEqual(accountOwner.ToString(), accounts[0].AccountOwner.ToString());
            Assert.AreEqual(balance, accounts[0].BalanceInMinorUnits);
        }

        [TestMethod]
        public void DeleteAccountThroughMainMenu_FeatureTest()
        {
            // Arrange
            AccountEntity account = AccountFactory.CreateAccount();
            _accountRepository.CreateAccount(account);

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "6", // Delete Account
                "1", // Select account
            });

            // Act
            _mainController.HandleMainMenuDisplay();

            // Assert
            List<AccountEntity> accounts = _accountRepository.GetAllAccounts();
            Assert.AreEqual(0, accounts.Count);
        }

        [TestMethod]
        public void CreateAccountWithInvalidBalance_FeatureTest()
        {
            // Arrange
            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "5", // Create Account
                "invalid", // Invalid balance input
            });

            // Act & Assert
            Assert.ThrowsException<FormatException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void CreateAccountWithNegativeBalance_FeatureTest()
        {

            long negativeBalance = -1000;
            _baseView.SetUserInputs(new List<string>
            {
                "1",
                "5",
                negativeBalance.ToString(),
            });


            Assert.ThrowsException<InvalidOperationException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void CreateAccountWithInvalidBalanceInput_FeatureTest()
        {
            // Arrange
            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "5", // Create Account
                "invalid", // Invalid balance input
                "1", // Select bank
                "1", // Select person
            });

            // Act & Assert
            Assert.ThrowsException<FormatException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void DeleteAccountWithNonExistingAccount_FeatureTest()
        {
            // Arrange
            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "6", // Delete Account
                "1", // Non-existing account
            });

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void CreateAccountWithNonExistingBank_FeatureTest()
        {
            // Arrange
            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "5", // Create Account
                "5000", // Balance
                "2", // Non-existing bank
                "1", // Select person
            });

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void CreateAccountWithNonExistingPerson_FeatureTest()
        {
            // Arrange
            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "5", // Create Account
                "5000", // Balance
                "1", // Select bank
                "2", // Non-existing person
            });

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void CreateAccountWithInvalidPerson_FeatureTest()
        {
            // Arrange
            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "5", // Create Account
                "5000", // Balance
                "1", // Select bank
                "invalid", // Invalid person input
            });

            // Act & Assert
            Assert.ThrowsException<FormatException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void CreateAccountWithInvalidBank_FeatureTest()
        {
            // Arrange
            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "5", // Create Account
                "5000", // Balance
                "invalid", // Invalid bank input
                "1", // Select person
            });

            // Act & Assert
            Assert.ThrowsException<FormatException>(() => _mainController.HandleMainMenuDisplay());
        }
    }
}