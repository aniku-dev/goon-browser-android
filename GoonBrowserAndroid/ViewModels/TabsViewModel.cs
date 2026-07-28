using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoonBrowserAndroid.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace GoonBrowserAndroid.ViewModels
{
    public partial class TabsViewModel : ObservableObject
    {
        public ObservableCollection<TabModel> Tabs { get; } = new();
        [ObservableProperty]
        private TabModel selectedTab;
        public ICommand BackToViewCmd { get; }

        public TabsViewModel()
        {
            BackToViewCmd = new Command(async () => await BackToView());
        }

        async Task BackToView()
        {
            await Shell.Current.GoToAsync(nameof(WebviewPage));
            // just go to ZA FAKKING BROWSER
        }

        [RelayCommand]
        public void SwitchTab(TabModel tab)
        {
            SelectedTab = tab;
        }
    }
}
