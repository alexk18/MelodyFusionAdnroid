using MelodyFusionAdnroid.Infrastructure;
using MelodyFusionAdnroid.Models;
using MelodyFusionAdnroid.Service;

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
            //Toast.MakeToast("������ ���� ���������").Show(TimeSpan.FromSeconds(2));
            await Shell.Current.GoToAsync(nameof(ProfilePage));
        }
        else
        {
            throw new Exception("Update failed");
        }

    }
}