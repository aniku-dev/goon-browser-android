using System;
using System.Collections.Generic;
using System.Text;

namespace GoonBrowserAndroid.Services
{
    public interface IDownloadService
    {
        Task DownloadAsync(string url);
    }
}
