using MelodyFusionAdnroid.Infrastructure;
using MelodyFusionAdnroid.Models;
using MelodyFusionAdnroid.Service;
using MelodyFusionAdnroid.ViewModels;
using System.IdentityModel.Tokens.Jwt;


namespace MelodyFusionAdnroid.Pages;

public partial class ProfilePage : ContentPage
{
    private readonly LocalStorage _localStorage;
    private readonly UserService _userService;
    public UserResponse CurrentUser { get; private set; }
    public bool IsAuthenticated { get; private set; }
    public ProfilePage(LocalStorage localStorage, UserService userService)
	{
		InitializeComponent();
        _localStorage = localStorage;
        _userService = userService;

    }

    public async void MoveToUpdateClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(UpdateInfoPage));
    }

    public async void MoveToChangeClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(UpdatePasswortPage));
    }

    public async void MoveToAdminClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AdminControlPage));
    }

    public async void DeleteProfileClicked(object sender, EventArgs e)
    {
        var userResponse = await _userService.DeleteProfile();
        CurrentUser = null;
        IsAuthenticated = false;
        _localStorage.Remove(LocalStorageKeys.AuthToken);
        _localStorage.Remove(LocalStorageKeys.Profile);
        if (userResponse != null)
        {
            //Toast.MakeToast("Данные были обновлены").Show(TimeSpan.FromSeconds(2));
            await Shell.Current.GoToAsync(nameof(UpdatePasswortPage));
        }
        else
        {
            throw new Exception("Update failed");
        }
    }

    protected override async void OnAppearing()
    {
        AdminControll.IsVisible = false;
        AdminControll.IsEnabled = false;
        string token = _localStorage[LocalStorageKeys.AuthToken].ToString();
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        if (jwtToken.Claims.Any(c => c.Value == "Admin"))
        {

                AdminControll.IsVisible = true;
                AdminControll.IsEnabled = true;

        }
        base.OnAppearing();
        var currentUser = await _userService.GetUserInfo();
        var userProfileViewModel = new UserProfileViewModel()
        {
            FirstName = currentUser.FirstName,
            LastName = currentUser.LastName,
            Email = currentUser.Email,
            UserName = currentUser.UserName
        };
        BindingContext = userProfileViewModel;
    }




}