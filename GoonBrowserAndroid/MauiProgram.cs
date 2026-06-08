using CommunityToolkit.Maui;
using GoonBrowserAndroid.ViewModels;
using GoonBrowserAndroid.Views;
using Microsoft.Extensions.Logging;

namespace GoonBrowserAndroid
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
                    fonts.AddFont("Ubuntu-Regular.ttf", "UbuntuRegular");
                    fonts.AddFont("Ubuntu-Bold.ttf", "UbuntuBold");
                })
                .UseMauiCommunityToolkit();

            // Views
            builder.Services.AddSingleton<WebviewPage>();
            builder.Services.AddSingleton<SettingsPage>();

            // ViewModels
            builder.Services.AddSingleton<WebviewViewModel>();
            builder.Services.AddSingleton<SettingsViewModel>();

            // Services

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
