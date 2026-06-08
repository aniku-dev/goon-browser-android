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

        private async void OnSearchCompleted(object? sender, EventArgs e)
        {
            if (BindingContext is WebviewViewModel browserView)
                await browserView.SearchAsync(WebView);
        }

        // Pure MVVM attempts to implement this didn't work, but this does, hence why it's
        // implemented like this using code-behind.
        private void BrowserView_Navigated(object sender, WebNavigatedEventArgs navigated)
        {
            if (BindingContext is WebviewViewModel browserView)
                browserView.UrlInput = navigated.Url;
        }
    }
}
