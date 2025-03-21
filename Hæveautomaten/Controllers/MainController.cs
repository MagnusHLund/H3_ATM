using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Interfaces.Views;
using Hæveautomaten.Views;

namespace Hæveautomaten.Controllers
{
    public class MainController : IMainController
    {
        private readonly IAdminController _adminController;
        private readonly IAutomatedTellerMachineController _automatedTellerMachineController;
        private readonly IBaseView _baseView;

        public MainController(
            IAdminController adminController,
            IAutomatedTellerMachineController automatedTellerMachineController,
            IBaseView baseView
        )
        {
            _adminController = adminController;
            _automatedTellerMachineController = automatedTellerMachineController;
            _baseView = baseView;
        }

        public void HandleMainMenuDisplay()
        {
            MainView.MainMenu();

            string input = _baseView.GetUserInput();
            HandleMainMenuInput(input);
        }

        public void HandleMainMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    _adminController.HandleAdminMenuDisplay();
                    break;
                case "2":
                    _automatedTellerMachineController.HandleAutomatedTellerMachineMenu();
                    break;
                default:
                    throw new InvalidOperationException("Invalid input");
            }
        }
    }
}