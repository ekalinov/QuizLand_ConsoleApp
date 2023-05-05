using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Quiz.Data;

namespace Quiz.Service
{
    public class QuizService : IQuizService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public QuizService(ApplicationDbContext applicationDbContext)
        {
               this.applicationDbContext= applicationDbContext;
        }


        public void Add(string title)
        {
            var quiz = new Models.Quiz();

            quiz.Title = title;

            this.applicationDbContext.Quizzes.Add(quiz);
            this.applicationDbContext.SaveChanges();
        }
    }
}
