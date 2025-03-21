using Hæveautomaten.Views;
using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Views;
using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Interfaces.Repositories;
using System.Security.Authentication;

namespace Hæveautomaten.Controllers
{
    public class AutomatedTellerMachineController : IAutomatedTellerMachineController
    {
        private readonly IAutomatedTellerMachineRepository _automatedTellerMachineRepository;
        private readonly ICreditCardController _creditCardController;
        private readonly IAccountController _accountController;
        private readonly IBankController _bankController;
        private readonly IBaseView _baseView;

        public AutomatedTellerMachineController(
            IAutomatedTellerMachineRepository automatedTellerMachineRepository,
            IBankController bankController,
            ICreditCardController creditCardController,
            IAccountController accountController,
            IBaseView baseView
        )
        {
            _automatedTellerMachineRepository = automatedTellerMachineRepository;
            _bankController = bankController;
            _creditCardController = creditCardController;
            _accountController = accountController;
            _baseView = baseView;
        }

        public void HandleAutomatedTellerMachineMenu()
        {
            AutomatedTellerMachineEntity atm = SelectAutomatedTellerMachine();

            CreditCardEntity creditCard = _creditCardController.SelectCreditCard();
            AccountEntity account = _accountController.GetAccountByCard(creditCard);

            UseAutomatedTellerMachine(atm, creditCard, account);
        }

        public bool CreateAutomatedTellerMachine()
        {
            BankEntity bank = _bankController.SelectBank();
            uint minimumExchangeAmount = uint.Parse(_baseView.GetUserInputWithTitle("Enter the minimum exchange amount: "));

            AutomatedTellerMachineEntity atm = new AutomatedTellerMachineEntity(
                bank: bank,
                minimumExchangeAmount: minimumExchangeAmount
            );

            bool success = _automatedTellerMachineRepository.CreateAutomatedTellerMachine(atm);
            return success;
        }

        public bool DeleteAutomatedTellerMachine()
        {
            AutomatedTellerMachineEntity atm = SelectAutomatedTellerMachine();

            bool success = _automatedTellerMachineRepository.DeleteAutomatedTellerMachine(atm.AutomatedTellerMachineId);
            return success;
        }

        public void UseAutomatedTellerMachine(AutomatedTellerMachineEntity atm, CreditCardEntity creditCard, AccountEntity account)
        {
            string userInput = _baseView.GetUserInputWithTitle("Enter pin code:");

            if (userInput == creditCard.PinCode.ToString())
            {
                long accountBalance = account.BalanceInMinorUnits / 100;
                AutomatedTellerMachineView.AutomatedTellerMachineManagementMenu(accountBalance.ToString());

                string input = _baseView.GetUserInput();

                switch (input)
                {
                    case "1":
                        WithdrawMoney(atm, account);
                        break;
                    case "2":
                        DepositMoney(atm, account);
                        break;
                    default:
                        _baseView.CustomOutput("Invalid input");
                        break;
                }
            }
            else
            {
                _baseView.CustomOutput("Invalid pin code");
                throw new AuthenticationException("Invalid pin code");
            }
        }

        public List<AutomatedTellerMachineEntity> GetAllAutomatedTellerMachines()
        {
            List<AutomatedTellerMachineEntity> atms = _automatedTellerMachineRepository.GetAllAutomatedTellerMachines();
            return atms;
        }

        public long DepositMoney(AutomatedTellerMachineEntity atm, AccountEntity accountEntity)
        {
            long amountToDeposit = long.Parse(_baseView.GetUserInputWithTitle("Enter the amount you want to deposit:"));

            if (amountToDeposit < atm.MinimumExchangeAmount)
            {
                throw new InvalidOperationException("The amount is below the minimum exchange amount");
            }

            accountEntity.BalanceInMinorUnits += amountToDeposit;

            AccountEntity updatedAccount = _accountController.UpdateAccount(accountEntity);
            return updatedAccount.BalanceInMinorUnits;
        }

        public long WithdrawMoney(AutomatedTellerMachineEntity atm, AccountEntity accountEntity)
        {
            long amountToWithdraw = long.Parse(_baseView.GetUserInputWithTitle("Enter the amount you want to withdraw:"));

            if (amountToWithdraw < atm.MinimumExchangeAmount)
            {
                throw new InvalidOperationException("The amount is below the minimum exchange amount");
            }

            if (amountToWithdraw > accountEntity.BalanceInMinorUnits)
            {
                throw new InvalidOperationException("The amount is above the account balance");
            }

            accountEntity.BalanceInMinorUnits -= amountToWithdraw;


            AccountEntity updatedAccount = _accountController.UpdateAccount(accountEntity);
            return updatedAccount.BalanceInMinorUnits;
        }

        public AutomatedTellerMachineEntity SelectAutomatedTellerMachine()
        {
            List<AutomatedTellerMachineEntity> atms = GetAllAutomatedTellerMachines();
            string[] atmIdentifiers = atms.Select(atm => atm.ToString()).ToArray();

            _baseView.CustomMenu(atmIdentifiers);

            string userInput = _baseView.GetUserInput();
            int atmIndex = int.Parse(userInput) - 1;

            return atms[atmIndex];
        }
    }
}