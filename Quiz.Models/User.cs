using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models
{
    public class User
    {
        public User()
        {
            this.UserAnswers = new HashSet<UserQuiz>();
        }
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public int UserPoints { get; set; }


        public ICollection<UserQuiz> UserAnswers { get; set; } 

        
    }   
}
