using Microsoft.Extensions.Options;
using Quiz.ConsoleUI.Common;
using System.Text;

namespace Quiz.ConsoleUI.Services
{
    public class SettingsService: ISettingsService
    {
        private MainMenuService mainMenu;
        private List<Option> options;
        public SettingsService()
        {
            this.mainMenu = new MainMenuService();
            this.options = new List<Option>();
        }

        public void Settings()
        {
            var sb = new StringBuilder();

            sb
           .AppendLine(Messages.Settings)
                .AppendLine("");


            // Create options that you want your menu to have
            options = new List<Option>
            {
                new Option(Messages.SetBackgroundColor, () => ChooseBackgroungColor()),
                new Option(Messages.SetTextColor, () => ChooseTextColor()),
                new Option(Messages.BackToMainMenuMessage, () => mainMenu.RunInteractiveMenu()),
            };

            Utilities.ChooseOption(options, sb.ToString());


        }

        public void ChooseBackgroungColor()
        {

            string[] colors = new string[]{"Black",
                                           "DarkBlue",
                                           "DarkGreen",
                                           "DarkMagenta",
                                           "DarkYellow ",
                                           "Gray",
                                           "Green",
                                           "White" };


            // Create options that you want your menu to have
            
            var options = new List<Option>();
            foreach (var color in colors)
            {
                options.Add(new Option(color, () => ConsoleConstants.SetBackgroundColor(color)));
            };

            options.Add(new Option(Messages.BackToSettingsMenu, () => Settings()));


            Utilities.ChooseOption(options, Messages.SetBackgroundColor);

        }

        public  void ChooseTextColor()
        {

            string[] colors = new string[]{"Black",
                                           "DarkBlue",
                                           "DarkGreen",
                                           "DarkMagenta",
                                           "DarkYellow ",
                                           "Gray",
                                           "Green",
                                           "White" };

            // Create options that you want your menu to have

            var options = new List<Option>();
            foreach (var color in colors)
            {
                options.Add(new Option(color, () => ConsoleConstants.SetTextColor(color)));
            };

            options.Add(new Option(Messages.BackToSettingsMenu, () => Settings()));


            Utilities.ChooseOption(options, Messages.SetBackgroundColor);

        }


    }
}
