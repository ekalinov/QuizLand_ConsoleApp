using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ConsoleUI.Models
{
    public class BestQuizUser
    {
        public int BestPlayerPoints { get; set; }

        public string BestPlayerName { get; set; } = null!;

        public TimeSpan BestPlayerElapsedTime { get; set; }


    }
}
