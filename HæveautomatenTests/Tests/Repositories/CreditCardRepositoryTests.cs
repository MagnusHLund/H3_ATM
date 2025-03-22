using Hæveautomaten.Data;
using Hæveautomaten.Entities;
using Hæveautomaten.Repositories;
using HæveautomatenTests.Factories;
using Microsoft.EntityFrameworkCore;

namespace HæveautomatenTests.Tests.Repositories
{
    [TestClass]
    public class CreditCardRepositoryTests
    {
        private HæveautomatenDbContext _dbContext;
        private CreditCardRepository _creditCardRepository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HæveautomatenDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new HæveautomatenDbContext(options);
            _creditCardRepository = new CreditCardRepository(_dbContext);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
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
            Assert.AreEqual(1, _dbContext.CreditCards.CountAsync().Result);
        }

        [TestMethod]
        public void CreateCreditCard_WithNullCreditCard_ThrowsNullReferenceException()
        {
            // Act & Assert
            Assert.ThrowsException<NullReferenceException>(() => _creditCardRepository.CreateCreditCard(null));
        }

        [TestMethod]
        public void DeleteCreditCard_WithExistingCard_DeletesSuccessfully()
        {
            // Arrange
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();
            _dbContext.CreditCards.Add(creditCard);
            _dbContext.SaveChanges();

            // Act
            bool result = _creditCardRepository.DeleteCreditCard(creditCard.CreditCardId);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _dbContext.CreditCards.CountAsync().Result);
        }

        [TestMethod]
        public void DeleteCreditCard_WithNonExistingCard_ThrowsKeyNotFoundException()
        {
            // Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => _creditCardRepository.DeleteCreditCard(999));
        }

        [TestMethod]
        public void GetAllCreditCards_WithValidParameters_ReturnsAllCreditCards()
        {
            // Arrange
            List<CreditCardEntity> creditCards = new List<CreditCardEntity>
            {
                CreditCardFactory.CreateCreditCard(),
                CreditCardFactory.CreateCreditCard()
            };
            _dbContext.CreditCards.AddRange(creditCards);
            _dbContext.SaveChanges();

            // Act
            List<CreditCardEntity> result = _creditCardRepository.GetAllCreditCards();

            // Assert
            Assert.AreEqual(creditCards.Count, result.Count);
        }

        [TestMethod]
        public void GetAllCreditCards_WithNoCreditCards_ReturnsEmptyList()
        {
            // Act
            List<CreditCardEntity> result = _creditCardRepository.GetAllCreditCards();

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void CreateCreditCard_WithDuplicateCard_ThrowsArgumentException()
        {
            // Arrange
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();
            _dbContext.CreditCards.Add(creditCard);
            _dbContext.SaveChanges();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _creditCardRepository.CreateCreditCard(creditCard));
        }
    }
}