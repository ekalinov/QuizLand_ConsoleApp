using Microsoft.EntityFrameworkCore;
using Quiz.ConsoleUI.Common;
using Quiz.ConsoleUI.Models;
using Quiz.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ConsoleUI.Services
{
    public class ResultsService : IResultsService
    {
        public void History()
        {
            throw new NotImplementedException();
        }

        public static bool IsNewBestScore(ApplicationDbContext dbContext,string quizTitle, int userPoints, TimeSpan userTimeSpan)
        {
            BestQuizUser? bestPlayerResults = dbContext.UsersQuizzes
                .Where(q => q.Quiz.Title == quizTitle)
                .Select(q => new BestQuizUser
                {
                    BestPlayerName = q.User.Username,
                    BestPlayerPoints = q.UserPoints,
                    BestPlayerElapsedTime = q.UserElapsedTime
                })
                .OrderByDescending(uq => uq.BestPlayerPoints)
                .ThenByDescending(uq => uq.BestPlayerElapsedTime)
                .ThenBy(uq => uq.BestPlayerName)
                .FirstOrDefault();



            if (bestPlayerResults==null)
            {
                return true;
            }

            if (bestPlayerResults == null||bestPlayerResults.BestPlayerPoints < userPoints ||
                (bestPlayerResults.BestPlayerPoints == userPoints && bestPlayerResults.BestPlayerElapsedTime < userTimeSpan))
            {
                return true;
            }

            return false;

        }


        public static string ShowReport(QuizReportModel report)
        {
            var sb = new StringBuilder();


            sb
                .AppendLine(String.Format(Messages.CurrentQuizResult,
                                                     report.QuizTitle,
                                                     report.UserPoints, 
                                                     report.UserElapsedTime.ToString("mm\\:ss\\.ff")))

                .AppendLine();

            foreach (var question in report.Questions)                  
            {
                sb
                    .AppendLine(question.QuestionTitle)
                    .AppendLine(string.Format(Messages.CorrectAnswerMessage, question.CorrectAnswerTitle));

                if (!String.IsNullOrEmpty(question.UserAnswerTitle))
                {
                    sb.AppendLine(string.Format(Messages.UserAnswerMessage, question.UserAnswerTitle));
                }

                sb.AppendLine();
            }

            return sb.ToString();

        }

        public void ShowResults()
        {
            throw new NotImplementedException();
        }
    }
}
