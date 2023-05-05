using Quiz.Data;
using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public QuestionService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }



        public int AddQuestion(string questionTitle, int quizId)
        {
            var question = new Question
            {
                Title = questionTitle
            };

            this.applicationDbContext.Questions.Add(question);

            var quiz = applicationDbContext.Quizzes.FirstOrDefault(q=>q.Id == quizId) ?? throw new ArgumentNullException("No such Quiz");

            quiz.Questions.Add(question);
            this.applicationDbContext.SaveChanges();

            return question.Id;

        }
    }
}
