using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models
{
    public class UserQuiz
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } = null!;


        public int QuizId { get; set; } 

        public Quiz Quiz { get; set; } = null!;


        public int UserPoints { get; set; }

        public TimeSpan UserElapsedTime { get; set; }

    }
}
    