namespace Quiz.ConsoleUI.Common
{
    public class ConsoleConstants
    {
        public const int ConsoleWindowWidth = 102;
        public const int ConsoleWindowHeight = 35;


        public const int ConsoleWidth = 100;
        public const int ConsoleHeight = 30;


        // BackgroudColor

        private static ConsoleColor consoleBackgroundColor = ConsoleColor.DarkBlue;

        public static ConsoleColor BackgroundColor
        {
            get { return consoleBackgroundColor; }
            private set { consoleBackgroundColor = value; }
        }

        public static void SetBackgroundColor(string color)
        {
            if (Enum.TryParse(color, out ConsoleColor userChoice))
            {
                consoleBackgroundColor = userChoice;
            }

        }


        //Text Colors

        private static ConsoleColor consoleTextColor = ConsoleColor.Yellow;

        public static ConsoleColor TextColor
        {
            get { return consoleTextColor; }
            private set { consoleTextColor = value; }
        }

        public static void SetTextColor(string color)
        {
            if (Enum.TryParse(color, out ConsoleColor userChoice))
            {
                consoleTextColor = userChoice;
            }
        }

    }
}
