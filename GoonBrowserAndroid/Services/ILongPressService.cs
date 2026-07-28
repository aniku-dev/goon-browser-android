using GoonBrowserAndroid.Models;
#if ANDROID
using WebView = Android.Webkit.WebView;
#endif

namespace GoonBrowserAndroid.Services
{
    public interface ILongPressService
    {
        void ShowMenu(WebView browserView, HitResultModel hit, IDownloadService downloadService);
    }
}
