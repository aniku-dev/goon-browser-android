#if ANDROID
using Android.Webkit;
#endif

namespace GoonBrowserAndroid.AdBlock;
#if ANDROID
public class AdBlockClient : WebViewClient
{
    private readonly AdBlockEngine _engine;

    public AdBlockClient(AdBlockEngine engine)
    {
        _engine = engine;
    }

    public override WebResourceResponse? ShouldInterceptRequest(
        Android.Webkit.WebView? view,
        IWebResourceRequest? request)
    {
        var url = request?.Url?.ToString();

        if (url != null && _engine.ShouldBlock(url))
        {
            return new WebResourceResponse(
                "text/plain",
                "utf-8",
                null
            );
        }

        return base.ShouldInterceptRequest(view, request);
    }
}
#endif
