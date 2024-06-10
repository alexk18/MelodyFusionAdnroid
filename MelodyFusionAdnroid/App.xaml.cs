using MelodyFusion.DLL.Infrastructure;
using MelodyFusion.DLL.Models;
using MelodyFusion.DLL.Service;
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
