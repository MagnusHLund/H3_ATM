using Hæveautomaten.Data;
using Hæveautomaten.Entities;
using Hæveautomaten.Repositories;
using HæveautomatenTests.Factories;
using HæveautomatenTests.Utils;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HæveautomatenTests.Tests.Repositories
{
    [TestClass]
    public class AccountRepositoryTests
    {
        private Mock<HæveautomatenDbContext> _dbContextMock;
        private AccountRepository _accountRepository;

        [TestInitialize]
        public void Setup()
        {
            _dbContextMock = new Mock<HæveautomatenDbContext>();
            _accountRepository = new AccountRepository(_dbContextMock.Object);
        }

        [TestMethod]
        public void CreateAccount_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            AccountEntity account = AccountFactory.CreateAccount();

            // Act
            bool result = _accountRepository.CreateAccount(account);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteAccount_WithExistingAccount_DeletesSuccessfully()
        {
            // Arrange
            AccountEntity account = AccountFactory.CreateAccount();
            _dbContextMock.Setup(db => db.Accounts.Find(account.AccountId)).Returns(account);

            // Act
            bool result = _accountRepository.DeleteAccount(account.AccountId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllAccounts_ReturnsAllAccounts()
        {
            // Arrange
            List<AccountEntity> accounts = AccountFactory.CreateAccounts();
            _dbContextMock.Setup(db => db.Accounts).Returns(MockUtils.CreateMockDbSet(accounts).Object);

            // Act
            List<AccountEntity> result = _accountRepository.GetAllAccounts();

            // Assert
            Assert.AreEqual(accounts.Count, result.Count);
        }
    }
}