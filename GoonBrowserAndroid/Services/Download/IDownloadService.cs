using System;
using System.Collections.Generic;
using System.Text;

namespace GoonBrowserAndroid.Services.Download
{
    public interface IDownloadService
    {
        Task DownloadAsync(string url);
    }
}
