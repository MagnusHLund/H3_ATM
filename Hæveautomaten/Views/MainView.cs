namespace Hæveautomaten.Views
{
    internal class MainView : AbstractMenuView
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