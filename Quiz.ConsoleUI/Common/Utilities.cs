using Quiz.ConsoleUI.Views;
using System.Runtime.Versioning;
using System.Text;

namespace Quiz.ConsoleUI.Common
{
    [SupportedOSPlatform("windows")]
    public class Utilities
    {

        /// <summary>
        ///     Writes the specified data, followed by the current line terminator, 
        ///     to the standard output stream, while wrapping lines that would otherwise 
        ///     break words.
        /// </summary>
        /// <param name="paragraph">The value to write.</param>
        /// <param name="tabSize">The value that indicates the column width of tab 
        ///   characters.</param>
        public static string WordWrap(string paragraph, int tabSize = 8)
        {
            var sb = new StringBuilder();

            string[] lines = paragraph
                        .Replace("\t", new String(' ', tabSize))
                        .Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                string process = lines[i];

                var wrapped = new List<string>();

                while (process.Length > ConsoleConstants.ConsoleWidth - 10)
                {
                    int wrapAt = process.LastIndexOf(' ', Math.Min(Console.WindowWidth - 10, process.Length));
                    if (wrapAt <= 0) break;

                    wrapped.Add(process[..wrapAt]);
                    process = process.Remove(0, wrapAt + 1);
                }


                foreach (string wrap in wrapped)
                {
                    sb.AppendLine(wrap);
                }

                sb.AppendLine(process);

            }
            return sb.ToString();
        }


        /// <summary>
        /// Builds the window body text with wrapped sentances and borders.
        /// </summary>
        /// <param name="bodyText">The value to write</param>
        public static string BodyText(string bodyText)
        {
            var sb = new StringBuilder();

            string[] bodyLines = bodyText.Split('\n', StringSplitOptions.None).ToArray();


            foreach (var line in bodyLines)
            {
                string cleanedLine = line.Replace("\n", "").Replace("\r", "");

                if (cleanedLine.Length >= ConsoleConstants.ConsoleWidth - 2)
                {
                    string wrappedLine = Utilities.WordWrap(cleanedLine);


                    string[] wrappedLines = wrappedLine.Split('\n', StringSplitOptions.None).ToArray();

                    foreach (var wrLine in wrappedLines)
                    {
                        string cleanedWrLine = wrLine.Replace("\n", "").Replace("\r", "");

                        sb.AppendLine(Utilities.WriteRow(cleanedWrLine));
                    }

                    continue;
                }

                sb.AppendLine(Utilities.WriteRow(cleanedLine));

            }

            return sb.ToString();

        }

        /// <summary>
        /// Writes a single line of body text with correct borders and alingment
        /// </summary>
        /// <param name="rowtext">The value to write</param>
        public static string WriteRow(string rowtext)
        {
            return "║" + rowtext + new string(' ', ConsoleConstants.ConsoleWidth - rowtext.Length) + "║";
        }



        /// <summary>
        /// Arrow controlled menu
        /// </summary>
        /// <param name="options"> List of Answers to scroll</param>
        /// <param name="questionTitle">Question name</param>
        public static string ChooseAnswer(List<Option> options, string questionTitle)
        {

            string userAnswer = string.Empty;

            int index = 0;

            // Store key info in here
            ConsoleKeyInfo keyinfo;

            bool isEnvoked = false;

            while (!isEnvoked)
            {
                InteractivMenu.WriteInteractivMenu(options, options[index], questionTitle);
                keyinfo = Console.ReadKey();

                // Handle each key input (down arrow will write the menu again with a different selected item)
                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        InteractivMenu.WriteInteractivMenu(options, options[index], questionTitle);

                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        InteractivMenu.WriteInteractivMenu(options, options[index], questionTitle);

                    }
                }
                // Handle different action for the option
                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    userAnswer = options[index].Name[3..];
                    isEnvoked = true;
                    index = 0;

                }

            }

            return userAnswer;


        }

        /// <summary>
        /// Arrow controlled menu
        /// </summary>
        /// <param name="options">List of possible options</param>
        public static void ChooseOption(List<Option> options)
        {

            string userAnswer = string.Empty;

            int index = 0;

            // Store key info in here
            ConsoleKeyInfo keyinfo;

            do
            {
                InteractivMenu.WriteInteractivMenu(options, options[index]);
                keyinfo = Console.ReadKey();

                // Handle each key input (down arrow will write the menu again with a different selected item)
                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        InteractivMenu.WriteInteractivMenu(options, options[index]);

                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        InteractivMenu.WriteInteractivMenu(options, options[index]);

                    }
                }
                // Handle different action for the option
                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    options[index].Selected.Invoke();
                    index = 0;
                }

            } while (keyinfo.Key != ConsoleKey.Escape);



        }


        /// <summary>
        /// Arrow controlled menu
        /// </summary>
        /// <param name="options">List of possible options</param>
        /// <param name="optionDetails">Header or Unchoiceable option</param>
        public static void ChooseOption(List<Option> options, string optionDetails)
        {
            string userAnswer = string.Empty;

            int index = 0;

            // Store key info in here
            ConsoleKeyInfo keyinfo;

            do
            {
                InteractivMenu.WriteInteractivMenu(options, options[index], optionDetails);
                keyinfo = Console.ReadKey();

                // Handle each key input (down arrow will write the menu again with a different selected item)
                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        InteractivMenu.WriteInteractivMenu(options, options[index], optionDetails);

                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        InteractivMenu.WriteInteractivMenu(options, options[index], optionDetails);

                    }
                }
                // Handle different action for the option
                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    options[index].Selected.Invoke();
                    index = 0;
                }

            } while (keyinfo.Key != ConsoleKey.Escape);



        }


       
    }
}
