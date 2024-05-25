
using MelodyFusionAdnroid.Infrastructure;
using MelodyFusionAdnroid.Pages;
using MelodyFusionAdnroid.Service;
using Microsoft.Extensions.Logging;
using Microcharts.Maui;

namespace MelodyFusionAdnroid
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMicrocharts()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddHttpClient("MelodyFusion", client =>
            {

                // Paste your server address here
                client.BaseAddress = new Uri("https://192.168.0.192:7293");

            })
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                });

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<RegisterService>();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<LocalStorage>();
            builder.Services.AddSingleton<RegistrationPage>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<ProfilePage>();
            builder.Services.AddSingleton<UpdateInfoPage>();
            builder.Services.AddSingleton<UpdatePasswortPage>();
            builder.Services.AddSingleton<AdminControlPage>();
            builder.Services.AddSingleton<AdminService>();
            builder.Services.AddSingleton<StatisticPage>();
            builder.Services.AddSingleton<StatisticService>();
            builder.Services.AddSingleton<SubPage>();

            return builder.Build();
        }
    }
}
