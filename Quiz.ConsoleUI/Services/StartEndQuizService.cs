using Microsoft.EntityFrameworkCore;
using Quiz.ConsoleUI.Common;
using Quiz.ConsoleUI.Models;
using Quiz.ConsoleUI.Views;

using Quiz.Data;
using Quiz.Models;


using System.Diagnostics;
using System.Runtime.Versioning;

namespace Quiz.ConsoleUI.Services
{
    [SupportedOSPlatform("windows")]
    public class StartEndQuizService : IStartEndQuizService
    {
        private List<Option> options;

        public StartEndQuizService()
        {
            
            this.options = new List<Option>();
        }

       

        public void StartQuiz(string quizTitle)
        {
            var dbContext = new ApplicationDbContext();

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

            if (ResultsService.IsNewBestScore(dbContext, quizTitle, quizReport.UserPoints, quizReport.UserElapsedTime))
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
           var mainMenu = new MainMenuService();
            // Create options that you want your menu to have
            options = new List<Option>
            {
                new Option(Messages.BackToMainMenuMessage, () => mainMenu.RunInteractiveMenu()),
                new Option(Messages.Exit, () => Environment.Exit(0)),
            };

            Utilities.ChooseOption(options, result);



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
