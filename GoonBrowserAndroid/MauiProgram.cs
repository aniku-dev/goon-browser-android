using CommunityToolkit.Maui;
using GoonBrowserAndroid.Services;
using GoonBrowserAndroid.Services.Download;
using GoonBrowserAndroid.Services.Tab;
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
            builder.Services.AddSingleton<TabsPage>();

            // ViewModels
            builder.Services.AddSingleton<WebviewViewModel>();
            builder.Services.AddSingleton<SettingsViewModel>();
            builder.Services.AddSingleton<TabsViewModel>();

            // Services
            builder.Services.AddSingleton<ITabService, TabService>();

            // Android specific stuff here
#if ANDROID
            builder.Services.AddHttpClient<IDownloadService, DownloadService>();
            builder.Services.AddSingleton<ILongPressService, LongPressService>();
#endif

// debugging block
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
