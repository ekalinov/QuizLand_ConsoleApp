using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Service
{
    public interface IAnswerService
    {
        int AddAnswer(int questionId, string title, bool isCorrect, int points);
    }
}
