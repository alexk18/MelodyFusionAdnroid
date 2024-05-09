
using MelodyFusionAdnroid.Pages;
using MelodyFusionAdnroid.Service;
using Microsoft.Extensions.Logging;

namespace MelodyFusionAdnroid
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //            builder.Services.AddHttpClient("MelodyFusion", client => {

            //                client.BaseAddress = new Uri(EnviromentSettings.BaseUrl);

            //            });
            builder.Services.AddHttpClient("MelodyFusion", client =>
            {

                client.BaseAddress = new Uri("http://192.168.0.192:5045");

            })
                 .ConfigurePrimaryHttpMessageHandler(() => {
                     var handler = new HttpClientHandler();

                     handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                     return handler;
                 })
                 ;
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<RegisterService>();
            builder.Services.AddSingleton<RegistrationPage>();


            return builder.Build();
        }
    }
}
