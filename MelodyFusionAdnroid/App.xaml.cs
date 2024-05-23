using MelodyFusionAdnroid.Infrastructure;
using MelodyFusionAdnroid.Service;

namespace MelodyFusionAdnroid
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
