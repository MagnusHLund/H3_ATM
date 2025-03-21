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
        private Mock<IAccountController> _accountControllerMock;
        private Mock<IBankController> _bankControllerMock;
        private Mock<IBaseView> _baseViewMock;
        private AutomatedTellerMachineController _atmController;

        [TestInitialize]
        public void Setup()
        {
            _atmRepositoryMock = new Mock<IAutomatedTellerMachineRepository>();
            _creditCardControllerMock = new Mock<ICreditCardController>();
            _accountControllerMock = new Mock<IAccountController>();
            _bankControllerMock = new Mock<IBankController>();
            _baseViewMock = new Mock<IBaseView>();

            _atmController = new AutomatedTellerMachineController(
                _atmRepositoryMock.Object,
                _bankControllerMock.Object,
                _creditCardControllerMock.Object,
                _accountControllerMock.Object,
                _baseViewMock.Object
            );
        }

        [TestMethod]
        public void CreateAutomatedTellerMachine_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            BankEntity bank = BankFactory.CreateBank();
            uint minimumExchangeAmount = 100;
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
        public void DepositMoney_WithValidATMAndAccount_UpdatesBalance()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            AccountEntity account = AccountFactory.CreateAccount();
            long depositAmount = 500;
            _baseViewMock.Setup(view => view.GetUserInputWithTitle("Enter the amount you want to deposit:")).Returns(depositAmount.ToString());
            _accountControllerMock.Setup(a => a.UpdateAccount(It.IsAny<AccountEntity>())).Returns(account);

            // Act
            long updatedBalance = _atmController.DepositMoney(atm, account);

            // Assert
            Assert.AreEqual(account.BalanceInMinorUnits, updatedBalance);
        }

        [TestMethod]
        public void DepositMoney_WithAmountBelowMinimum_ThrowsInvalidOperationException()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            AccountEntity account = AccountFactory.CreateAccount();
            _baseViewMock.Setup(view => view.GetUserInputWithTitle("Enter the amount you want to deposit:")).Returns("1");

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _atmController.DepositMoney(atm, account));
        }

        [TestMethod]
        public void WithdrawMoney_WithValidATMAndAccount_UpdatesBalance()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            AccountEntity account = AccountFactory.CreateAccount();
            long withdrawAmount = 500;
            _baseViewMock.Setup(view => view.GetUserInputWithTitle("Enter the amount you want to withdraw:")).Returns(withdrawAmount.ToString());
            _accountControllerMock.Setup(a => a.UpdateAccount(It.IsAny<AccountEntity>())).Returns(account);

            // Act
            long updatedBalance = _atmController.WithdrawMoney(atm, account);

            // Assert
            Assert.AreEqual(account.BalanceInMinorUnits, updatedBalance);
        }

        [TestMethod]
        public void WithdrawMoney_WithAmountAboveBalance_ThrowsInvalidOperationException()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            AccountEntity account = AccountFactory.CreateAccount();
            _baseViewMock.Setup(view => view.GetUserInputWithTitle("Enter the amount you want to withdraw:")).Returns("1000000");

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _atmController.WithdrawMoney(atm, account));
        }

        [TestMethod]
        public void WithdrawMoney_WithAmountBelowMinimum_ThrowsInvalidOperationException()
        {
            // Arrange
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();
            AccountEntity account = AccountFactory.CreateAccount();
            _baseViewMock.Setup(view => view.GetUserInputWithTitle("Enter the amount you want to withdraw:")).Returns("1");

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _atmController.WithdrawMoney(atm, account));
        }
    }
}