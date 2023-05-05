using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quiz.Data;
using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Quiz.Service.Importers
{
    public class QuestionImporterService : IQuestionImporterService
    {
        private readonly ApplicationDbContext db;

        public QuestionImporterService(ApplicationDbContext db)
        {

            this.db = db;
        }

        //TODO:
        public string ImportQuestionsFromTextFile(string filePath)
        {

            string quizPattern = @"<Quiz>((.|\n)*?)<Quiz\/>"; //
            string quizTitlePattern = @"<QuizTitle>(?<QuizTitle>(.|\n)*?)<QuizTitle\/>";
            string quizDescriptionPattern = @"<Description>(?<Description>(.|\n)*?)<Description\/>";
            string quizQuestionPatterm = @"<Questions>(?<Questions>(.|\n)*?)<Questions\/>";


            string paragraphPattern = @"[^\r\n]+((\r|\n|\r\n)[^\r\n]+)*";
            string questionPattern = @"((?<QuestionNumber>[0-9]{1,2}\.)(?<Question>.{1,}))";
            string answerPattern = @"((?<AnswerIndex>[ABCD]{1})\.(?<Answer>.{1,}))";
            string additionInfoPattern = @"^(?![0-9]|[ABCD]|Answer:).+";
            string corectAnswerPattern = @"(Answer:) {0,}(?<CorrectAnswerIndex>[ABCD])";

            var quizzes = Regex.Matches(File.ReadAllText(filePath), quizPattern);

            foreach (Match quizFromFile in quizzes)
            {
               var quiz = new Models.Quiz();

                var quizTitle = Regex.Match(quizFromFile.ToString(), quizTitlePattern)
                                          .Groups["QuizTitle"]
                                          .Value;

                var quizDescription = Regex.Match(quizFromFile.ToString(), quizDescriptionPattern)
                                          .Groups["Description"]
                                          .Value;

                quiz.Title = quizTitle;
                quiz.Description = quizDescription;


                var questionsBlock = Regex.Match(quizFromFile.ToString(), quizQuestionPatterm)
                                            .Groups["Questions"]
                                            .Value;

                var questions = Regex.Matches(questionsBlock.ToString(), paragraphPattern);



                foreach (var questionMatch in questions)
                {


                    string questionTitle = Regex.Match(questionMatch.ToString()!, questionPattern)
                                            .Groups["Question"]
                                            .Value;


                    var additionalInfoParam = Regex.Match(questionMatch.ToString()!, additionInfoPattern, RegexOptions.Multiline);

                    string? additionalInfo = null;

                    if (additionalInfoParam.Success)
                    {
                        additionalInfo = additionalInfoParam.ToString();
                    }




                    char correctAnswerIndex = char.Parse(Regex.Match(questionMatch.ToString()!, corectAnswerPattern)
                                                 .Groups["CorrectAnswerIndex"]
                                                 .Value
                                                 .Trim());


                    var question = new Question()
                    {
                        Title = questionTitle,
                        Description = additionalInfo,
                        Quiz = quiz,

                    };


                    var answerArgs = Regex.Matches(questionMatch.ToString()!, answerPattern);
                    foreach (Match answerRegex in answerArgs)
                    {
                        var answer = new Answer
                        {
                            Title = answerRegex.Groups["Answer"]
                                                  .Value
                                                  .Trim()
                        };

                        char answerIndex = char.Parse(answerRegex.Groups["AnswerIndex"]
                                                  .Value
                                                  .Trim());


                        if (answerIndex == correctAnswerIndex)
                        {
                            answer.IsCorrect = true;
                            answer.Points = 1;

                        }
                        else
                        {
                            answer.IsCorrect = false;
                            answer.Points = 0;

                        }

                        question.Answers.Add(answer);
                    }



                    db.Questions.Add(question);

                }

            }

            db.SaveChanges();

            return $"{db.Questions.Count()} Questions are added";


        }
    }
}
