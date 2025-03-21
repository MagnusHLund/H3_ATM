using Hæveautomaten.Interfaces.Views;

namespace Hæveautomaten.Views
{
    public class BaseView : IBaseView
    {
        public string GetUserInput()
        {
            return Console.ReadLine() ?? "";
        }

        public string GetUserInputWithTitle(string message)
        {
            Console.WriteLine(message);
            return GetUserInput();
        }

        public void CustomOutput(string output)
        {

            Console.WriteLine(output);
        }

        public void CustomMenu(string[] customMenuOptions, string customBackTitle = "Back")
        {
            DisplayMenu(customMenuOptions, customBackTitle);
        }

        public static void DisplayHeader(string headerTitle)
        {
            int lineLength = headerTitle.Length + 4;
            string border = new string('=', lineLength);

            //Console.Clear();
            Console.WriteLine(border);
            Console.WriteLine($"  {headerTitle}  ");
            Console.WriteLine(border);
            Console.WriteLine();
        }

        private protected static void DisplayMenu(string[] options, string customBackTitle = "Back")
        {
            for (int i = 0; i < options.Length; i++)
            {
                ConsoleColor consoleColor = i % 2 == 0 ? ConsoleColor.White : ConsoleColor.Gray;
                Console.ForegroundColor = consoleColor;

                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            Console.WriteLine($"0. {customBackTitle}");
        }
    }
}