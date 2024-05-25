namespace MelodyFusionAdnroid.Pages;

public partial class SubPage : ContentPage
{
	public SubPage()
	{
		InitializeComponent();
	}


    public async void BackToProfileClicked(object sender, EventArgs e)
    {
        //Toast.MakeToast("Данные были обновлены").Show(TimeSpan.FromSeconds(2));
        await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
    }

    public async void PayClicked(object sender, EventArgs e)
    {
        //Toast.MakeToast("Данные были обновлены").Show(TimeSpan.FromSeconds(2));
        await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");

        
    }
}