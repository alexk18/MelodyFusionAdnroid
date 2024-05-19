namespace MelodyFusionAdnroid.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    public async void TestClicked(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new RegistrationPage(_registerService));
        await Shell.Current.GoToAsync(nameof(ProfilePage));
    }



}