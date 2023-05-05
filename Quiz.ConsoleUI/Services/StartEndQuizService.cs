using Microsoft.EntityFrameworkCore;
using Quiz.ConsoleUI.Common;
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

       

        public void StartQuiz(string quizName)
        {
            var dbContext = new ApplicationDbContext();

            Stopwatch sw = Stopwatch.StartNew();

             var quiz = dbContext.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefault(q => q.Title == quizName);



            var random = new Random();
            var questions = new List<int>();
            int questionCounter = 0;

            int userPoints = 0;

            while (true)
            {

                if (questionCounter == 10) break;


                int[] questionsIds = quiz!.Questions.Select(q => q.Id).ToArray();
                int randomId = random.Next(0, questionsIds.Length);


                Question? question = quiz.Questions
                                    .FirstOrDefault(q => q.Id == questionsIds[randomId]);
                questionCounter++;

                string questionTitle = String.Format(Messages.QuestionTitleMessage, questionCounter, question!.Title);

                if (question == null || questions.Contains(question!.Id))
                {
                    continue;
                }



                int currAnswer = 0;

                options = new List<Option>();


                foreach (var answer in question.Answers)
                {
                    char[] answerIndex = "ABCDE".ToArray();

                    options.Add(new Option($"{answerIndex[currAnswer]}. {answer.Title}", null!));


                    currAnswer++;
                }


                var userAnswer = new Answer()
                {
                    Title = Utilities.ChooseAnswer(options, questionTitle)
                };


                Answer correctAnswer = question.Answers.First(a => a.IsCorrect);



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
                }

                questions.Add(question.Id);

            }

            sw.Stop();
            TimeSpan ts = sw.Elapsed;

            string result = String.Format(Messages.CurrentQuizResult, quiz!.Title, userPoints, ts.ToString("mm\\:ss\\.ff"));

            EndQuiz(result);

            Console.ReadKey();

        }


        public void StartRandomQuiz()
        {
            var dbContext = new ApplicationDbContext();
            Stopwatch sw = Stopwatch.StartNew();

            var questions = dbContext.Questions
                .Include(q => q.Answers)
                .OrderBy(x => Guid.NewGuid())
                .Take(10);


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
                }

                questionCounter++;
            }

            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            string result = String.Format(Messages.CurrentQuizResult, Messages.RandomQuizName, userPoints, ts.ToString("mm\\:ss\\.ff"));

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
    }
}
