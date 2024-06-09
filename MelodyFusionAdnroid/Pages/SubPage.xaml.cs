using MelodyFusionAdnroid.Service;

namespace MelodyFusionAdnroid.Pages;

public partial class SubPage : ContentPage
{
    private string CLIENT_AUTHORIZATION = "sandbox_d5n9sdp7_htxc964mtfngf5g4";
    private readonly PaymentService _paymentService;


    public SubPage(PaymentService paymentService)
	{
		InitializeComponent();
        _paymentService = paymentService;
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