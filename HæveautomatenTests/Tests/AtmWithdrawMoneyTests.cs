using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces;
using HæveautomatenTests.Factories;
using Moq;

namespace HæveautomatenTests.Tests
{
    [TestClass]
    public class AtmWithdrawMoneyTests
    {
        private Mock<IAutomatedTellerMachineController> _mockedAutomatedTellerMachineController;

        [TestInitialize]
        public void Setup()
        {
            _mockedAutomatedTellerMachineController = new Mock<IAutomatedTellerMachineController>();
        }

        [TestMethod]
        public void WithdrawMoney_WithValidParameters_ReturnsUpdatedAccountBalance()
        {
            // Assert
            CreditCardEntity creditCard = CreditCardFactory.CreateCreditCard();
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();

            // Act
            _mockedAutomatedTellerMachineController.Object.WithdrawMoney(creditCard, atm, 100);

            // Assert
            Assert.AreEqual(900, account.BalanceInMinorUnits);
        }

        [TestMethod]
        public void WithdrawMoney_WithInsufficientFunds_ThrowsInvalidOperationException()
        {
            // Assert
            AccountEntity account = AccountFactory.CreateAccount(balanceInMinorUnits: 10);
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine();

            // Act
            Assert.ThrowsException<InvalidOperationException>(() => atm.WithdrawMoney(account, 100));
        }

        [TestMethod]
        public void WithdrawMoney_WithLessThanMinimumExchangeAmount_ThrowsInvalidOperationException()
        {
            // Assert
            AccountEntity account = AccountFactory.CreateAccount();
            AutomatedTellerMachineEntity atm = AutomatedTellerMachineFactory.CreateAutomatedTellerMachine(minimumExchangeAmount: 50);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => atm.WithdrawMoney(account, 25));
        }
    }
}