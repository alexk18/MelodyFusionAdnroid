using MelodyFusionAdnroid.Infrastructure;
using MelodyFusionAdnroid.Service;

namespace MelodyFusionAdnroid
{
    public partial class App : Application
    {
        //private readonly AuthService _authService;
        //private readonly RegisterService _registerService;
        //private readonly LocalStorage _localStorage;
        //private readonly UserService _userService;
        //AuthService authService, RegisterService registerService, LocalStorage localStorage, UserService userService
        public App()
        {
            InitializeComponent();

            //_authService = authService;
            //_registerService = registerService;
            //_localStorage = localStorage;
            //_userService = userService;

            MainPage = new AppShell();
            //MainPage = new NavigationPage(new MainPage(_authService, _registerService, _localStorage, _userService));
        }
    }
}
