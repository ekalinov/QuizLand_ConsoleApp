using Microsoft.Extensions.Options;
using Quiz.ConsoleUI.Common;
using Quiz.ConsoleUI.Models;

using Quiz.Data;
using System.Text;

namespace Quiz.ConsoleUI.Services
{
    public class MainMenuService : IMainMenuService
    {
        private readonly ISettingsService settingsService;
        private readonly IStartEndQuizService startEndQuizService;
        private readonly IResultsService resultsService;


        public MainMenuService(
            ISettingsService _settingsService,
            IStartEndQuizService _startEndQuizService,
            IResultsService _resultsService)
        {
            this.settingsService = _settingsService;
            this.startEndQuizService = _startEndQuizService;
            this.resultsService = _resultsService;
        }

        public void RunInteractiveMenu()
        {
            
            Console.Clear();
            // Create options that you want your menu to have
            var options = new List<Option>
            {
                new Option(Messages.ChooseQuiz, () => startEndQuizService.ChooseQuiz()),
                new Option(Messages.RandomQuiz, () => startEndQuizService.ConfirmStartRandomQuiz()),
                new Option(Messages.Results, () =>  resultsService.Results()),
                new Option(Messages.Settings, () =>  settingsService.Settings()),

                new Option(Messages.Exit, () => Environment.Exit(0)),
            };

            Utilities.ChooseOption(options, Messages.MainMenu);
        }













    }
}