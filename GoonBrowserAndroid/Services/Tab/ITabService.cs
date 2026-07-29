using GoonBrowserAndroid.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GoonBrowserAndroid.Services.Tab
{
    public interface ITabService
    {
        ObservableCollection<TabModel> Tabs { get; }
        TabModel? SelectedTab { get; set; }
        event Action<TabModel>? TabChanged;
        TabModel NewTab();
        TabModel CloseTab(TabModel tab);
        TabModel SwitchTab(TabModel tab);
    }
}
