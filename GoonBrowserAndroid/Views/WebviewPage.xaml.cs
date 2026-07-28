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
        }

        // this fucking func here in codebehind bcuz WAAAA I DONT WANNA WORK IN FUCKIGN VIRW MODEL!!!!
        protected override bool OnBackButtonPressed()
        {
            if (WebView != null && WebView.CanGoBack) // "WebView" is x:Name name of the element in the bitch ass xaml page
            {
                WebView.GoBack();
                return true; // handled: do not pop the page
            }
            return base.OnBackButtonPressed();
        }

        private async void OnSearchCompleted(object? sender, EventArgs e)
        {
            if (BindingContext is WebviewViewModel browserView)
            {
                await browserView.SearchAsync(WebView);
            }
        }

        // Pure MVVM attempts to implement this didn't work, but this does, hence why it's
        // implemented like this using code-behind.
        // Also doesn't update when simply navigating from page to page, this isn't a
        // critical bug so will be fixed later
        private void BrowserView_Navigated(object sender, WebNavigatedEventArgs navigated)
        {
            if (BindingContext is WebviewViewModel browserView)
            {
                browserView.UrlInput = navigated.Url;
                browserView.SelectedTab.Url = navigated.Url;
            }
        }
    }
}
