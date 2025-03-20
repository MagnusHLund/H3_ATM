using Hæveautomaten.Data;
using Hæveautomaten.Entities;
using Hæveautomaten.Repositories;
using HæveautomatenTests.Factories;
using HæveautomatenTests.Utils;
using Moq;

namespace HæveautomatenTests.Tests.Repositories
{
    [TestClass]
    public class CreditCardRepositoryTests
    {
        private Mock<HæveautomatenDbContext> _dbContextMock;
        private CreditCardRepository _creditCardRepository;

        [TestInitialize]
        public void Setup()
        {
            _dbContextMock = new Mock<HæveautomatenDbContext>();
            _creditCardRepository = new CreditCardRepository(_dbContextMock.Object);
        }

        [TestMethod]
        public void CreateCreditCard_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();

            // Act
            bool result = _creditCardRepository.CreateCreditCard(creditCard);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteCreditCard_WithExistingCard_DeletesSuccessfully()
        {
            // Arrange
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();
            _dbContextMock.Setup(db => db.CreditCards.Find(creditCard.CreditCardId)).Returns(creditCard);

            // Act
            bool result = _creditCardRepository.DeleteCreditCard(creditCard.CreditCardId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllCreditCards_ReturnsAllCreditCards()
        {
            // Arrange
            List<CreditCardEntity> creditCards = new List<CreditCardEntity>
            {
                CreditCardFactory.CreateCreditCard(),
                CreditCardFactory.CreateCreditCard()
            };
            _dbContextMock.Setup(db => db.CreditCards).Returns(MockUtils.CreateMockDbSet(creditCards).Object);

            // Act
            List<CreditCardEntity> result = _creditCardRepository.GetAllCreditCards();

            // Assert
            Assert.AreEqual(creditCards.Count, result.Count);
        }
    }
}