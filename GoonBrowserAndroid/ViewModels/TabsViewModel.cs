using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoonBrowserAndroid.Models;
using GoonBrowserAndroid.Services.Tab;
using GoonBrowserAndroid.Views;
using System.Windows.Input;

namespace GoonBrowserAndroid.ViewModels
{
    public partial class TabsViewModel : ObservableObject
    {
        private readonly ITabService _tabService;
        public ITabService TabService => _tabService;
        public ICommand GoToSettingsCmd { get; }
        public TabsViewModel(ITabService tabService)
        {
            _tabService = tabService;
            // dedicated tab service so that webview and tabs pages draw from
            _tabService.NewTab();

            GoToSettingsCmd = new Command(async () => await GoToSettings());
        }

        async Task BackToView()
        {
            await Shell.Current.GoToAsync(nameof(WebviewPage));
            // just go to ZA FAKKING BROWSER
        }

        [RelayCommand]
        private void NewTab()
        {
            _tabService.NewTab();
        }

        [RelayCommand]
        private async Task CloseTab(TabModel tab)
        {
            bool deleteTabConfirmed = await Shell.Current.DisplayAlertAsync(
            "Delete Tab",
            "Are you sure you want to delete this tab?",
            "Confirm",
            "Cancel");

            if (!deleteTabConfirmed)
            {
                return;
            }

            _tabService.CloseTab(tab);
        }

        [RelayCommand]
        private void SwitchTab(TabModel tab)
        {
            _tabService.SwitchTab(tab);
            BackToView();
        }

        async Task GoToSettings()
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
            // just go to the fucking settings here
        }
    }
}
