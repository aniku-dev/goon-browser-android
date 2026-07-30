using CommunityToolkit.Maui;
using GoonBrowserAndroid.Services.LongPress;
using GoonBrowserAndroid.Services.Download;
using GoonBrowserAndroid.Services.Settings;
using GoonBrowserAndroid.Services.Tab;
using GoonBrowserAndroid.ViewModels;
using GoonBrowserAndroid.Views;
using Microsoft.Extensions.Logging;
using GoonBrowserAndroid.AdBlock;

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
            builder.Services.AddSingleton<SettingsService>();

#if ANDROID // Android specific stuff here
            builder.Services.AddHttpClient<IDownloadService, DownloadService>();
            builder.Services.AddSingleton<ILongPressService, LongPressService>();
            builder.Services.AddSingleton<AdBlockClient>();
            builder.Services.AddSingleton<AdBlockEngine>();
#endif

            // debugging block
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
