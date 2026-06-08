using GoonBrowserAndroid.Views;
using System.Windows.Input;

namespace GoonBrowserAndroid.ViewModels
{
    public partial class SettingsViewModel
    {
        public ICommand BackToViewCmd { get; }

        public SettingsViewModel()
        {
            BackToViewCmd = new Command(async () => await BackToView());
        }

        async Task BackToView()
        {
            await Shell.Current.GoToAsync(nameof(WebviewPage));
            // just go to ZA FAKKING BROWSER
        }
    }
}
