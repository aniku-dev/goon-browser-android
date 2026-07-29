using GoonBrowserAndroid.Models;
using GoonBrowserAndroid.ViewModels;

namespace GoonBrowserAndroid
{
    public partial class WebviewPage : ContentPage
    {
        public WebviewPage(WebviewViewModel webviewViewModel)
        {
            InitializeComponent();
            BindingContext = webviewViewModel;

            SearchBar.Completed += OnSearchCompleted;
            webviewViewModel.TabService.TabChanged += LoadTab;
        }

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
    }
}
