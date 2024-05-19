using MelodyFusionAdnroid.Pages;

namespace MelodyFusionAdnroid
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));

            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));

            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));

            Routing.RegisterRoute(nameof(UpdateInfoPage), typeof(UpdateInfoPage));
            
            Routing.RegisterRoute(nameof(UpdatePasswortPage), typeof(UpdatePasswortPage));

            Routing.RegisterRoute(nameof(AdminControlPage), typeof(AdminControlPage));
        }
    }
}
