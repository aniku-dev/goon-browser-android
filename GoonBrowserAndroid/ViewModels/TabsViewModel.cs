using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoonBrowserAndroid.Models;
using GoonBrowserAndroid.Services.Tab;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GoonBrowserAndroid.ViewModels
{
    public partial class TabsViewModel : ObservableObject
    {
        private readonly ITabService _tabService;
        public ITabService TabService => _tabService;
        public ICommand BackToViewCmd { get; }

        public TabsViewModel(ITabService tabService)
        {
            _tabService = tabService;
            // dedicated tab service so that webview and tabs pages draw from
            // the same collection source of tab models

            BackToViewCmd = new Command(async () => await BackToView());
        }

        async Task BackToView()
        {
            await Shell.Current.GoToAsync(nameof(WebviewPage));
            // just go to ZA FAKKING BROWSER
        }

        [RelayCommand]
        private void SwitchTab(TabModel tab)
        {
            _tabService.SwitchTab(tab);
            BackToView();

            System.Diagnostics.Debug.WriteLine($"Selected: {_tabService.SelectedTab.Title}");
            System.Diagnostics.Debug.WriteLine($"URL: {_tabService.SelectedTab.Url}");
        }
    }
}
