namespace Hæveautomaten.Views
{
    internal class CustomView : AbstractMenuView
    {
        internal static string GetUserInput()
        {
            return Console.ReadLine() ?? "";
        }

        internal static string GetUserInputWithTitle(string message)
        {
            Console.WriteLine(message);
            return GetUserInput();
        }

        internal static void CustomOutput(string output, bool clearConsole)
        {
            if (clearConsole)
            {
                Console.Clear();
            }

            Console.WriteLine(output);
        }
    }
}