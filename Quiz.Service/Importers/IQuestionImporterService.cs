using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Service.Importers
{
    public interface IQuestionImporterService
    {

        string ImportQuestionsFromTextFile( string filePath);

    }
}
