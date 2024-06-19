using CommunityToolkit.Maui.Alerts;
using MelodyFusion.DLL.Infrastructure;
using MelodyFusion.DLL.Models;
using MelodyFusion.DLL.Models.Request;
using MelodyFusion.DLL.Models.Response;
using MelodyFusion.DLL.Service;

namespace MelodyFusionAdnroid.Pages;

public partial class UpdatePasswortPage : ContentPage
{
    private readonly LocalStorage _localStorage;
    private readonly UserService _userService;
    public UserResponse CurrentUser { get; private set; }
    public UpdatePasswortPage(LocalStorage localStorage, UserService userService)
	{
		InitializeComponent();
        _localStorage = localStorage;
        _userService = userService;
    }

    public async void UpdatePasswordClicked(object sender, EventArgs e)
    {
        var oldPass = OldPass.Text;
        var newPass = NewPass.Text;


        var passwordRequest = new ChangePasswordRequest
        {
           OldPassword = oldPass,
           NewPassword = newPass
        };
        var userResponse = await _userService.UpdatePassword(passwordRequest);
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

    public async void BackToProfileClicked(object sender, EventArgs e)
    {
        //Toast.MakeToast("Данные были обновлены").Show(TimeSpan.FromSeconds(2));
        await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
    }
}