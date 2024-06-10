using MelodyFusion.DLL.Infrastructure;
using MelodyFusion.DLL.Models;
using MelodyFusion.DLL.Models.Request;
using MelodyFusion.DLL.Models.Response;
using MelodyFusion.DLL.Service;
using MelodyFusionAdnroid.ViewModels;
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
            PageSize = 100,
        };
        var listUser = await _adminService.GetUserList(_userAllRequest);

        UserShowList.ItemsSource = listUser;
    }

    public async void MakeAdminClicked(object sender, EventArgs e)
    {
        
        var button = sender as Button;
        var user = button?.BindingContext as UserResponse; // Получаем объект пользователя, связанный с кнопкой
        var userId = user.Id.ToString(); // Заполняем блок Entry айдишником пользователя
        var _changeRoleRequest = new ChangeRoleRequest
        {
            UserId = userId,
            Role = 200
        };
        var changeRoleResponse = await _adminService.AddAdminRole(_changeRoleRequest);
        if (changeRoleResponse != null)
        {
            //await Shell.Current.GoToAsync(nameof(AdminControlPage));
        }
        else
        {
            throw new Exception("Registration failed");
        }
    }
    public async void DeleteAdminClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var user = button?.BindingContext as UserResponse; // Получаем объект пользователя, связанный с кнопкой
        var userId = user.Id.ToString(); // Заполняем блок Entry айдишником пользователя
        var _changeRoleRequest = new ChangeRoleRequest
        {
            UserId = userId,
            Role = 200
        };
        var changeRoleResponse = await _adminService.DeleteAdminRole(_changeRoleRequest);
        if (changeRoleResponse != null)
        {
            //await Shell.Current.GoToAsync(nameof(AdminControlPage));
        }
        else
        {
            throw new Exception("Registration failed");
        }
    }

    public async void BanClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var user = button?.BindingContext as UserResponse; // Получаем объект пользователя, связанный с кнопкой
        var userId = user.Id.ToString(); // Заполняем блок Entry айдишником пользователя
        var userResponse = await _adminService.BanUser(userId);
        if (userResponse != null)
        {
            //await Shell.Current.GoToAsync(nameof(AdminControlPage));
        }
        else
        {
            throw new Exception("Registration failed");
        }
    }

    // Функционал который уже не используется но удалять жалко , оставлю тут как в память о моём техническом прогрессе при разработке приложений MAUI

    //private void SelectUserClicked(object sender, EventArgs e)
    //{
    //    var button = sender as Button;
    //    var user = button?.BindingContext as UserResponse; // Получаем объект пользователя, связанный с кнопкой
    //    if (user != null)
    //    {
    //        profileId.Text = user.Id.ToString(); // Заполняем блок Entry айдишником пользователя
    //    }
    //}

    public async void BackToProfileClicked(object sender, EventArgs e)
    {
        //Toast.MakeToast("Данные были обновлены").Show(TimeSpan.FromSeconds(2));
        await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
    }

    public async void GoToStatisticClicked(object sender, EventArgs e)
    {
        //Toast.MakeToast("Данные были обновлены").Show(TimeSpan.FromSeconds(2));
        await Shell.Current.GoToAsync($"//{nameof(StatisticPage)}");
    }
}