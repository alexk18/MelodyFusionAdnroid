using MelodyFusionAdnroid.Infrastructure;
using MelodyFusionAdnroid.Models;
using MelodyFusionAdnroid.Service;
using Microsoft.Maui.ApplicationModel.Communication;

namespace MelodyFusionAdnroid.Pages;

public partial class AdminControlPage : ContentPage
{
    
    private readonly LocalStorage _localStorage;
    private readonly UserService _userService;
    private readonly AdminService _adminService;
    public UserResponse CurrentUser { get; private set; }
    public AdminControlPage(LocalStorage localStorage, UserService userService, AdminService adminService)
	{
		InitializeComponent();
        _localStorage = localStorage;
        _userService = userService;
        _adminService = adminService;

    }

    public async void ShowUserClicked(object sender, EventArgs e)
    {
        var _userAllRequest = new UserAllRequest()
        {
            SearchRequest = "",
            PageFrom = 0,
            PageSize = 5,
        };
        var listUser = await _adminService.GetUserList(_userAllRequest);

        UserShowList.ItemsSource = listUser;
    }

    public async void MakeAdminClicked(object sender, EventArgs e)
    {
        var userId = profileId.Text;
        var _changeRoleRequest = new ChangeRoleRequest
        {
            UserId = userId,
            Role = 200
        };
        var changeRoleResponse = await _adminService.AddAdminRole(_changeRoleRequest);
        if (changeRoleResponse != null)
        {
            await Shell.Current.GoToAsync(nameof(AdminControlPage));
        }
        else
        {
            throw new Exception("Registration failed");
        }
    }
    public async void DeleteAdminClicked(object sender, EventArgs e)
    {
        var userId = profileId.Text;
        var _changeRoleRequest = new ChangeRoleRequest
        {
            UserId = userId,
            Role = 200
        };
        var changeRoleResponse = await _adminService.DeleteAdminRole(_changeRoleRequest);
        if (changeRoleResponse != null)
        {
            await Shell.Current.GoToAsync(nameof(AdminControlPage));
        }
        else
        {
            throw new Exception("Registration failed");
        }
    }

    public async void BanClicked(object sender, EventArgs e)
    {

    }
}