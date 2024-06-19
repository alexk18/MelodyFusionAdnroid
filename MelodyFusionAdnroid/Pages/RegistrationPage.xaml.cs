namespace MelodyFusionAdnroid.Pages;

using CommunityToolkit.Maui.Alerts;
using MelodyFusion.DLL.Models;
using MelodyFusion.DLL.Models.Request;
using MelodyFusion.DLL.Service;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

public partial class RegistrationPage : ContentPage
{

    private readonly RegisterService _registerService;
	public RegistrationPage(RegisterService registerService)
	{
		InitializeComponent();
        _registerService = registerService;
    }

    public async void RegClicked(object sender, EventArgs e)
    {
        var firstName = firstNameEntry.Text;
        var lastName = lastNameEntry.Text;
        var userName = userNameEntry.Text;
        var email = emailEntry.Text;
        var password = passwordEntry.Text;
        var registerRequest = new RegistrationRequest
        {
            Firstname = firstName,
            Lastname = lastName,
            UserName = userName,
            Email = email,
            Password = password,
            Role = 100,
        };
        var registrationResponse = await _registerService.Registr(registerRequest);
        if (registrationResponse != null)
        {
            var toast = Toast.Make("Registration successful", CommunityToolkit.Maui.Core.ToastDuration.Long, 24);
            toast.Show();
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
        else
        {
            var toast = Toast.Make("Registration failed", CommunityToolkit.Maui.Core.ToastDuration.Long, 24);
            toast.Show();
        }

    }
    public async void LogMovClicked(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }
}