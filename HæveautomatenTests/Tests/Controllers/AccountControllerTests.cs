using Moq;
using Hæveautomaten.Entities;
using Hæveautomaten.Controllers;
using HæveautomatenTests.Factories;
using Hæveautomaten.Interfaces.Views;
using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Interfaces.Repositories;

namespace HæveautomatenTests.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        private Mock<IAccountRepository> _mockAccountRepository;
        private Mock<IBankController> _mockBankController;
        private Mock<IPersonController> _mockPersonController;
        private Mock<IBaseView> _mockBaseView;
        private AccountController _accountController;

        [TestInitialize]
        public void Setup()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockBankController = new Mock<IBankController>();
            _mockPersonController = new Mock<IPersonController>();
            _mockBaseView = new Mock<IBaseView>();

            _accountController = new AccountController(
                _mockAccountRepository.Object,
                _mockBankController.Object,
                _mockPersonController.Object,
                _mockBaseView.Object
            );
        }

        [TestMethod]
        public void CreateAccount_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            long balance = 10000;
            BankEntity bank = BankFactory.CreateBank();
            PersonEntity person = PersonFactory.CreatePerson();
            AccountEntity account = AccountFactory.CreateAccount(person, bank, balanceInMinorUnits: balance);

            _mockBaseView.Setup(view => view.GetUserInputWithTitle("Enter balance:")).Returns(balance.ToString());
            _mockBankController.Setup(b => b.SelectBank()).Returns(bank);
            _mockPersonController.Setup(p => p.SelectPerson()).Returns(person);
            _mockAccountRepository.Setup(r => r.CreateAccount(It.IsAny<AccountEntity>())).Returns(true);

            // Act
            bool result = _accountController.CreateAccount();

            // Assert
            Assert.IsTrue(result);
            _mockAccountRepository.Verify(r => r.CreateAccount(It.Is<AccountEntity>(a =>
                a.BalanceInMinorUnits == balance &&
                a.Bank == bank &&
                a.AccountOwner == person
            )), Times.Once);
        }

        [TestMethod]
        public void CreateAccount_WithoutBank_ThrowsArgumentNullException()
        {
            // Arrange
            _mockBaseView.Setup(view => view.GetUserInputWithTitle("Enter balance: ")).Returns("10000");
            _mockBankController.Setup(b => b.SelectBank()).Returns((BankEntity)null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _accountController.CreateAccount());
        }

        [TestMethod]
        public void CreateAccount_WithoutOwner_ThrowsArgumentNullException()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank();
            _mockBaseView.Setup(view => view.GetUserInputWithTitle("Enter balance: ")).Returns("10000");
            _mockBankController.Setup(b => b.SelectBank()).Returns(bank);
            _mockPersonController.Setup(p => p.SelectPerson()).Returns((PersonEntity)null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _accountController.CreateAccount());
        }

        [TestMethod]
        public void DeleteAccount_WithExistingAccount_DeletesSuccessfully()
        {
            // Arrange
            AccountEntity account = AccountFactory.CreateAccount();
            _mockAccountRepository.Setup(r => r.DeleteAccount(account.AccountId)).Returns(true);
            _mockAccountRepository.Setup(r => r.GetAllAccounts()).Returns(new List<AccountEntity> { account });
            _mockBaseView.Setup(view => view.GetUserInput()).Returns("1");

            // Act
            bool result = _accountController.DeleteAccount();

            // Assert
            Assert.IsTrue(result);
            _mockAccountRepository.Verify(r => r.DeleteAccount(account.AccountId), Times.Once);
        }

        [TestMethod]
        public void DeleteAccount_WithNonExistingAccount_ThrowsKeyNotFoundException()
        {
            // Arrange
            AccountEntity account = AccountFactory.CreateAccount();
            _mockAccountRepository.Setup(r => r.DeleteAccount(account.AccountId)).Returns(false);
            _mockAccountRepository.Setup(r => r.GetAllAccounts()).Returns(new List<AccountEntity> { account });
            _mockBaseView.Setup(view => view.GetUserInput()).Returns("1");

            // Act
            bool success = _accountController.DeleteAccount();

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void GetAllAccounts_WhenCalled_ReturnsAllAccounts()
        {
            // Arrange
            List<AccountEntity> accounts = AccountFactory.CreateAccounts(numberOfAccounts: 3);
            _mockAccountRepository.Setup(r => r.GetAllAccounts()).Returns(accounts);

            // Act
            List<AccountEntity> result = _accountController.GetAllAccounts();

            // Assert
            Assert.AreEqual(accounts.Count, result.Count);
            CollectionAssert.AreEqual(accounts, result);
        }

        [TestMethod]
        public void GetAllAccounts_WithNoAccounts_ReturnsEmptyList()
        {
            // Arrange
            List<AccountEntity> accounts = new List<AccountEntity>();
            _mockAccountRepository.Setup(r => r.GetAllAccounts()).Returns(accounts);

            // Act
            List<AccountEntity> result = _accountController.GetAllAccounts();

            // Assert
            Assert.AreEqual(0, result.Count);
            CollectionAssert.AreEqual(accounts, result);
        }
    }
}