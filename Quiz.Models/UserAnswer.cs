using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;

        public IdentityUser User { get; set; } = null!;


        public int? AnswerId { get; set; }

        public Answer? Answer { get; set; }


        public int QuestionId { get; set; }

        public Question Question { get; set; } = null!;

    }
}
