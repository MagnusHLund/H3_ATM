using Hæveautomaten.Interfaces.Controllers;

namespace Hæveautomaten.Controllers
{
    public class MainController : IMainController
    {
        public void HandleMainMenuDisplay()
        {
            // Display main menu
            // Gets user input
            // Calls HandleMainMenu with user input
        }

        public void HandleMainMenu(string input)
        {
            // Show main menu and await user input
            // If user input is 1, call AdminController
            // If user input is 2, call AutomatedTellerMachineController
            // If user input is 0, exit application
        }
    }
}