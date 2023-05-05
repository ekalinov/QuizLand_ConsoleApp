using Quiz.ConsoleUI.Common;
using Quiz.ConsoleUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;


namespace Quiz.ConsoleUI.Views
{
    [SupportedOSPlatform("windows")]
    public class InteractivMenu
    {

        /// <summary>
        /// Print window with Logo, borders and body text
        /// </summary>
        /// <param name="bodyText">Body as a string</param>
       
        private static void DrawInteractivWindow(string bodyText)
        {
            {
                
                Console.BackgroundColor = ConsoleConstants.BackgroundColor;
                Console.ForegroundColor = ConsoleConstants.TextColor;
                Console.SetWindowSize(ConsoleConstants.ConsoleWindowWidth, ConsoleConstants.ConsoleWindowHeight);

                //always start drawing border from point (0,0);
                Console.SetCursorPosition(0, 0);





                string logo = @"
╔════════════════════════════════════════════════════════════════════════════════════════════════════╗
║                   ██████╗ ██╗   ██╗██╗███████╗██╗      █████╗ ███╗   ██╗██████╗                    ║
║                  ██╔═══██╗██║   ██║██║╚══███╔╝██║     ██╔══██╗████╗  ██║██╔══██╗                   ║
║                  ██║   ██║██║   ██║██║  ███╔╝ ██║     ███████║██╔██╗ ██║██║  ██║                   ║
║                  ██║▄▄ ██║██║   ██║██║ ███╔╝  ██║     ██╔══██║██║╚██╗██║██║  ██║                   ║
║                  ╚██████╔╝╚██████╔╝██║███████╗███████╗██║  ██║██║ ╚████║██████╔╝                   ║
║                   ╚══▀▀═╝  ╚═════╝ ╚═╝╚══════╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═════╝                    ║
╠════════════════════════════════════════════════════════════════════════════════════════════════════╣";


                //Drawing the Body of the window 

                string body = Utilities.BodyText(bodyText);





                int numLines = body.Split('\n').Length;



                string emptyRows = string.Empty;

                for (int i = 0; i < ConsoleConstants.ConsoleHeight - 9 - numLines; i++)
                {
                    emptyRows += "║" + new string(' ', ConsoleConstants.ConsoleWidth) + "║" + "\n";

                }



                string endLine = "╚";
                endLine += new string('═', ConsoleConstants.ConsoleWidth);
                endLine += "╝";

                var complete = logo + '\n' + body + emptyRows + endLine;


                Console.WriteLine(complete);

            }




        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="selectedOption"></param>
        public static void WriteInteractivMenu(List<Option> options, Option selectedOption)
        {
            var sb = new StringBuilder();

            foreach (Option option in options)
            {

                if (option.Name == Messages.Exit)
                {
                    sb.AppendLine();
                }

                if (option == selectedOption)
                {
                    sb.Append("> ");
                }
                else
                {
                    sb.Append(' ');
                }

                sb.AppendLine(option.Name);
            }

            DrawInteractivWindow(sb.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="selectedOption"></param>
        /// <param name="header"></param>
        public static void WriteInteractivMenu(List<Option> options, Option selectedOption, string header)
        {
            var sb = new StringBuilder();

            sb.AppendLine(header);
            sb.AppendLine();

            foreach (Option option in options)
            {
                if (option.Name == Messages.BackToMainMenuMessage || 
                    option.Name == Messages.Exit || 
                    option.Name ==Messages.BackToSettingsMenu)
                {
                    sb.AppendLine();
                }

                if (option == selectedOption)
                {
                    sb.Append("> ");
                }
                else
                {
                    sb.Append(' ');
                }

                sb.AppendLine(option.Name);
            }


            DrawInteractivWindow(sb.ToString());
        }

        public static void CorectAnswer(List<Option> options, string correctAnswer, string questionTitle)
        {
            var sb = new StringBuilder();

            sb.AppendLine(questionTitle);
            sb.AppendLine();

            foreach (Option option in options)
            {
                if (option.Name == Messages.BackToMainMenuMessage)
                {
                    sb.AppendLine();
                }

                sb.Append(option.Name);

                string answ = option.Name[3..];

                if (answ == correctAnswer)
                {
                    sb.AppendLine(" <-Correct Answer ");
                }
                else
                {
                    sb.AppendLine(" ");
                }

            }


            DrawInteractivWindow(sb.ToString());
        }

        public static void InCorectAnswer(List<Option> options, string correctAnswer, string userAnswer, string questionTitle)
        {
            var sb = new StringBuilder();

            sb.AppendLine(questionTitle);
            sb.AppendLine();

            foreach (Option option in options)
            {
                if (option.Name == Messages.BackToMainMenuMessage)
                {
                    sb.AppendLine();
                }

                sb.Append(option.Name);

                string answ = option.Name[3..];

                if (answ == correctAnswer)
                {
                    sb.AppendLine(" <-Correct Answer ");
                }
                else if (answ == userAnswer)
                {
                    sb.AppendLine(" <-Your Answer ");
                }
                else
                {
                    sb.AppendLine(" ");
                }

            }


            DrawInteractivWindow(sb.ToString());
        }
    }

    

}
