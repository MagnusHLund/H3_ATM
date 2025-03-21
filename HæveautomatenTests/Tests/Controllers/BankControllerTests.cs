using Moq;
using Hæveautomaten.Entities;
using Hæveautomaten.Controllers;
using HæveautomatenTests.Factories;
using Hæveautomaten.Interfaces.Repositories;
using Hæveautomaten.Interfaces.Views;

namespace HæveautomatenTests.Tests.Controllers
{
    [TestClass]
    public class BankControllerTests
    {
        private Mock<IBankRepository> _bankRepositoryMock;
        private Mock<IBaseView> _baseViewMock;
        private BankController _bankController;

        [TestInitialize]
        public void Setup()
        {
            _bankRepositoryMock = new Mock<IBankRepository>();
            _baseViewMock = new Mock<IBaseView>();

            _bankController = new BankController(
                _bankRepositoryMock.Object,
                _baseViewMock.Object
            );
        }

        [TestMethod]
        public void CreateBank_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            string bankName = "Test Bank";
            _baseViewMock.Setup(view => view.GetUserInputWithTitle("Enter the bank name: ")).Returns(bankName);
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
            _baseViewMock.Setup(view => view.GetUserInputWithTitle("Enter the bank name: ")).Returns(string.Empty);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _bankController.CreateBank());
        }

        [TestMethod]
        public void CreateBank_WithNullBankName_ThrowsArgumentNullException()
        {
            // Arrange
            _baseViewMock.Setup(view => view.GetUserInputWithTitle("Enter the bank name: ")).Returns((string)null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _bankController.CreateBank());
        }

        [TestMethod]
        public void DeleteBank_WithExistingBank_DeletesSuccessfully()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank();
            _bankRepositoryMock.Setup(r => r.DeleteBank(bank.BankId)).Returns(true);
            _bankRepositoryMock.Setup(r => r.GetAllBanks()).Returns(new List<BankEntity> { bank });
            _baseViewMock.Setup(view => view.CustomMenu(It.IsAny<string[]>(), It.IsAny<string>()));
            _baseViewMock.Setup(view => view.GetUserInput()).Returns("1");

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
            _bankRepositoryMock.Setup(r => r.GetAllBanks()).Returns(new List<BankEntity> { bank });
            _baseViewMock.Setup(view => view.CustomMenu(It.IsAny<string[]>(), It.IsAny<string>()));
            _baseViewMock.Setup(view => view.GetUserInput()).Returns("1");

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

        [TestMethod]
        public void SelectBank_WithValidInput_ReturnsCorrectBank()
        {
            // Arrange
            List<BankEntity> banks = new List<BankEntity>
            {
                BankFactory.CreateBank("Bank 1"),
                BankFactory.CreateBank("Bank 2")
            };
            _bankRepositoryMock.Setup(r => r.GetAllBanks()).Returns(banks);
            _baseViewMock.Setup(view => view.CustomMenu(It.IsAny<string[]>(), It.IsAny<string>()));
            _baseViewMock.Setup(view => view.GetUserInput()).Returns("2");

            // Act
            BankEntity selectedBank = _bankController.SelectBank();

            // Assert
            Assert.IsNotNull(selectedBank);
            Assert.AreEqual(banks[1], selectedBank);
        }

        [TestMethod]
        public void SelectBank_WithInvalidInput_ThrowsException()
        {
            // Arrange
            List<BankEntity> banks = new List<BankEntity>
            {
                BankFactory.CreateBank("Bank 1"),
                BankFactory.CreateBank("Bank 2")
            };
            _bankRepositoryMock.Setup(r => r.GetAllBanks()).Returns(banks);
            _baseViewMock.Setup(view => view.CustomMenu(It.IsAny<string[]>(), It.IsAny<string>()));
            _baseViewMock.Setup(view => view.GetUserInput()).Returns("invalid");

            // Act & Assert
            Assert.ThrowsException<FormatException>(() => _bankController.SelectBank());
        }
    }
}