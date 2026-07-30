#if ANDROID
using Android.Content;
using Android.OS;
using Android.Provider;

namespace GoonBrowserAndroid.Services.Download
{
    public partial class DownloadService : IDownloadService
    {
        private readonly HttpClient _httpClient;

        public DownloadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // made the ai slop function if statements
        // conform to the coding conventions better
        public async Task DownloadAsync(string url)
        {
            var fileName = Path.GetFileName(new Uri(url).LocalPath);

            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = Guid.NewGuid().ToString();
            }

            var context = Platform.AppContext;

            var values = new ContentValues();

            values.Put(MediaStore.IMediaColumns.DisplayName, fileName);
            values.Put(MediaStore.IMediaColumns.MimeType, GetMimeType(fileName));

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
            {
                values.Put(MediaStore.IMediaColumns.RelativePath, Android.OS.Environment.DirectoryDownloads);
            }

            // place for where to save the file
            var uri = context.ContentResolver.Insert(
                MediaStore.Downloads.ExternalContentUri,
                values);

            // if uri is null, cry about it
            if (uri == null)
            {
                throw new Exception("Failed to create download");
            }

            // download from internet (so epic and LEGENDARY)
            using var response = await _httpClient.GetAsync(
                url,
                HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            await using var input = await response.Content.ReadAsStreamAsync();

            // open destination file for writing
            await using var output = context.ContentResolver.OpenOutputStream(uri)
            // or else throw exception
            ?? throw new Exception("Unable to open output stream");

            // copy downloaded bytes into the file
            await input.CopyToAsync(output);
        }

        private static string GetMimeType(string fileName)
        {
            var extension = Path.GetExtension(fileName)?.TrimStart('.');

            if (string.IsNullOrEmpty(extension))
            {
                return "application/octet-stream";
            }


            var mime = Android.Webkit.MimeTypeMap.Singleton?.GetMimeTypeFromExtension(extension.ToLowerInvariant());

            return mime ?? "application/octet-stream";
        }
    }
}
#endif