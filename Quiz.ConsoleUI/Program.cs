﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quiz.ConsoleUI.Services;
using Quiz.Data;
using Quiz.Service.Importers;

namespace Quiz.ConsoleUI
{
    public class Program
    {
        
        static void Main()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            //var dbContext = serviceProvider.GetService<ApplicationDbContext>();

            //dbContext!.Database.EnsureDeleted();
            //dbContext.Database.EnsureCreated();

            ////DB seeding

            //var questionImporrterService = serviceProvider.GetService<IQuestionImporterService>();
            //questionImporrterService!.ImportQuestionsFromTextFile("QuizQuestionsJSON.txt");


            var mainMenuService = serviceProvider.GetService<IMainMenuService>();

             mainMenuService!.RunInteractiveMenu();

        }


        public static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(connectionString));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                 .AddEntityFrameworkStores<ApplicationDbContext>();


           services.AddSingleton<IMainMenuService,MainMenuService>();
           services.AddSingleton<IQuestionImporterService, QuestionImporterService>();
            services.AddSingleton<IStartEndQuizService, StartEndQuizService>();
            services.AddSingleton<ISettingsService, SettingsService>();

        }
    }
}