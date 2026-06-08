using System;
using System.Collections.Generic;
using System.Text;

namespace GoonBrowserAndroid.Services.Download
{
    public partial class DownloadService : IDownloadService
    {
        private readonly HttpClient _httpClient;

        public DownloadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DownloadAsync(string url)
        {
            var bytes = await _httpClient.GetByteArrayAsync(url);

            // save via MediaStore, Downloads, etc.
        }
    }
}
