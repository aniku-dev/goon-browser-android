using GoonBrowserAndroid.Enums;

namespace GoonBrowserAndroid.Models;

public class HitResultModel
{
    public MediaTypeEnum MediaType { get; set; }

    public string? Url { get; set; }
}