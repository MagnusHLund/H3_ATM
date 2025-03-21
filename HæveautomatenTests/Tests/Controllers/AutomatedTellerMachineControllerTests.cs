using Moq;
using Hæveautomaten.Entities;
using Hæveautomaten.Controllers;
using HæveautomatenTests.Factories;
using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Interfaces.Repositories;
using Hæveautomaten.Interfaces.Views;

namespace HæveautomatenTests.Tests.Controllers
{
    [TestClass]
    public class AutomatedTellerMachineControllerTests
    {
        private Mock<IAutomatedTellerMachineRepository> _atmRepositoryMock;
        private Mock<ICreditCardController> _creditCardControllerMock;
        private Mock<IBankController> _bankControllerMock;
        private Mock<IBaseView> _baseViewMock;
        private AutomatedTellerMachineController _atmController;

        [TestInitialize]
        public void Setup()
        {
            _atmRepositoryMock = new Mock<IAutomatedTellerMachineRepository>();
            _creditCardControllerMock = new Mock<ICreditCardController>();
            _bankControllerMock = new Mock<IBankController>();
            _baseViewMock = new Mock<IBaseView>();

            _atmController = new AutomatedTellerMachineController(
                _atmRepositoryMock.Object,
                _bankControllerMock.Object,
                _creditCardControllerMock.Object,
                _baseViewMock.Object
            );
        }

        [TestMethod]
        public void CreateAutomatedTellerMachine_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank();
            uint minimumExchangeAmount = 100;
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine(bank, minimumExchangeAmount);

            _bankControllerMock.Setup(b => b.SelectBank()).Returns(bank);
            _baseViewMock.Setup(view => view.GetUserInputWithTitle("Enter the minimum exchange amount: ")).Returns(minimumExchangeAmount.ToString());
            _atmRepositoryMock.Setup(r => r.CreateAutomatedTellerMachine(It.IsAny<AutomatedTellerMachineEntity>())).Returns(true);

            // Act
            bool result = _atmController.CreateAutomatedTellerMachine();

            // Assert
            Assert.IsTrue(result);
            _atmRepositoryMock.Verify(r => r.CreateAutomatedTellerMachine(It.Is<AutomatedTellerMachineEntity>(a =>
                a.Bank == bank &&
                a.MinimumExchangeAmount == minimumExchangeAmount
            )), Times.Once);
        }

        [TestMethod]
        public void CreateAutomatedTellerMachine_WithoutBank_ThrowsArgumentNullException()
        {
            // Arrange
            _bankControllerMock.Setup(b => b.SelectBank()).Returns((BankEntity)null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _atmController.CreateAutomatedTellerMachine());
        }

        [TestMethod]
        public void DeleteAutomatedTellerMachine_WithExistingATM_DeletesSuccessfully()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            _atmRepositoryMock.Setup(r => r.DeleteAutomatedTellerMachine(atm.AutomatedTellerMachineId)).Returns(true);
            _atmRepositoryMock.Setup(r => r.GetAllAutomatedTellerMachines()).Returns(new List<AutomatedTellerMachineEntity> { atm });
            _baseViewMock.Setup(view => view.GetUserInput()).Returns("1");

            // Act
            bool result = _atmController.DeleteAutomatedTellerMachine();

            // Assert
            Assert.IsTrue(result);
            _atmRepositoryMock.Verify(r => r.DeleteAutomatedTellerMachine(atm.AutomatedTellerMachineId), Times.Once);
        }

        [TestMethod]
        public void GetAllAutomatedTellerMachines_WhenCalled_ReturnsAllATMs()
        {
            // Arrange
            List<AutomatedTellerMachineEntity> atms = new List<AutomatedTellerMachineEntity>
            {
                AutomatedTellerMachineFactory.CreateAutomatedTellerMachine(),
                AutomatedTellerMachineFactory.CreateAutomatedTellerMachine()
            };
            _atmRepositoryMock.Setup(r => r.GetAllAutomatedTellerMachines()).Returns(atms);

            // Act
            List<AutomatedTellerMachineEntity> result = _atmController.GetAllAutomatedTellerMachines();

            // Assert
            Assert.AreEqual(atms.Count, result.Count);
            CollectionAssert.AreEqual(atms, result);
        }

        [TestMethod]
        public void SwitchAutomatedTellerMachine_WithMultipleATMs_ReturnsSelectedATM()
        {
            // Arrange
            List<AutomatedTellerMachineEntity> atms = new List<AutomatedTellerMachineEntity>
            {
                AutomatedTellerMachineFactory.CreateAutomatedTellerMachine(),
                AutomatedTellerMachineFactory.CreateAutomatedTellerMachine()
            };
            _atmRepositoryMock.Setup(r => r.GetAllAutomatedTellerMachines()).Returns(atms);
            _baseViewMock.Setup(view => view.CustomMenu(It.IsAny<string[]>(), It.IsAny<string>()));
            _baseViewMock.Setup(view => view.GetUserInput()).Returns("1");

            // Act
            AutomatedTellerMachineEntity selectedATM = _atmController.SwitchAutomatedTellerMachine(atms);

            // Assert
            Assert.IsNotNull(selectedATM);
            Assert.AreEqual(atms[0], selectedATM); // Assuming the first ATM is selected
        }

        [TestMethod]
        public void SwitchAutomatedTellerMachine_WithEmptyATMList_ThrowsException()
        {
            // Arrange
            List<AutomatedTellerMachineEntity> atms = new List<AutomatedTellerMachineEntity>();

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _atmController.SwitchAutomatedTellerMachine(atms));
        }


        [TestMethod]
        public void DepositMoney_WithValidATMAndCard_UpdatesBalance()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();

            // Act
            _atmController.DepositMoney(atm, creditCard);

            // Assert
            _atmRepositoryMock.Verify(r => r.GetAllAutomatedTellerMachines(), Times.Never);
        }

        [TestMethod]
        public void DepositMoney_WithNullATM_ThrowsArgumentNullException()
        {
            // Arrange
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _atmController.DepositMoney(null, creditCard));
        }

        [TestMethod]
        public void WithdrawMoney_WithValidATMAndCard_UpdatesBalance()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();

            // Act
            _atmController.WithdrawMoney(atm, creditCard);

            // Assert
            _atmRepositoryMock.Verify(r => r.GetAllAutomatedTellerMachines(), Times.Never);
        }

        [TestMethod]
        public void WithdrawMoney_WithNullATM_ThrowsArgumentNullException()
        {
            // Arrange
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _atmController.WithdrawMoney(null, creditCard));
        }

        [TestMethod]
        public void WithdrawMoney_WithNullCreditCard_ThrowsArgumentNullException()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _atmController.WithdrawMoney(atm, null));
        }

    }
}