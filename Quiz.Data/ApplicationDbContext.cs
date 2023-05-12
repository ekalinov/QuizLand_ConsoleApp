using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Quiz.Models;
using System.Reflection.Emit;

namespace Quiz.Data
{
    public class ApplicationDbContext : DbContext
    
    {

        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }



        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Models.Quiz> Quizzes { get; set; } = null!;
        public DbSet<Answer> Answers { get; set; } = null!;

        public DbSet<UserQuiz> UsersQuizzes { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString!);


            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserQuiz>()
            .HasOne(u => u.User)
            .WithMany(u => u.UserAnswers)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserQuiz>()
        .HasOne(u => u.Quiz)
        .WithMany(u => u.TopUsers)
        .HasForeignKey(u => u.QuizId)
        .OnDelete(DeleteBehavior.NoAction);
        }
    }
}