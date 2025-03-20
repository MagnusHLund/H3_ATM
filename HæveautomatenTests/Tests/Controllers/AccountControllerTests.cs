using Moq;
using Hæveautomaten.Entities;
using HæveautomatenTests.Factories;
using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Interfaces.Repositories;

namespace HæveautomatenTests.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        private Mock<IAccountController> _mockAccountController;
        private Mock<IAccountRepository> _mockAccountRepository;

        [TestInitialize]
        public void Setup()
        {
            _mockAccountController = new Mock<IAccountController>();
            _mockAccountRepository = new Mock<IAccountRepository>();
        }

        [TestMethod]
        public void CreateAccount_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            AccountEntity account = AccountFactory.CreateAccount();
            _mockAccountRepository.Setup(x => x.CreateAccount(account)).Returns(true);

            // Act
            bool successfullyCreated = _mockAccountController.Object.CreateAccount(account);

            // Assert
            Assert.IsTrue(successfullyCreated);
        }

        [TestMethod]
        public void CreateAccount_WithoutBank_ThrowsArgumentNullException()
        {
            // Arrange
            AccountEntity account = AccountFactory.CreateAccount(
                associatedBank: null
            );

            _mockAccountRepository.Setup(x => x.CreateAccount(account)).Returns(true);

            // Act & assert
            Assert.ThrowsException<ArgumentNullException>(() => _mockAccountController.Object.CreateAccount(account));
        }

        [TestMethod]
        public void CreateAccount_WithoutOwner_ThrowsArgumentNullException()
        {
            // Arrange
            AccountEntity account = AccountFactory.CreateAccount(
                accountOwner: null
            );

            // Act & assert
            Assert.ThrowsException<ArgumentNullException>(() => _mockAccountController.Object.CreateAccount(account));
        }

        [TestMethod]
        public void CreateAccount_WithNotFoundBank_ThrowsInvalidOperationException()
        {
            // Arrange
            AccountEntity account = AccountFactory.CreateAccount(
                associatedBank: null
            );

            // Act & assert
            Assert.ThrowsException<KeyNotFoundException>(() => _mockAccountController.Object.CreateAccount(account));
        }

        [TestMethod]
        public void CreateAccount_WithOwnerNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            AccountEntity account = AccountFactory.CreateAccount(
                accountOwner: null
            );

            // Act & assert
            Assert.ThrowsException<KeyNotFoundException>(() => _mockAccountController.Object.CreateAccount(account));
        }

        [TestMethod]
        public void DeleteAccount_WithExistingAccount_DeletesSuccessfully()
        {
            // Arrange
            AccountEntity account = AccountFactory.CreateAccount();
            _mockAccountRepository.Setup(x => x.DeleteAccount(account.AccountId)).Returns(true);

            // Act
            bool successfullyDeleted = _mockAccountController.Object.DeleteAccount(account);

            // Assert
            Assert.IsTrue(successfullyDeleted);
        }

        [TestMethod]
        public void DeleteAccount_WithNonExistingAccount_ThrowsKeyNotFoundException()
        {
            // Arrange
            AccountEntity account = AccountFactory.CreateAccount();
            _mockAccountRepository.Setup(x => x.DeleteAccount(account.AccountId)).Returns(false);

            // Act & assert
            Assert.ThrowsException<KeyNotFoundException>(() => _mockAccountController.Object.DeleteAccount(account));
        }

        [TestMethod]
        public void GetAllAccounts_WhenCalled_ReturnsAllAccounts()
        {
            // Arrange
            List<AccountEntity> accounts = AccountFactory.CreateAccounts();
            _mockAccountController.Setup(x => x.GetAllAccounts()).Returns(accounts);

            // Act
            List<AccountEntity> allAccounts = _mockAccountController.Object.GetAllAccounts();

            // Assert
            Assert.AreEqual(accounts, allAccounts);
        }

        [TestMethod]
        public void GetAllAccounts_WithNoAccounts_ReturnsEmptyList()
        {
            // Arrange
            List<AccountEntity> accounts = new List<AccountEntity>();
            _mockAccountController.Setup(x => x.GetAllAccounts()).Returns(accounts);

            // Act
            List<AccountEntity> allAccounts = _mockAccountController.Object.GetAllAccounts();

            // Assert
            Assert.AreEqual(accounts, allAccounts);
        }
    }
}