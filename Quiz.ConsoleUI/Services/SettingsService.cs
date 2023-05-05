using Quiz.ConsoleUI.Common;

namespace Quiz.ConsoleUI.Services
{
    public class SettingsService: ISettingsService
    {

        public SettingsService()
        {
        }


        public void ChooseBackgroungColor()
        {
            var mainMenu = new MainMenuService();

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

            options.Add(new Option(Messages.BackToSettingsMenu, () => mainMenu.Settings()));


            Utilities.ChooseOption(options, Messages.SetBackgroundColor);

        }

        public  void ChooseTextColor()
        {
            var mainMenu = new MainMenuService();


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

            options.Add(new Option(Messages.BackToSettingsMenu, () => mainMenu.Settings()));


            Utilities.ChooseOption(options, Messages.SetBackgroundColor);

        }


    }
}
