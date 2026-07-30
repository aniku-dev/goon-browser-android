using GoonBrowserAndroid.Models;
using GoonBrowserAndroid.ViewModels;
using GoonBrowserAndroid.Services.Settings;
using GoonBrowserAndroid.AdBlock;



namespace GoonBrowserAndroid.Views;

public partial class WebviewPage : ContentPage
{
    private readonly SettingsService _settings;
    private readonly AdBlockEngine _engine;

    public WebviewPage(WebviewViewModel webviewViewModel, SettingsService settings, AdBlockEngine engine)
    {
        InitializeComponent();
        BindingContext = webviewViewModel;
        _settings = settings;
        _engine = engine;

        SearchBar.Completed += OnSearchCompleted;
        webviewViewModel.TabService.TabChanged += LoadTab;
        LoadAdBlock();

#if ANDROID
        if (BrowserView.Handler?.PlatformView is Android.Webkit.WebView nativeWebView)
        {
            if (_settings.IsAdBlockingEnabled)
            {
                nativeWebView.SetWebViewClient(
                    new AdBlockClient(_engine)
                );
            }
        }
#endif
    }

    // When the back button is pressed in the Android navbar,
    // the browser goes back to the previous page in the WebView instead of the app itself.
    // That is the intended purpose and this method works.
    protected override bool OnBackButtonPressed()
    {
        if (BindingContext is WebviewViewModel browserView)
        {
            browserView.GoBack(BrowserView);
        }

        return true;
    }

    // Object sender and event arguments here because it wants url to do search with.
    // If it doesn't have a URL to send to the search method, the code refuses to run
    // and gives error accordingly.
    private async void OnSearchCompleted(object? sender, EventArgs e)
    {
        if (BindingContext is WebviewViewModel browserView)
        {
            await browserView.SearchAsync(BrowserView);
        }
    }

    // Pure MVVM attempts to implement this didn't work, but this does, hence why it's
    // implemented like this using code-behind.
    // Also doesn't update when simply navigating from page to page, this isn't a
    // critical bug so will be fixed later
    private async void BrowserView_Navigated(object sender, WebNavigatedEventArgs navigated)
    {
        if (BindingContext is WebviewViewModel browserView)
        {
            browserView.UrlInput = navigated.Url;
            browserView.TabService.SelectedTab.Url = navigated.Url;
            // set the title of the tab to the correct parametre instead of "Tab" every
            // ffffffFFFFUCKING TIME
            if (sender is WebView webView)
            {
                try
                {
                    var title = await webView.EvaluateJavaScriptAsync("document.title");
                    browserView.TabService.SelectedTab.Title = title.Trim('"');
                }
                catch
                {
                    // can be empty here, because if try fails then it does nothing
                }
            }
        }
    }

    // loads the selected tab's url when switching, or else tab gets overwritten
    // by the previous tab and gets malformed into being the same url & page
    private void LoadTab(TabModel tab)
    {
        if (!string.IsNullOrWhiteSpace(tab.Url))
        {
            BrowserView.Source = tab.Url;
        }
    }

#if ANDROID
    private void BrowserView_HandlerChanged(object? sender, EventArgs e)
    {
        if (BrowserView.Handler?.PlatformView is Android.Webkit.WebView nativeWebView)
        {
            nativeWebView.Settings.JavaScriptEnabled = true;

            nativeWebView.SetWebViewClient(
                new AdBlockClient(_engine)
            );
        }
    }
#endif

    private async void LoadAdBlock()
    {
        await _engine.UpdateListsAsync();
    }
}
