namespace Hæveautomaten.Interfaces.Views
{
    public interface IBaseView
    {
        string GetUserInput();
        string GetUserInputWithTitle(string message);
        void CustomOutput(string output, bool clearConsole);
        void CustomMenu(string[] customMenuOptions, string customBackTitle = "Back");
    }
}