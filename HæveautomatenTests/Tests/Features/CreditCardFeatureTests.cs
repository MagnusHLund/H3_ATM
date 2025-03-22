using Hæveautomaten.Entities;
using HæveautomatenTests.Views;
using Hæveautomaten.Controllers;
using HæveautomatenTests.Factories;
using HæveautomatenTests.Repositories;
using System.ComponentModel.DataAnnotations;

namespace HæveautomatenTests.Tests.Features
{
    [TestClass]
    public class CreditCardFeatureTests
    {
        private InMemoryCreditCardRepository _creditCardRepository;
        private InMemoryAccountRepository _accountRepository;
        private InMemoryPersonRepository _personRepository;
        private InMemoryBankRepository _bankRepository;
        private FakeBaseView _baseView;
        private MainController _mainController;
        private AdminController _adminController;

        [TestInitialize]
        public void Setup()
        {
            _creditCardRepository = new InMemoryCreditCardRepository();
            _accountRepository = new InMemoryAccountRepository();
            _personRepository = new InMemoryPersonRepository();
            _bankRepository = new InMemoryBankRepository();
            _baseView = new FakeBaseView();

            BankController bankController = new BankController(_bankRepository, _baseView);
            PersonController personController = new PersonController(_personRepository, _baseView);
            AccountController accountController = new AccountController(_accountRepository, bankController, personController, _baseView);
            CreditCardController creditCardController = new CreditCardController(_creditCardRepository, personController, accountController, _baseView);

            _adminController = new AdminController(
                accountController,
                personController,
                creditCardController,
                bankController,
                null,
                _baseView
            );

            _mainController = new MainController(_adminController, null, _baseView);

            BankEntity bank = BankFactory.CreateBank("Test Bank");
            _bankRepository.CreateBank(bank);

            PersonEntity person = PersonFactory.CreatePerson();
            _personRepository.CreatePerson(person);

            AccountEntity account = AccountFactory.CreateAccount(person, bank);
            _accountRepository.CreateAccount(account);
        }

        [TestMethod]
        public void CreateCreditCardThroughMainMenu_FeatureTest()
        {
            // Arrange
            string cardNumber = "1234123412341234";
            string expirationDate = "12/25";
            ushort cvv = 123;
            ushort pinCode = 1234;
            string isBlocked = "false";

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "3", // Create Credit Card
                "1", // Select person
                "1", // Select account
                cardNumber, // Card number
                expirationDate, // Expiration date
                cvv.ToString(), // CVV
                pinCode.ToString(), // Pin code
                isBlocked // Is blocked
            });

            // Act
            _mainController.HandleMainMenuDisplay();

            // Assert
            List<CreditCardEntity> creditCards = _creditCardRepository.GetAllCreditCards();
            Assert.AreEqual(1, creditCards.Count);
            Assert.AreEqual(cardNumber, creditCards[0].CardNumber);
            Assert.AreEqual(cvv, creditCards[0].Cvv);
            Assert.AreEqual(pinCode, creditCards[0].PinCode);
            Assert.IsFalse(creditCards[0].IsBlocked);
        }

        [TestMethod]
        public void DeleteCreditCardThroughMainMenu_FeatureTest()
        {
            // Arrange
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();
            _creditCardRepository.CreateCreditCard(creditCard);

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "4", // Delete Credit Card
                "1" // Select credit card
            });

            // Act
            _mainController.HandleMainMenuDisplay();

            // Assert
            List<CreditCardEntity> creditCards = _creditCardRepository.GetAllCreditCards();
            Assert.AreEqual(0, creditCards.Count);
        }

        [TestMethod]
        public void CreateCreditCardWithInvalidCardNumber_FeatureTest()
        {
            // Arrange
            string invalidCardNumber = "invalid";

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "3", // Create Credit Card
                "1", // Select person
                "1", // Select account
                invalidCardNumber, // Invalid card number
                "12/25", // Expiration date
                "123", // CVV
                "1234", // Pin code
                "false" // Is blocked
            });

            // Act & Assert
            Assert.ThrowsException<ValidationException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void CreateCreditCardWithExpiredExpiryDate_FeatureTest()
        {
            // Arrange
            string expiredDate = "01/20"; // Expired date

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "3", // Create Credit Card
                "1", // Select person
                "1", // Select account
                "1234123412341234", // Card number
                expiredDate, // Expiration date
                "123", // CVV
                "1234", // Pin code
                "false" // Is blocked
            });

            // Act & Assert
            Assert.ThrowsException<ValidationException>(() => _mainController.HandleMainMenuDisplay());
        }

        [TestMethod]
        public void CreateCreditCardWithBlockedStatus_FeatureTest()
        {
            // Arrange
            string cardNumber = "1234123412341234";
            string expirationDate = "12/25";
            ushort cvv = 123;
            ushort pinCode = 1234;
            string isBlocked = "true"; // Blocked card

            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "3", // Create Credit Card
                "1", // Select person
                "1", // Select account
                cardNumber, // Card number
                expirationDate, // Expiration date
                cvv.ToString(), // CVV
                pinCode.ToString(), // Pin code
                isBlocked // Is blocked
            });

            // Act
            _mainController.HandleMainMenuDisplay();

            // Assert
            List<CreditCardEntity> creditCards = _creditCardRepository.GetAllCreditCards();
            Assert.AreEqual(1, creditCards.Count);
            Assert.IsTrue(creditCards[0].IsBlocked);
        }

        [TestMethod]
        public void DeleteCreditCardWithNonExistingCard_FeatureTest()
        {
            // Arrange
            _baseView.SetUserInputs(new List<string>
            {
                "1", // Navigate to Admin Menu
                "4", // Delete Credit Card
                "1" // Non-existing credit card
            });

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _mainController.HandleMainMenuDisplay());
        }
    }
}