using Moq;
using Hæveautomaten.Entities;
using HæveautomatenTests.Factories;
using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Interfaces.Views;
using Hæveautomaten.Interfaces.Repositories;
using Hæveautomaten.Controllers;
using System.Globalization;

namespace HæveautomatenTests.Tests.Controllers
{
    [TestClass]
    public class CreditCardControllerTests
    {
        private Mock<ICreditCardRepository> _creditCardRepositoryMock;
        private Mock<IPersonController> _personControllerMock;
        private Mock<IAccountController> _accountControllerMock;
        private Mock<IBaseView> _baseViewMock;
        private CreditCardController _creditCardController;

        [TestInitialize]
        public void Setup()
        {
            _creditCardRepositoryMock = new Mock<ICreditCardRepository>();
            _personControllerMock = new Mock<IPersonController>();
            _accountControllerMock = new Mock<IAccountController>();
            _baseViewMock = new Mock<IBaseView>();

            _creditCardController = new CreditCardController(
                _creditCardRepositoryMock.Object,
                _personControllerMock.Object,
                _accountControllerMock.Object,
                _baseViewMock.Object
            );
        }

        [TestMethod]
        public void CreateCreditCard_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            PersonEntity person = PersonFactory.CreatePerson();
            AccountEntity account = AccountFactory.CreateAccount(person);
            ulong cardNumber = 1234123412341234;
            DateTime expirationDate = DateTime.Now.AddYears(3);
            ushort cvv = 123;
            ushort pinCode = 1234;
            bool isBlocked = false;

            string expectedExpirationDateString = expirationDate.ToString("MM/yy").Replace("-", "/");

            _personControllerMock.Setup(p => p.SelectPerson()).Returns(person);
            _accountControllerMock.Setup(a => a.GetAccountsByPerson(person)).Returns(new List<AccountEntity> { account });
            _accountControllerMock.Setup(a => a.SelectAccount(It.IsAny<List<AccountEntity>>())).Returns(account);
            _baseViewMock.Setup(view => view.GetUserInputWithTitle("Enter the card number: ")).Returns(cardNumber.ToString());
            _baseViewMock.Setup(view => view.GetUserInputWithTitle("Enter the expiration date (MM/YY): ")).Returns(expectedExpirationDateString);
            _baseViewMock.Setup(view => view.GetUserInputWithTitle("Enter the CVV: ")).Returns(cvv.ToString());
            _baseViewMock.Setup(view => view.GetUserInputWithTitle("Enter the pin code: ")).Returns(pinCode.ToString());
            _baseViewMock.Setup(view => view.GetUserInputWithTitle("Is the card blocked? (true/false): ")).Returns(isBlocked.ToString());
            _creditCardRepositoryMock.Setup(r => r.CreateCreditCard(It.IsAny<CreditCardEntity>())).Returns(true);

            // Act
            bool result = _creditCardController.CreateCreditCard();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteCreditCard_WithExistingCard_DeletesSuccessfully()
        {
            // Arrange
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();
            _creditCardRepositoryMock.Setup(r => r.DeleteCreditCard(creditCard.CreditCardId)).Returns(true);
            _creditCardRepositoryMock.Setup(r => r.GetAllCreditCards()).Returns(new List<CreditCardEntity> { creditCard });
            _baseViewMock.Setup(view => view.CustomMenu(It.IsAny<string[]>(), It.IsAny<string>()));
            _baseViewMock.Setup(view => view.GetUserInput()).Returns("1");

            // Act
            bool result = _creditCardController.DeleteCreditCard();

            // Assert
            Assert.IsTrue(result);
            _creditCardRepositoryMock.Verify(r => r.DeleteCreditCard(creditCard.CreditCardId), Times.Once);
        }

