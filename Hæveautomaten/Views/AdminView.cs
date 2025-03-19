namespace Hæveautomaten.Views
{
    internal class AdminView : AbstractMenuView
    {
        internal static void AdminMenu()
        {
            string[] adminMenuOptions = {
                "Create person",
                "Delete person",
                "Create card",
                "Delete card",
                "Create account",
                "Delete account",
                "Create bank",
                "Delete bank",
                "Create ATM",
                "Delete ATM",
            };

            DisplayHeader("Admin menu");
            DisplayMenu(adminMenuOptions);
        }
    }
}