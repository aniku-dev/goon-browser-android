using CommunityToolkit.Mvvm.ComponentModel;
using GoonBrowserAndroid.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;

namespace GoonBrowserAndroid.Services.Tab
{
    public partial class TabService : ObservableObject, ITabService
    {
        public ObservableCollection<TabModel> Tabs { get; } = new();
        public event Action<TabModel>? TabChanged;

        [ObservableProperty]
        private TabModel selectedTab;

        public TabModel NewTab()
        {
            var tab = new TabModel();

            Tabs.Add(tab);
            SelectedTab = tab;

            return tab;
        }

        public TabModel? CloseTab(TabModel tab)
        {
            if (!Tabs.Contains(tab))
            {
                return SelectedTab;
            }

            Tabs.Remove(tab);

            if (SelectedTab == tab)
            {
                SelectedTab = Tabs.LastOrDefault();
            }

            if (Tabs.Count == 0)
            {
                Toast.Make("No tabs left, new tab created automatically!");
                return NewTab();
            }

            return SelectedTab;
        }

        public TabModel SwitchTab(TabModel tab)
        {
            SelectedTab = tab;
            TabChanged?.Invoke(tab); // load selected tab url on page when switching

            return SelectedTab;
        }
    }
}
