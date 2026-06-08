using System;
using System.Collections.Generic;
using System.Text;

namespace GoonBrowserAndroid.Enums
{
    public partial class MediaTypeEnums
    {
        public enum MediaType
        {
            Image,
            Video,
            File
        }


        public record MediaDownloadRequest(
            MediaType Type,
            string Url);
    }
}
