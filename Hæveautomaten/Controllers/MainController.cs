using Hæveautomaten.Views;
using Hæveautomaten.Interfaces.Controllers;

namespace Hæveautomaten.Controllers
{
    public class MainController : IMainController
    {
        private readonly IAdminController _adminController;
        private readonly IAutomatedTellerMachineController _automatedTellerMachineController;

        public MainController(IAdminController adminController, IAutomatedTellerMachineController automatedTellerMachineController)
        {
            _adminController = adminController;
            _automatedTellerMachineController = automatedTellerMachineController;
        }

        public void HandleMainMenuDisplay()
        {
            while (true)
            {
                MainView.MainMenu();

                string input = CustomView.GetUserInput();
                HandleMainMenuInput(input);
            }
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
                case "0":
                    CloseApplication();
                    break;
                default:
                    throw new InvalidOperationException("Invalid input");
            }
        }

        private static void CloseApplication()
        {
            Environment.Exit(0);
        }
    }
}