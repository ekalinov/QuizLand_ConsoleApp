using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ConsoleUI.Models
{
    public class QuizReportModel
    {

        public QuizReportModel()
        {
            this.Questions = new HashSet<QuestionReportModel>();
        }


        public string QuizTitle { get; set; } = null!; 

        

        public ICollection<QuestionReportModel> Questions { get; set; } = null!;

        public int  UserPoints { get; set; }

        public TimeSpan UserElapsedTime { get; set; }


    }
}
