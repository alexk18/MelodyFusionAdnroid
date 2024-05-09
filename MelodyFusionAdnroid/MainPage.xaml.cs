using MelodyFusionAdnroid.Pages;
using MelodyFusionAdnroid.Models;
using MelodyFusionAdnroid.Service;

namespace MelodyFusionAdnroid
{
    public partial class MainPage : ContentPage
    {
        private readonly AuthService _authService;
        private readonly RegisterService _registerService;

        public MainPage( AuthService authService, RegisterService registerService)
        {
            InitializeComponent();
            _authService = authService;
            _registerService = registerService;
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
                throw new Exception("Login Succeed");
            }
            else
            {
                throw new Exception("Login failed");
            }
        }

        public async void RegMovClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new RegistrationPage(_registerService));
            await Shell.Current.GoToAsync(nameof(RegistrationPage));
        }
    }

}
