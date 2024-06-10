using CommunityToolkit.Maui.Alerts;
using MelodyFusion.DLL.Infrastructure;
using MelodyFusion.DLL.Models;
using MelodyFusion.DLL.Models.Request;
using MelodyFusion.DLL.Models.Response;
using MelodyFusion.DLL.Service;
using MelodyFusionAdnroid.ViewModels;
using System.Threading.Tasks;

namespace MelodyFusionAdnroid.Pages;

public partial class UpdateInfoPage : ContentPage
{
    private readonly LocalStorage _localStorage;
    private readonly UserService _userService;
    public UserResponse CurrentUser { get; private set; }
    public bool IsAuthenticated { get; private set; }
    public UpdateInfoPage(LocalStorage localStorage, UserService userService)
	{
		InitializeComponent();
        _localStorage = localStorage;
        _userService = userService;
    }


    public async void UpdateDataClicked(object sender, EventArgs e)
    {
        var firstName = firstNameF.Text;
        var lastName = lastNameT.Text;
        var userName = userNameT.Text; 
        var email = EmailT.Text;

        var userRequest = new UserRequest
        {
            firstName = firstName,
            lastName = lastName,
            userName = userName,
            email = email
        };
        var userResponse = await _userService.UpdateUserInfo(userRequest);
        if (userResponse != null)
        {
            var toast = Toast.Make("Data has been updated", CommunityToolkit.Maui.Core.ToastDuration.Long, 24);
            toast.Show();
            await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
        }
        else
        {
            throw new Exception("Update failed");
        }

    }

    protected override async void OnAppearing()
    {
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

    public async void BackToProfileClicked(object sender, EventArgs e)
    {
            //Toast.MakeToast("Данные были обновлены").Show(TimeSpan.FromSeconds(2));
            await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
    }
}