        [TestMethod]
        public void GetAllCreditCards_WhenCalled_ReturnsAllCreditCards()
        {
            // Arrange
            List<CreditCardEntity> creditCards = new List<CreditCardEntity>
            {
                CreditCardFactory.CreateCreditCard(),
                CreditCardFactory.CreateCreditCard()
            };
            _creditCardRepositoryMock.Setup(r => r.GetAllCreditCards()).Returns(creditCards);

            // Act
            List<CreditCardEntity> result = _creditCardController.GetAllCreditCards();

            // Assert
            Assert.AreEqual(creditCards.Count, result.Count);
            CollectionAssert.AreEqual(creditCards, result);
        }

        [TestMethod]
        public void SelectCreditCard_WithValidInput_ReturnsCorrectCreditCard()
        {
            // Arrange
            List<CreditCardEntity> creditCards = new List<CreditCardEntity>
            {
                CreditCardFactory.CreateCreditCard(),
                CreditCardFactory.CreateCreditCard()
            };
            _creditCardRepositoryMock.Setup(r => r.GetAllCreditCards()).Returns(creditCards);
            _baseViewMock.Setup(view => view.CustomMenu(It.IsAny<string[]>(), It.IsAny<string>()));
            _baseViewMock.Setup(view => view.GetUserInput()).Returns("2");

            // Act
            CreditCardEntity selectedCreditCard = _creditCardController.SelectCreditCard();

            // Assert
            Assert.IsNotNull(selectedCreditCard);
            Assert.AreEqual(creditCards[1], selectedCreditCard);
        }

        [TestMethod]
        public void SelectCreditCard_WithInvalidInput_ThrowsException()
        {
            // Arrange
            List<CreditCardEntity> creditCards = new List<CreditCardEntity>
            {
                CreditCardFactory.CreateCreditCard(),
                CreditCardFactory.CreateCreditCard()
            };
            _creditCardRepositoryMock.Setup(r => r.GetAllCreditCards()).Returns(creditCards);
            _baseViewMock.Setup(view => view.CustomMenu(It.IsAny<string[]>(), It.IsAny<string>()));
            _baseViewMock.Setup(view => view.GetUserInput()).Returns("invalid");

            // Act & Assert
            Assert.ThrowsException<FormatException>(() => _creditCardController.SelectCreditCard());
        }

        [TestMethod]
        public void ConvertStringToDateTime_WithValidInput_ReturnsCorrectDateTime()
        {
            // Arrange
            string expirationDate = "12/23";

            // Act
            DateTime result = _creditCardController.ConvertStringToDateTime(expirationDate);

            // Assert
            Assert.AreEqual(new DateTime(2023, 12, 1), result);
        }

        [TestMethod]
        public void ConvertStringToDateTime_WithInvalidFormat_ThrowsFormatException()
        {
            // Arrange
            string expirationDate = "12-23"; // Invalid format

            // Act & Assert
            Assert.ThrowsException<FormatException>(() => _creditCardController.ConvertStringToDateTime(expirationDate));
        }

        [TestMethod]
        public void ConvertStringToDateTime_WithMissingParts_ThrowsIndexOutOfRangeException()
        {
            // Arrange
            string expirationDate = "12"; // Missing year

            // Act & Assert
            Assert.ThrowsException<IndexOutOfRangeException>(() => _creditCardController.ConvertStringToDateTime(expirationDate));
        }

        [TestMethod]
        public void ConvertStringToDateTime_WithInvalidMonth_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            string expirationDate = "13/23"; // Invalid month

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _creditCardController.ConvertStringToDateTime(expirationDate));
        }

        [TestMethod]
        public void ConvertStringToDateTime_WithNonNumericInput_ThrowsFormatException()
        {
            // Arrange
            string expirationDate = "MM/YY"; // Non-numeric input

            // Act & Assert
            Assert.ThrowsException<FormatException>(() => _creditCardController.ConvertStringToDateTime(expirationDate));
        }

        [TestMethod]
        public void ConvertStringToDateTime_WithValidSingleDigitMonth_ReturnsCorrectDateTime()
        {
            // Arrange
            string expirationDate = "1/25";

            // Act
            DateTime result = _creditCardController.ConvertStringToDateTime(expirationDate);

            // Assert
            Assert.AreEqual(new DateTime(2025, 1, 1), result);
        }
    }
}