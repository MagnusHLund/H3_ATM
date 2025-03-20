using Moq;
using Hæveautomaten.Entities;
using HæveautomatenTests.Factories;
using Hæveautomaten.Interfaces.Controllers;

namespace HæveautomatenTests.Tests.Controllers
{
    [TestClass]
    public class BankControllerTests
    {
        private Mock<IBankController> _bankControllerMock;

        [TestInitialize]
        public void Setup()
        {
            _bankControllerMock = new Mock<IBankController>();
        }

        [TestMethod]
        public void CreateBank_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank();
            _bankControllerMock.Setup(b => b.CreateBank(bank)).Returns(true);

            // Act
            bool result = _bankControllerMock.Object.CreateBank(bank);

            // Assert
            Assert.IsTrue(result);
            _bankControllerMock.Verify(b => b.CreateBank(bank), Times.Once);
        }

        [TestMethod]
        public void CreateBank_WithEmptyStringBankName_ThrowsArgumentException()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank(bankName: "");
            _bankControllerMock.Setup(b => b.CreateBank(bank)).Throws(new ArgumentException("Bank name cannot be empty"));

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _bankControllerMock.Object.CreateBank(bank));
        }

        [TestMethod]
        public void CreateBank_WithNullBankName_ThrowsArgumentNullException()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank(bankName: null);
            _bankControllerMock.Setup(b => b.CreateBank(bank)).Throws(new ArgumentNullException("Bank name cannot be null"));

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _bankControllerMock.Object.CreateBank(bank));
        }

        [TestMethod]
        public void DeleteBank_WithExistingBank_DeletesSuccessfully()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank();
            _bankControllerMock.Setup(b => b.DeleteBank(bank)).Returns(true);

            // Act
            bool result = _bankControllerMock.Object.DeleteBank(bank);

            // Assert
            Assert.IsTrue(result);
            _bankControllerMock.Verify(b => b.DeleteBank(bank), Times.Once);
        }

        [TestMethod]
        public void DeleteBank_WithNonExistingBank_ThrowsException()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank();
            _bankControllerMock.Setup(b => b.DeleteBank(bank)).Throws(new Exception("Bank not found"));

            // Act & Assert
            Assert.ThrowsException<Exception>(() => _bankControllerMock.Object.DeleteBank(bank));
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
            _bankControllerMock.Setup(b => b.GetAllBanks()).Returns(banks);

            // Act
            List<BankEntity> result = _bankControllerMock.Object.GetAllBanks();

            // Assert
            Assert.AreEqual(banks.Count, result.Count);
            _bankControllerMock.Verify(b => b.GetAllBanks(), Times.Once);
        }
    }
}