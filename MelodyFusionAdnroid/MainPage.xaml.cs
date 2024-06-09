using MelodyFusionAdnroid.Pages;
using MelodyFusionAdnroid.Models;
using MelodyFusionAdnroid.Service;
using MelodyFusionAdnroid.Infrastructure;
using Newtonsoft.Json;
using MelodyFusionAdnroid.Models.Response;
using MelodyFusionAdnroid.Models.Request;
using CommunityToolkit.Maui.Alerts;

namespace MelodyFusionAdnroid
{
    public partial class MainPage : ContentPage
    {
        private readonly AuthService _authService;
        private readonly RegisterService _registerService;
        private readonly LocalStorage _localStorage;
        private readonly UserService _userService;
        public UserResponse CurrentUser { get; private set; }
        public bool IsAuthenticated { get; private set; }

        public MainPage(AuthService authService, RegisterService registerService, LocalStorage localStorage, UserService userService)
        {
            InitializeComponent();
            _authService = authService;
            _registerService = registerService;
            _localStorage = localStorage;
            _userService = userService;
        }

        public async void AuthClicked(object sender, EventArgs e)
        {
            var email = emailEntry.Text;
            var password = passwordEntry.Text;
            var loginRequest = new LoginRequest
            {
                UserName = email,
                Password = password
            };
            var loginResponse = await _authService.Login(loginRequest);
            if (loginResponse != null)
            {
                _localStorage.AddOrUpdate(LocalStorageKeys.AuthToken, loginResponse.Token);
                var currentUser = await _userService.GetUserInfo();
                _localStorage.AddOrUpdate(LocalStorageKeys.Profile, currentUser);
                CurrentUser = currentUser;
                IsAuthenticated = true;
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");

            }
            else
            {
                var toast = Toast.Make("Login failed,please check your data and try again", CommunityToolkit.Maui.Core.ToastDuration.Long, 24);
                toast.Show();
              
            }
        }

        public async void RegMovClicked(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync($"//{nameof(RegistrationPage)}");
        }
    }

}
