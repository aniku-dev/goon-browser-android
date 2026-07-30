using GoonBrowserAndroid.Models;
using GoonBrowserAndroid.Services.Download;
#if ANDROID
using WebView = Android.Webkit.WebView;
#endif

namespace GoonBrowserAndroid.Services.LongPress
{
    public interface ILongPressService
    {
        void ShowMenu(WebView browserView, HitResultModel hit, IDownloadService downloadService);
    }
}
