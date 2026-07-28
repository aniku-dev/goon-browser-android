using GoonBrowserAndroid.Enums;
using GoonBrowserAndroid.Models;
using GoonBrowserAndroid.Services;
using Android.Widget;
using WebView = Android.Webkit.WebView;
// make sure WebView isn't ambiguous becuz 2 references use same name??? WHY MICROSLOP FUCK U
// so just equal the webview to that full reference, or else C# cries like fucking bitch ce pula mea(wtf)

public class LongPressService : ILongPressService
{
    /// <summary>
    /// Converts Android HitTestResult into something the app understands.
    /// </summary>
    public void ShowMenu(WebView browserView, HitResultModel hit, IDownloadService downloadService)
    {
        System.Diagnostics.Debug.WriteLine(
            $"Long press: {hit.MediaType} - {hit.Url}");

        var popup = new PopupMenu(browserView.Context, browserView);

        popup.MenuItemClick += async (_, args) =>
        {
            switch (args.Item.TitleFormatted?.ToString())
            {
                case "Download":
                    if (!string.IsNullOrWhiteSpace(hit.Url))
                    {
                        await downloadService.DownloadAsync(hit.Url);
                    }
                    break;

                case "Copy Link":

                    // TODO

                    break;
            }
        };

        popup.Show();
    }
    public static HitResultModel Parse(WebView.HitTestResult hit)
    {   
        return ((HitTestTypeEnum)hit.Type) switch
        {

            HitTestTypeEnum.Image => new()
            {
                MediaType = MediaTypeEnum.Image,
                Url = hit.Extra
            },

            HitTestTypeEnum.SrcImageAnchor => new()
            {
                MediaType = MediaTypeEnum.Image,
                Url = hit.Extra
            },

            HitTestTypeEnum.SrcAnchor => new()
            {
                MediaType = MediaTypeEnum.File,
                Url = hit.Extra
            },

            _ => new()
            {
                MediaType = MediaTypeEnum.Unknown,
                Url = null
            }
        };
    }
}