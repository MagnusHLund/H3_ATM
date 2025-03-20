using Hæveautomaten.Views;
using Hæveautomaten.Interfaces.Controllers;

namespace Hæveautomaten.Controllers
{
    public class AdminController : IAdminController
    {
        private IAccountController _accountController;
        private IPersonController _personController;
        private ICreditCardController _creditCardController;
        private IBankController _bankController;
        private IAutomatedTellerMachineController _automatedTellerMachineController;

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
            _automatedTellerMachineController = automatedTellerMachineController;
        }

        public void HandleAdminMenuDisplay()
        {
            AdminView.AdminMenu();
            string userInput = CustomView.GetUserInput();

            HandleAdminMenuInput(userInput);
        }

        public void HandleAdminMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    _personController.CreatePerson();
                    break;
                case "2":
                    _personController.DeletePerson();
                    break;
                case "3":
                    _creditCardController.DeleteCreditCard();
                    break;
                case "4":
                    _creditCardController.DeleteCreditCard();
                    break;
                case "5":
                    _accountController.CreateAccount();
                    break;
                case "6":
                    _accountController.DeleteAccount();
                    break;
                case "7":
                    _bankController.CreateBank();
                    break;
                case "8":
                    _bankController.DeleteBank();
                    break;
                case "9":
                    _automatedTellerMachineController.CreateAutomatedTellerMachine();
                    break;
                case "10":
                    _automatedTellerMachineController.DeleteAutomatedTellerMachine();
                    break;
                case "0":
                    break;
                default:
                    throw new InvalidOperationException("Invalid input");
            }
        }
    }
}