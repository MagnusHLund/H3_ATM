using Moq;
using Hæveautomaten.Data;
using Hæveautomaten.Entities;
using Hæveautomaten.Repositories;
using HæveautomatenTests.Factories;
using Microsoft.EntityFrameworkCore;

namespace HæveautomatenTests.Tests.Repositories
{
    [TestClass]
    public class AutomatedTellerMachineRepositoryTests
    {
        private HæveautomatenDbContext _dbContext;
        private AutomatedTellerMachineRepository _atmRepository;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HæveautomatenDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new HæveautomatenDbContext(options);
            _atmRepository = new AutomatedTellerMachineRepository(_dbContext);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [TestMethod]
        public void CreateAutomatedTellerMachine_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();

            // Act
            bool result = _atmRepository.CreateAutomatedTellerMachine(atm);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _dbContext.AutomatedTellerMachines.CountAsync().Result);
        }

        [TestMethod]
        public void CreateAutomatedTellerMachine_WithNullATM_ThrowsNullReferenceException()
        {
            // Act & Assert
            Assert.ThrowsException<NullReferenceException>(() => _atmRepository.CreateAutomatedTellerMachine(null));
        }

        [TestMethod]
        public void DeleteAutomatedTellerMachine_WithExistingATM_DeletesSuccessfully()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            _dbContext.AutomatedTellerMachines.Add(atm);
            _dbContext.SaveChanges();

            // Act
            bool result = _atmRepository.DeleteAutomatedTellerMachine(atm.AutomatedTellerMachineId);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _dbContext.AutomatedTellerMachines.CountAsync().Result);
        }

        [TestMethod]
        public void DeleteAutomatedTellerMachine_WithNonExistingATM_ThrowsKeyNotFoundException()
        {
            // Act & assert
            Assert.ThrowsException<KeyNotFoundException>(() => _atmRepository.DeleteAutomatedTellerMachine(0));
        }

        [TestMethod]
        public void DeleteAutomatedTellerMachine_WithNullATM_ThrowsKeyNotFoundException()
        {
            // Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => _atmRepository.DeleteAutomatedTellerMachine(0));
        }

        [TestMethod]
        public void GetAllAutomatedTellerMachines_ReturnsAllATMs()
        {
            // Arrange
            List<AutomatedTellerMachineEntity> atms = new List<AutomatedTellerMachineEntity>
            {
                AutomatedTellerMachineFactory.CreateAutomatedTellerMachine(),
                AutomatedTellerMachineFactory.CreateAutomatedTellerMachine()
            };
            _dbContext.AutomatedTellerMachines.AddRange(atms);
            _dbContext.SaveChanges();

            // Act
            List<AutomatedTellerMachineEntity> result = _atmRepository.GetAllAutomatedTellerMachines();

            // Assert
            Assert.AreEqual(atms.Count, result.Count);
        }

        [TestMethod]
        public void GetAllAutomatedTellerMachines_WithNoATMs_ReturnsEmptyList()
        {
            // Act
            List<AutomatedTellerMachineEntity> result = _atmRepository.GetAllAutomatedTellerMachines();

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void CreateAutomatedTellerMachine_WithDuplicateATM_ThrowsArgumentException()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            _dbContext.AutomatedTellerMachines.Add(atm);
            _dbContext.SaveChanges();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _atmRepository.CreateAutomatedTellerMachine(atm));
        }
    }
}