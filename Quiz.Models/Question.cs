namespace Quiz.Models
{
    public class Question
    {

        public Question()
        {
            this.Answers = new HashSet<Answer>();
            this.UserAnswers = new HashSet<UserAnswer>();
        }

        public int Id { get; set; }


        public string Title { get; set; } = null!;

        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; } = null!;



        public string? Description { get; set; }

        public virtual ICollection<Answer> Answers { get; set; } = null!;

        public virtual ICollection<UserAnswer> UserAnswers { get; set; } = null!;
    }
}