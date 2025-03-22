using Hæveautomaten.Data;
using Hæveautomaten.Entities;
using Hæveautomaten.Repositories;
using HæveautomatenTests.Factories;
using Microsoft.EntityFrameworkCore;

namespace HæveautomatenTests.Tests.Repositories
{
    [TestClass]
    public class AccountRepositoryTests
    {
        private HæveautomatenDbContext _dbContext;
        private AccountRepository _accountRepository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HæveautomatenDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new HæveautomatenDbContext(options);
            _accountRepository = new AccountRepository(_dbContext);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
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
            Assert.AreEqual(1, _dbContext.Accounts.CountAsync().Result);
        }

        [TestMethod]
        public void CreateAccount_WithNullAccount_ThrowsNullReferenceException()
        {
            // Act & Assert
            Assert.ThrowsException<NullReferenceException>(() => _accountRepository.CreateAccount(null));
        }

        [TestMethod]
        public void DeleteAccount_WithExistingAccount_DeletesSuccessfully()
        {
            // Arrange
            AccountEntity account = AccountFactory.CreateAccount();
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();

            // Act
            bool result = _accountRepository.DeleteAccount(account.AccountId);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _dbContext.Accounts.CountAsync().Result);
        }

        [TestMethod]
        public void DeleteAccount_WithNonExistingAccount_ThrowsKeyNotFoundException()
        {
            // Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => _accountRepository.DeleteAccount(999));
        }

        [TestMethod]
        public void GetAllAccounts_ReturnsAllAccounts()
        {
            // Arrange
            List<AccountEntity> accounts = AccountFactory.CreateAccounts(numberOfAccounts: 3);
            _dbContext.Accounts.AddRange(accounts);
            _dbContext.SaveChanges();

            // Act
            List<AccountEntity> result = _accountRepository.GetAllAccounts();

            // Assert
            Assert.AreEqual(accounts.Count, result.Count);
        }

        [TestMethod]
        public void GetAllAccounts_WithNoAccounts_ReturnsEmptyList()
        {
            // Act
            List<AccountEntity> result = _accountRepository.GetAllAccounts();

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetAccountsByPerson_WithExistingPerson_ReturnsAccounts()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson();
            List<AccountEntity> accounts = AccountFactory.CreateAccounts(accountOwner: person);
            _dbContext.Accounts.AddRange(accounts);
            _dbContext.SaveChanges();

            // Act
            List<AccountEntity> result = _accountRepository.GetAccountsByPerson(person.PersonId);

            // Assert
            Assert.AreEqual(accounts.Count, result.Count);
        }

        [TestMethod]
        public void GetAccountsByPerson_WithNonExistingPerson_ReturnsEmptyList()
        {
            // Act
            List<AccountEntity> result = _accountRepository.GetAccountsByPerson(999);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void UpdateAccount_WithValidData_UpdatesSuccessfully()
        {
            // Arrange
            AccountEntity account = AccountFactory.CreateAccount();
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();

            account.BalanceInMinorUnits += 5000;

            // Act
            AccountEntity updatedAccount = _accountRepository.UpdateAccount(account);

            // Assert
            Assert.AreEqual(account.BalanceInMinorUnits, updatedAccount.BalanceInMinorUnits);
        }

        [TestMethod]
        public void UpdateAccount_WithNonExistingAccount_ThrowsDbUpdateConcurrencyException()
        {
            // Arrange
            AccountEntity account = AccountFactory.CreateAccount();
            account.AccountId = 999; // Simulate non-existing account

            // Act & Assert
            Assert.ThrowsException<DbUpdateConcurrencyException>(() => _accountRepository.UpdateAccount(account));
        }
    }
}