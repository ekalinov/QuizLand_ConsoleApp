using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models
{
    public class Quiz
    {
        public Quiz()
        {
            this.Questions= new HashSet<Question>();
            this.TopUsers = new HashSet<UserQuiz>();
        }

        public int Id { get; set; }


        public string Title  { get; set; } = null!; 


        public string? Description { get; set; } 


        public virtual ICollection<Question> Questions { get; set; } = null!;

        public TimeSpan  QuizElapsedTime { get; set; }

        public virtual ICollection<UserQuiz> TopUsers { get; set; }


    }
}
