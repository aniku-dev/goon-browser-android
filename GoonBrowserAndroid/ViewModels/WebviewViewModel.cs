using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoonBrowserAndroid.Services.Download;
using GoonBrowserAndroid.Views;
using System.Windows.Input;

namespace GoonBrowserAndroid.ViewModels
{
    public partial class WebviewViewModel : ObservableObject
    {
        // services
        //private readonly IDownloadService _downloadService;

        // let's add some philosophical and gooner ass links cuz y not :p
        private static readonly string[] startUrls =
        {
            "https://www.youtube.com/watch?v=TLselMX7RsM",
            "https://www.onlyfans.com/anna_himejima",
            "https://www.onlyfans.com/mommy-anna",
            "https://youtu.be/1QWLek3lodI?si=6ZmuWvDP0w14Fxqb",
        };

        // properties
        [ObservableProperty] string urlSource = startUrls[Random.Shared.Next(startUrls.Length)];
        [ObservableProperty] string urlInput;

        // buttons
        public ICommand GoBackCmd { get; }
        public ICommand GoForwardCmd { get; }
        public ICommand GoToSettingsCmd { get; }

        public WebviewViewModel(/*IDownloadService downloadService*/)
        {
            //_downloadService = downloadService;

            GoBackCmd = new Command<object>(GoBack);
            GoForwardCmd = new Command<object>(GoForward);
            GoToSettingsCmd = new Command(async () => await GoToSettings());
        }

        [RelayCommand]
        void GoBack(object WebView)
        { 
            if (WebView is WebView browserView) 
                browserView.GoBack(); 
        }

        [RelayCommand]
        void GoForward(object WebView) 
        { 
            if (WebView is WebView browserView) 
                browserView.GoForward(); 
        }

        async Task GoToSettings()
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
            // just go to the fucking settings here
        }

        [RelayCommand]
        public async Task SearchAsync(Microsoft.Maui.Controls.WebView browserView)
        {
            if (browserView == null) 
                return;

            var url = UrlInput?.Trim(); // .Trim trims any remaining newline/chars

            if (string.IsNullOrEmpty(url)) 
                return;

            /* if a url address doesn't start with either http:// or https://, it basically apends
             * said prefix to the url automatically without the user having to put it in.
             * this is convenient. also, HTTPS superiority!
             */

            if (!url.StartsWith("http://") && !url.StartsWith("https://")) 
                url = "https://" + url;

            browserView.Source = url;

            await Task.CompletedTask;

            UrlInput = url;
        }

        /*
        [RelayCommand]
        public async Task DownloadMedia(string url)
        {
            await _downloadService.DownloadAsync(url);
        }
        */
    }
}
