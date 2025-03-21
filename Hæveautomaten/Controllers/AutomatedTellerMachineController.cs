using Hæveautomaten.Views;
using Hæveautomaten.Entities;
using Hæveautomaten.Interfaces.Views;
using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Interfaces.Repositories;

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
            IBaseView baseView
        )
        {
            _automatedTellerMachineRepository = automatedTellerMachineRepository;
            _bankController = bankController;
            _creditCardController = creditCardController;
            _baseView = baseView;
        }

        public void HandleAutomatedTellerMachineMenu()
        {
            // Call GetAllAutomatedTellerMachines() and store the result in a variable
            // Store variable for which atm is selected. The first is selected by default.

            // Show automated teller machine menu and await user input
            // If user input is 1, call UseAutomatedTellerMachine()
            // If user input is 2, call SwitchAutomatedTellerMachine()
            // If user input is 0, return to main menu

            AutomatedTellerMachineEntity atm = SelectAutomatedTellerMachine();

            while (true)
            {
                CreditCardEntity creditCard = _creditCardController.SelectCreditCard();
                AccountEntity account = _accountController.GetAccountByCard(creditCard);

                string bankName = atm.Bank.ToString();

                AutomatedTellerMachineView.AutomatedTellerMachineMainMenu(bankName);
            }
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

        public void UseAutomatedTellerMachine(AutomatedTellerMachineEntity atm)
        {
            // Use an automated teller machine
        }

        public AutomatedTellerMachineEntity SwitchAutomatedTellerMachine(List<AutomatedTellerMachineEntity> atms)
        {
            // Switch to another automated teller machine
            // Select an atm from the list and return it

            throw new NotImplementedException();
        }

        public List<AutomatedTellerMachineEntity> GetAllAutomatedTellerMachines()
        {
            List<AutomatedTellerMachineEntity> atms = _automatedTellerMachineRepository.GetAllAutomatedTellerMachines();
            return atms;
        }

        public void DepositMoney(AutomatedTellerMachineEntity atm, CreditCardEntity creditCard)
        {

        }

        public void WithdrawMoney(AutomatedTellerMachineEntity atm, CreditCardEntity creditCard)
        {
            // Deposit money into the atm
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