namespace Hæveautomaten.Views
{
    internal class MainView : BaseView
    {
        internal static void MainMenu()
        {
            string[] mainMenuOptions = {
                "Admin menu",
                "ATM menu",
            };

            string customBackTitle = "Exit program";

            DisplayHeader("Main menu");
            DisplayMenu(mainMenuOptions, customBackTitle);
        }
    }
}