using Microsoft.EntityFrameworkCore;
using Quiz.ConsoleUI.Common;
using Quiz.ConsoleUI.Models;
using Quiz.ConsoleUI.Views;

using Quiz.Data;
using Quiz.Models;


using System.Diagnostics;
using System.Runtime.Versioning;
using System.Text;

namespace Quiz.ConsoleUI.Services
{
    [SupportedOSPlatform("windows")]
    public class StartEndQuizService : IStartEndQuizService
    {
        private List<Option> options;
        private readonly ApplicationDbContext dbContext;
        private  readonly IMainMenuService mainMenuService;

        public StartEndQuizService(ApplicationDbContext _dbContext, IMainMenuService _mainMenuService)
        {
            this.mainMenuService= _mainMenuService;
            this.dbContext= _dbContext;
            this.options = new List<Option>();
        }

        public void ChooseQuiz()
        {
            //var dbContext = new ApplicationDbContext();

            var quizzes = dbContext.Quizzes
                .Select(q => new QuizViewModel { Title = q.Title, Description = q.Description! })
                .ToList();

            // Create options that you want your menu to have
            options = new List<Option>();
            foreach (var quiz in quizzes)
            {
                options.Add(new Option(quiz.Title, () => ConfirmStartQuiz(quiz.Title, quiz.Description)));

            }
            options.Add(new Option(Messages.BackToMainMenuMessage, () => mainMenuService.RunInteractiveMenu()));

            Utilities.ChooseOption(options, Messages.ChooseQuiz);
        }

        public void StartQuiz(string quizTitle)
        {
           // var dbContext = new ApplicationDbContext();

            Stopwatch sw = Stopwatch.StartNew();

             var questions = dbContext.Questions
                .Where(q => q.Quiz.Title == quizTitle)
                .Include(q => q.Answers)
                .OrderBy(x => Guid.NewGuid())
                .Take(10)
                .ToList();


            QuizReportModel quizReport = Quiz(questions, quizTitle);

            sw.Stop();
            TimeSpan ts = sw.Elapsed;




            quizReport.UserElapsedTime = ts;

            if (ResultsService.IsNewBestScoreStatic(dbContext, quizTitle, quizReport.UserPoints, quizReport.UserElapsedTime))
            {
                //TODO: 
            }

            string result = ResultsService.ShowReport(quizReport);

            EndQuiz(result);

            Console.ReadKey();

        }

        public void StartRandomQuiz()
        {
            var dbContext = new ApplicationDbContext();
            Stopwatch sw = Stopwatch.StartNew();

            string quizTitle = Messages.RandomQuiz;

            var questions = dbContext.Questions
                .Include(q => q.Answers)
                .OrderBy(x => Guid.NewGuid())
                .Take(10)
                .ToList();


            QuizReportModel quizReport = Quiz( questions, quizTitle);

            sw.Stop();
            TimeSpan ts = sw.Elapsed;

            quizReport.UserElapsedTime = ts;

            string result = ResultsService.ShowReport(quizReport);
            EndQuiz(result);



            Console.ReadKey();
        }

        public void EndQuiz(string result)
        {
            options = new List<Option>
            {
                new Option(Messages.BackToMainMenuMessage, () => mainMenuService.RunInteractiveMenu()),
                new Option(Messages.Exit, () => Environment.Exit(0)),
            };

            Utilities.ChooseOption(options, result);



        }


        public void ConfirmStartQuiz(string quizName, string quizDescrioption)
        {
            // var startEndQuizService = new StartEndQuizService();
            var sb = new StringBuilder();

            sb
           .AppendLine(String.Format(Messages.QuizToAttendMessage, quizName))
                .AppendLine("")
                .AppendLine("")
                .AppendLine(quizDescrioption);


            // Create options that you want your menu to have
            options = new List<Option>
            {
                new Option(String.Format(Messages.StartQuiz,quizName), () => StartQuiz(quizName)),
                new Option(Messages.BackToMainMenuMessage, () => mainMenuService.RunInteractiveMenu()),
            };

            Utilities.ChooseOption(options, sb.ToString());

        }

        public void ConfirmStartRandomQuiz()
        {
            // var startEndQuizService = new StartEndQuizService();


            var sb = new StringBuilder();

            sb
           .AppendLine(String.Format(Messages.QuizToAttendMessage, Messages.RandomQuizName))
                .AppendLine("")
                .AppendLine("")
                .AppendLine(Messages.RandomQuizDescription);


            // Create options that you want your menu to have
            options = new List<Option>
            {
                new Option(Messages.RandomQuizMessage, () =>StartRandomQuiz()),
                new Option(Messages.BackToMainMenuMessage, () => mainMenuService.RunInteractiveMenu()),
            };

            Utilities.ChooseOption(options, sb.ToString());

        }

        public QuizReportModel Quiz(List<Question> questions, string quizTitle)
        {
            var quizReport = new QuizReportModel()
            {
                QuizTitle = quizTitle
            };
            int questionCounter = 1;
            int userPoints = 0;

            foreach (var question in questions)
            {

                options = new List<Option>();
                int currAnswer = 0;


                foreach (var answer in question.Answers)
                {
                    char[] answerIndex = "ABCDE".ToArray();

                    options.Add(new Option($"{answerIndex[currAnswer]}. {answer.Title}", null!)); ;


                    currAnswer++;
                }

                string questionTitle = String.Format(Messages.QuestionTitleMessage, questionCounter, question.Title);

                var userAnswer = new Answer()
                {
                    Title = Utilities.ChooseAnswer(options, questionTitle)
                };



                Answer correctAnswer = question.Answers.First(a => a.IsCorrect);

                var reportAnswers = new QuestionReportModel()
                {
                    QuestionTitle = questionTitle,
                    CorrectAnswerTitle = correctAnswer.Title,

                };

                if (userAnswer.Title == correctAnswer.Title)
                {

                    InteractivMenu.CorectAnswer(options, userAnswer.Title, questionTitle);

                    Console.ReadKey();

                    userPoints++;
                }
                else

                {
                    InteractivMenu.InCorectAnswer(options, correctAnswer.Title, userAnswer.Title, questionTitle);

                    Console.ReadKey();

                    reportAnswers.UserAnswerTitle = userAnswer.Title;
                }


                quizReport.Questions.Add(reportAnswers);
                questionCounter++;


            }

            quizReport.UserPoints = userPoints;

            return quizReport;
        }
    }
}
