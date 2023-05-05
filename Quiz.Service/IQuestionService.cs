using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Service
{
    public interface IQuestionService
    {
        int AddQuestion(string question,int quizId);
    }
}
