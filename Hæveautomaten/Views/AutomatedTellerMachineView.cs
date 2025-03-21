namespace Hæveautomaten.Views
{
    internal class AutomatedTellerMachineView : BaseView
    {
        internal static void AutomatedTellerMachineManagementMenu(string balance)
        {
            string[] automatedTellerMachineManagementMenuOptions = {
                "Withdraw",
                "Deposit",
            };

            DisplayHeader($"Your bank balance is: {balance}");
            DisplayMenu(automatedTellerMachineManagementMenuOptions);
        }
    }
}