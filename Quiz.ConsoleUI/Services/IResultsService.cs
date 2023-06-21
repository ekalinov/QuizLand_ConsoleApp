using Quiz.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ConsoleUI.Services
{
    public interface IResultsService
    {
       bool IsNewBestScore(ApplicationDbContext dbContext, string quizTitle, int userPoints, TimeSpan userTimeSpan);
       
       void ShowResults();

        void History();

        void Results();

    }
}
