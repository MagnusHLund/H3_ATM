using Hæveautomaten.Data;
using Hæveautomaten.Entities;
using Hæveautomaten.Repositories;
using HæveautomatenTests.Factories;
using HæveautomatenTests.Utils;
using Moq;

namespace HæveautomatenTests.Tests.Repositories
{
    [TestClass]
    public class BankRepositoryTests
    {
        private Mock<HæveautomatenDbContext> _dbContextMock;
        private BankRepository _bankRepository;

        [TestInitialize]
        public void Setup()
        {
            _dbContextMock = new Mock<HæveautomatenDbContext>();
            _bankRepository = new BankRepository(_dbContextMock.Object);
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
        }

        [TestMethod]
        public void DeleteBank_WithExistingBank_DeletesSuccessfully()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank();
            _dbContextMock.Setup(db => db.Banks.Find(bank.BankId)).Returns(bank);

            // Act
            bool result = _bankRepository.DeleteBank(bank.BankId);

            // Assert
            Assert.IsTrue(result);
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
            _dbContextMock.Setup(db => db.Banks).Returns(MockUtils.CreateMockDbSet(banks).Object);

            // Act
            List<BankEntity> result = _bankRepository.GetAllBanks();

            // Assert
            Assert.AreEqual(banks.Count, result.Count);
        }
    }
}