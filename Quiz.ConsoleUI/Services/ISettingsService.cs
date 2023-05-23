using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ConsoleUI.Services
{
    public interface ISettingsService
    {
        void Settings();

        void ChooseTextColor();

        void ChooseBackgroungColor();
    }
}
