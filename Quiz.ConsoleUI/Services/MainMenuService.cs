using Microsoft.Extensions.Options;
using Quiz.ConsoleUI.Common;
using Quiz.ConsoleUI.Models;

using Quiz.Data;
using System.Text;

namespace Quiz.ConsoleUI.Services
{
    public class MainMenuService : IMainMenuService
    {

        public MainMenuService()
        {
        }

        public void RunInteractiveMenu()
        {
            var settingsSerive = new SettingsService();
            var startEndQuiz = new StartEndQuizService();
            var resulService = new ResultsService();
            Console.Clear();
            // Create options that you want your menu to have
            var options = new List<Option>
            {
                new Option(Messages.ChooseQuiz, () => startEndQuiz.ChooseQuiz()),
                new Option(Messages.RandomQuiz, () => startEndQuiz.ConfirmStartRandomQuiz()),
                new Option(Messages.Results, () =>  resulService.Results()),
                new Option(Messages.Settings, () =>  settingsSerive.Settings()),

                new Option(Messages.Exit, () => Environment.Exit(0)),
            };

            Utilities.ChooseOption(options, Messages.MainMenu);
        }













    }
}