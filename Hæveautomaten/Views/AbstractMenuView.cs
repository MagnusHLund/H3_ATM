namespace Hæveautomaten.Views
{
    internal abstract class AbstractMenuView
    {
        private protected static void DisplayHeader(string headerTitle)
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