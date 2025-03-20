using Moq;
using Hæveautomaten.Entities;
using Hæveautomaten.Controllers;
using HæveautomatenTests.Factories;
using Hæveautomaten.Interfaces.Repositories;

namespace HæveautomatenTests.Tests.Controllers
{
    [TestClass]
    public class BankControllerTests
    {
        private Mock<IBankRepository> _bankRepositoryMock;
        private BankController _bankController;

        [TestInitialize]
        public void Setup()
        {
            _bankRepositoryMock = new Mock<IBankRepository>();
            _bankController = new BankController(_bankRepositoryMock.Object);
        }

        [TestMethod]
        public void CreateBank_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            string bankName = "Test Bank";
            BankEntity bank = new BankEntity(bankName);
            _bankRepositoryMock.Setup(r => r.CreateBank(It.IsAny<BankEntity>())).Returns(true);

            // Act
            bool result = _bankController.CreateBank();

            // Assert
            Assert.IsTrue(result);
            _bankRepositoryMock.Verify(r => r.CreateBank(It.Is<BankEntity>(b => b.BankName == bankName)), Times.Once);
        }

        [TestMethod]
        public void CreateBank_WithEmptyStringBankName_ThrowsArgumentException()
        {
            // Arrange
            _bankRepositoryMock.Setup(r => r.CreateBank(It.IsAny<BankEntity>())).Throws(new ArgumentException("Bank name cannot be empty"));

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _bankController.CreateBank());
        }

        [TestMethod]
        public void CreateBank_WithNullBankName_ThrowsArgumentNullException()
        {
            // Arrange
            _bankRepositoryMock.Setup(r => r.CreateBank(It.IsAny<BankEntity>())).Throws(new ArgumentNullException("Bank name cannot be null"));

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _bankController.CreateBank());
        }

        [TestMethod]
        public void DeleteBank_WithExistingBank_DeletesSuccessfully()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank();
            _bankRepositoryMock.Setup(r => r.DeleteBank(bank.BankId)).Returns(true);

            // Act
            bool result = _bankController.DeleteBank();

            // Assert
            Assert.IsTrue(result);
            _bankRepositoryMock.Verify(r => r.DeleteBank(bank.BankId), Times.Once);
        }

        [TestMethod]
        public void DeleteBank_WithNonExistingBank_ThrowsKeyNotFoundException()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank();
            _bankRepositoryMock.Setup(r => r.DeleteBank(bank.BankId)).Throws(new KeyNotFoundException("Bank not found"));

            // Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => _bankController.DeleteBank());
        }

        [TestMethod]
        public void GetAllBanks_WhenCalled_ReturnsAllBanks()
        {
            // Arrange
            List<BankEntity> banks = new List<BankEntity>
            {
                BankFactory.CreateBank(),
                BankFactory.CreateBank()
            };
            _bankRepositoryMock.Setup(r => r.GetAllBanks()).Returns(banks);

            // Act
            List<BankEntity> result = _bankController.GetAllBanks();

            // Assert
            Assert.AreEqual(banks.Count, result.Count);
            CollectionAssert.AreEqual(banks, result);
            _bankRepositoryMock.Verify(r => r.GetAllBanks(), Times.Once);
        }
    }
}