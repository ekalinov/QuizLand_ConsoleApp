using Quiz.Data;
using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Service
{
    public class AnswerService : IAnswerService
    {
        private readonly ApplicationDbContext db;

        public AnswerService(ApplicationDbContext db)
        {
            this.db = db;
        }


        public int AddAnswer(int questionId, string title, bool isCorrect,int points)
        {
            var question = db.Questions.FirstOrDefault(q => q.Id == questionId) 
                                        ?? throw new ArgumentNullException("No such Question");


            var answer = new Answer()
            {
                Title = title,
                IsCorrect = isCorrect,
                Points = points
            };
                 
            question.Answers.Add(answer);

            db.SaveChanges();   

            return answer.Id;

        }
    }
}
