namespace Hæveautomaten.Views
{
    internal class AutomatedTellerMachineView : BaseView
    {
        internal static void AutomatedTellerMachineMainMenu(string bankName)
        {
            string[] automatedTellerMachineMenuOptions = {
                "Insert card",
                "Switch ATM",
            };

            DisplayHeader($"Welcome to {bankName}'s ATM");
            DisplayMenu(automatedTellerMachineMenuOptions);
        }

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