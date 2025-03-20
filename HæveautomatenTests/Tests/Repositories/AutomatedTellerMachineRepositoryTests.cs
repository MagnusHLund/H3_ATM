using Hæveautomaten.Data;
using Hæveautomaten.Entities;
using Hæveautomaten.Repositories;
using HæveautomatenTests.Factories;
using HæveautomatenTests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace HæveautomatenTests.Tests.Repositories
{
    [TestClass]
    public class AutomatedTellerMachineRepositoryTests
    {
        private Mock<HæveautomatenDbContext> _dbContextMock;
        private AutomatedTellerMachineRepository _atmRepository;

        [TestInitialize]
        public void Setup()
        {
            _dbContextMock = new Mock<HæveautomatenDbContext>();
            _atmRepository = new AutomatedTellerMachineRepository(_dbContextMock.Object);
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
        }

        [TestMethod]
        public void DeleteAutomatedTellerMachine_WithExistingATM_DeletesSuccessfully()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            _dbContextMock.Setup(db => db.AutomatedTellerMachines.Find(atm.AutomatedTellerMachineId)).Returns(atm);

            // Act
            bool result = _atmRepository.DeleteAutomatedTellerMachine(atm.AutomatedTellerMachineId);

            // Assert
            Assert.IsTrue(result);
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
            _dbContextMock.Setup(db => db.AutomatedTellerMachines).Returns(MockUtils.CreateMockDbSet(atms).Object);

            // Act
            List<AutomatedTellerMachineEntity> result = _atmRepository.GetAllAutomatedTellerMachines();

            // Assert
            Assert.AreEqual(atms.Count, result.Count);
        }
    }
}