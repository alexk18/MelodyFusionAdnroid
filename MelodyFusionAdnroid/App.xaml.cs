using MelodyFusionAdnroid.Infrastructure;
using MelodyFusionAdnroid.Models;
using MelodyFusionAdnroid.Service;
using System.Globalization;
using MelodyFusionAdnroid.TranslatorLan;

namespace MelodyFusionAdnroid
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //Translator.Instance.CultureInfo = new CultureInfo("uk-UA");

            MainPage = new AppShell();
        }

    }
}
