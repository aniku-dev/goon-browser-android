using GoonBrowserAndroid.Views;
using System.Windows.Input;
using GoonBrowserAndroid.Services.Settings;

namespace GoonBrowserAndroid.ViewModels
{
    public partial class SettingsViewModel
    {
        private readonly SettingsService _settings;
        public ICommand BackToViewCmd { get; }

        public SettingsViewModel(SettingsService settings)
        {
            _settings = settings;

            BackToViewCmd = new Command(async () => await BackToView());
        }

        async Task BackToView()
        {
            await Shell.Current.GoToAsync(nameof(WebviewPage));
            // just go to ZA FAKKING BROWSER
        }

        public bool IsAdBlockingEnabled
        {
            get => _settings.IsAdBlockingEnabled;
            set => _settings.IsAdBlockingEnabled = value;
        }
    }
}
