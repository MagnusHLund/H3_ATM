using Hæveautomaten.Interfaces.Controllers;

namespace Hæveautomaten.Controllers
{
    public class AdminController : IAdminController
    {
        private IAccountController _accountController;
        private IPersonController _personController;
        private ICreditCardController _creditCardController;
        private IBankController _bankController;
        private IAutomatedTellerMachineController _AutomatedTellerMachineController;

        public AdminController(IAccountController accountController,
            IPersonController personController,
            ICreditCardController creditCardController,
            IBankController bankController,
            IAutomatedTellerMachineController automatedTellerMachineController
        )
        {
            _accountController = accountController;
            _personController = personController;
            _creditCardController = creditCardController;
            _bankController = bankController;
            _AutomatedTellerMachineController = automatedTellerMachineController;
        }

        public void HandleAdminMenuDisplay()
        {
            // Show menu
        }

        public void HandleAdminMenu(string input)
        {
            // Show admin menu and await user input
            // 1 "Create person" - Call CreatePerson()
            // 2 "Delete person" - Call DeletePerson()
            // 3 "Delete Card" - Call DeleteCard() 
            // 4 "Delete Card" - Call DeleteCard() 
            // 5 "Create account" - Call CreateAccount()
            // 6 "Delete account" - Call DeleteAccount()
            // 7 "Create bank" - Call CreateBank()
            // 8 "Delete bank" - Call DeleteBank()
            // 9 "Create ATM" - Call CreateAutomatedTellerMachine()
            // 10 "Delete ATM" - Call DeleteAutomatedTellerMachine()
            // 0 "Back" - Return to the main menu
        }

        public void CreateAccount()
        {
            // Create a new account

            // First pick a bank
            // Then pick a person
            // Then create the account which connects the bank and the person.

            // Call accountController().CreateAccount(account)  
        }

        public void DeleteAccount()
        {
            // Get all accounts and store them in a variable
            // Select an account pass it to accountController().DeleteAccount(account)
        }

        public void CreateAutomatedTellerMachine()
        {
            // Create a new automated teller machine

            // First pick a bank, which owns the machine.
            // Then decide the minimum exchange amount.

            // Call automatedTellerMachineController().CreateAutomatedTellerMachine(atm)
        }

        public void DeleteAutomatedTellerMachine()
        {
            // Get all automated teller machines and store them in a variable
            // Select an automated teller machine pass it to automatedTellerMachineController().DeleteAutomatedTellerMachine(atm)
        }

        public void CreateBank()
        {
            // Create a new bank. Input bank name.
            // Create bank and save it in the database

            // Call bankController().CreateBank(bank)
        }

        public void DeleteBank()
        {
            // Get all banks and store them in a variable
            // Select a bank pass it to bankController().DeleteBank(bank)
        }

        public void CreateCreditCard()
        {
            // First pick a person, that owns the credit card
            // Get the values of the credit card number, expiration date, and cvv, 
            // Expiration date, pin code, isBlocked, associatedAccountNumber

            // Call creditCardController().CreateCreditCard(creditCard)
        }

        public void DeleteCreditCard()
        {
            // Get all credit cards and store them in a variable
            // Select an credit cards pass it to creditCardController().DeleteCreditCard(creditCard)
        }

        public void CreatePerson()
        {
            // Input the persons First name and last name. optional middlename.

            // Call personController().CreatePerson(person)
        }

        public void DeletePerson()
        {
            // Get all people and store them in a variable
            // Select a person pass it to personController().DeletePerson(person)
        }
    }
}