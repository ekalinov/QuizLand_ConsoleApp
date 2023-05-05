namespace Quiz.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public bool  IsCorrect { get; set; }

        public int Points { get; set; }
    }
}