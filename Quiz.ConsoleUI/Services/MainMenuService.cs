using Microsoft.Extensions.Options;
using Quiz.ConsoleUI.Common;
using Quiz.ConsoleUI.Models;

using Quiz.Data;
using System.Text;

namespace Quiz.ConsoleUI.Services
{
    public class MainMenuService : IMainMenuService

    {
        
        private  List<Option> options;

        public MainMenuService()
        {
                this.options = new List<Option>();
        }

        

        public   void RunInteractiveMenu()
        {
            Console.Clear();
            // Create options that you want your menu to have
            options = new List<Option>
            {
                new Option(Messages.ChooseQuiz, () => ChooseQuiz()),
                new Option(Messages.RandomQuiz, () => ConfirmStartRandomQuiz()),
                new Option(Messages.Results, () =>  Results()),
                new Option(Messages.Settings, () =>  Settings()),

                new Option(Messages.Exit, () => Environment.Exit(0)),
            };

            Utilities.ChooseOption(options, Messages.MainMenu);
        }



        public  void ChooseQuiz()
        {
            var dbContext = new ApplicationDbContext();

            var quizzes = dbContext.Quizzes
                .Select(q => new QuizViewModel { Title = q.Title, Description = q.Description! })
                .ToList();

            // Create options that you want your menu to have
            options = new List<Option>();
            foreach (var quiz in quizzes)
            {
                options.Add(new Option(quiz.Title, () => ConfirmStartQuiz(quiz.Title,quiz.Description)));

            }
            options.Add(new Option(Messages.BackToMainMenuMessage, () => RunInteractiveMenu()));

            Utilities.ChooseOption(options,Messages.ChooseQuiz);
        }
               
        public  void ConfirmStartQuiz(string quizName, string quizDescrioption)
        {
            var startEndQuizService = new StartEndQuizService();
            var sb = new StringBuilder();

            sb
           .AppendLine(String.Format(Messages.QuizToAttendMessage, quizName))
                .AppendLine("")
                .AppendLine("")
                .AppendLine(quizDescrioption);


            // Create options that you want your menu to have
            options = new List<Option>
            {
                new Option(String.Format(Messages.StartQuiz,quizName), () => startEndQuizService.StartQuiz(quizName)),
                new Option(Messages.BackToMainMenuMessage, () => RunInteractiveMenu()),
            };

            Utilities.ChooseOption(options, sb.ToString());

        }
              
        public  void ConfirmStartRandomQuiz()
        {
            var startEndQuizService = new StartEndQuizService();


            var sb = new StringBuilder();

            sb
           .AppendLine(String.Format(Messages.QuizToAttendMessage, Messages.RandomQuizName))
                .AppendLine("")
                .AppendLine("")
                .AppendLine(Messages.RandomQuizDescription);


            // Create options that you want your menu to have
            options = new List<Option>
            {
                new Option(Messages.RandomQuizMessage, () => startEndQuizService.StartRandomQuiz()),
                new Option(Messages.BackToMainMenuMessage, () => RunInteractiveMenu()),
            };

            Utilities.ChooseOption(options,sb.ToString());

        }
                
        public  void Results()
        {
            Console.WriteLine("TODO:Results");
        }

        public  void Settings()
        {
            var sb = new StringBuilder();

            var settingsService = new SettingsService();
            sb
           .AppendLine(Messages.Settings)
                .AppendLine("");


            // Create options that you want your menu to have
            options = new List<Option>
            {
                new Option(Messages.SetBackgroundColor, () => settingsService.ChooseBackgroungColor()),
                new Option(Messages.SetTextColor, () => settingsService.ChooseTextColor()),
                new Option(Messages.BackToMainMenuMessage, () => RunInteractiveMenu()),
            };

            Utilities.ChooseOption(options, sb.ToString());


        }
        



    }
}