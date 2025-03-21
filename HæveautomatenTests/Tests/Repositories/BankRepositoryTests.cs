using Hæveautomaten.Data;
using Hæveautomaten.Entities;
using Hæveautomaten.Repositories;
using HæveautomatenTests.Factories;
using Microsoft.EntityFrameworkCore;

namespace HæveautomatenTests.Tests.Repositories
{
    [TestClass]
    public class BankRepositoryTests
    {
        private HæveautomatenDbContext _dbContext;
        private BankRepository _bankRepository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HæveautomatenDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new HæveautomatenDbContext(options);
            _bankRepository = new BankRepository(_dbContext);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [TestMethod]
        public void CreateBank_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank();

            // Act
            bool result = _bankRepository.CreateBank(bank);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _dbContext.Banks.CountAsync().Result);
        }

        [TestMethod]
        public void CreateBank_WithNullBank_ThrowsNullReferenceException()
        {
            // Act & Assert
            Assert.ThrowsException<NullReferenceException>(() => _bankRepository.CreateBank(null));
        }

        [TestMethod]
        public void DeleteBank_WithExistingBank_DeletesSuccessfully()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank();
            _dbContext.Banks.Add(bank);
            _dbContext.SaveChanges();

            // Act
            bool result = _bankRepository.DeleteBank(bank.BankId);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _dbContext.Banks.CountAsync().Result);
        }

        [TestMethod]
        public void DeleteBank_WithNonExistingBank_ThrowsKeyNotFoundException()
        {
            // Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => _bankRepository.DeleteBank(999));
        }

        [TestMethod]
        public void GetAllBanks_ReturnsAllBanks()
        {
            // Arrange
            List<BankEntity> banks = new List<BankEntity>
            {
                BankFactory.CreateBank(),
                BankFactory.CreateBank()
            };
            _dbContext.Banks.AddRange(banks);
            _dbContext.SaveChanges();

            // Act
            List<BankEntity> result = _bankRepository.GetAllBanks();

            // Assert
            Assert.AreEqual(banks.Count, result.Count);
        }

        [TestMethod]
        public void GetAllBanks_WithNoBanks_ReturnsEmptyList()
        {
            // Act
            List<BankEntity> result = _bankRepository.GetAllBanks();

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void CreateBank_WithDuplicateBank_ThrowsArgumentException()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank();
            _dbContext.Banks.Add(bank);
            _dbContext.SaveChanges();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _bankRepository.CreateBank(bank));
        }
    }
}