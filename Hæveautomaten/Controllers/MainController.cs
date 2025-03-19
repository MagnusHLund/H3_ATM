using Hæveautomaten.Interfaces;

namespace Hæveautomaten.Controllers
{
    public class MainController : IMainController
    {
        public void HandleMainMenu()
        {
            // Show main menu and await user input
            // If user input is 1, call AdminController
            // If user input is 2, call AutomatedTellerMachineController
            // If user input is 0, exit application
        }
    }
}