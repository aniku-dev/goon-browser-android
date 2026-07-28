using CommunityToolkit.Mvvm.ComponentModel;

namespace GoonBrowserAndroid.Models
{
    public class TabModel : ObservableObject
    {
        
        private string _url = "https://www.duckduckgo.com";
        private string _title = "Tab";
        private string _favicon;
        private bool _isLoading;

        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Favicon
        {
            get => _favicon;
            set => SetProperty(ref _favicon, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }
    }
}
