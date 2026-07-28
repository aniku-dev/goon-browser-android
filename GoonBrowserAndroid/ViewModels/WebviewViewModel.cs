using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoonBrowserAndroid.Models;
using GoonBrowserAndroid.Services;
using GoonBrowserAndroid.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace GoonBrowserAndroid.ViewModels
{
    public partial class WebviewViewModel : ObservableObject
    {
        // services
        private readonly IDownloadService _downloadService;
        private readonly ILongPressService _longPressService;

        // models
        public ObservableCollection<TabModel> Tabs { get; } = new();
        [ObservableProperty]
        private TabModel selectedTab;

        // let's add some silly tacit ass links cuz y not :p
        private static readonly string[] startUrls =
        {
            "https://www.youtube.com/watch?v=TLselMX7RsM",
            "https://www.onlyfans.com/anna_himejima",
            "https://www.onlyfans.com/mommy-anna",
            "https://www.youtube.com/watch?v=1QWLek3lodI",
            "https://www.youtube.com/watch?v=W3xOTV40GSc",
            "https://www.youtube.com/watch?v=W3xOTV40GSc?t=236",
        };

        // properties
        [ObservableProperty] string urlSource = startUrls[Random.Shared.Next(startUrls.Length)];
        [ObservableProperty] string urlInput;

        // buttons
        public ICommand GoBackCmd { get; }
        public ICommand GoForwardCmd { get; }
        public ICommand RefreshCmd { get;  }
        public ICommand GoToSettingsCmd { get; }
        public ICommand GoToTabsCmd { get; }

        public WebviewViewModel(IDownloadService downloadService, ILongPressService longPressService)
        {
            _downloadService = downloadService;
            _longPressService = longPressService;

            NewTab();

            GoBackCmd = new Command<object>(GoBack);
            GoForwardCmd = new Command<object>(GoForward);
            RefreshCmd = new Command<object>(RefreshPage);
            GoToSettingsCmd = new Command(async () => await GoToSettings());
            GoToTabsCmd = new Command(async () => await GoToTabs());
        }

        [RelayCommand]
        public void GoBack(object WebView)
        {
            if (WebView is WebView browserView)
            {
                browserView.GoBack();
            }
        }

        [RelayCommand]
        public void GoForward(object WebView)
        {
            if (WebView is WebView browserView)
            {
                browserView.GoForward();
            }
        }

        [RelayCommand]
        public void RefreshPage(object WebView)
        {
            if (WebView is WebView browserView)
            {
                browserView.Reload();
            }
        }

        async Task GoToSettings()
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
            // just go to the fucking settings here
        }

        async Task GoToTabs()
        {
            await Shell.Current.GoToAsync(nameof(TabsPage));
            // just go to fucking tab
        }

        [RelayCommand]
        public async Task SearchAsync(Microsoft.Maui.Controls.WebView browserView)
        {
            if (browserView == null)
            {
                return;
            }

            var url = UrlInput?.Trim(); // .Trim trims any remaining newline/chars

            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            /* if a url address doesn't start with either http:// or https://, it basically apends
             * said prefix to the url automatically without the user having to put it in.
             * this is convenient. also, HTTPS superiority!
             */

            if (!url.StartsWith("http://") && !url.StartsWith("https://")) 
            {
                url = "https://" + url;
            }

            browserView.Source = url;

            await Task.CompletedTask;

            UrlInput = url;
        }

        [RelayCommand]
        public async Task DownloadMedia(string url)
        {
            await _downloadService.DownloadAsync(url);
        }

        [RelayCommand]
        public void NewTab()
        {
            var tab = new TabModel();

            Tabs.Add(tab);
            SelectedTab = tab;

           System.Diagnostics.Debug.WriteLine("Tabs amount since added: " + Tabs.Count);
        }

        [RelayCommand]
        public void CloseTab(TabModel tab)
        {
            try
            {
                if (!Tabs.Contains(tab))
                {
                    return;
                }

                Tabs.Remove(tab);

                if (SelectedTab == tab)
                {
                    SelectedTab = Tabs.LastOrDefault();
                }
            }
            catch
            {
                // If i leave the try catch, it leaves blank for 0 tabs, satisfactory solution imo
                // it doesnt reach a toast when i put it here, so just comment
            }
        }
    }
}
