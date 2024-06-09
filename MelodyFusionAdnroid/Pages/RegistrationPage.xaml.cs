namespace MelodyFusionAdnroid.Pages;
using MelodyFusionAdnroid.Models;
using MelodyFusionAdnroid.Models.Request;
using MelodyFusionAdnroid.Service;
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
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
        else
        {
            throw new Exception("Registration failed");
        }

    }
    public async void LogMovClicked(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }
}