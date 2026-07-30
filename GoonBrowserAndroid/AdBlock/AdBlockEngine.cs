namespace GoonBrowserAndroid.AdBlock
{
    public class AdBlockEngine
    {
        private readonly List<string> _blockedDomains = new();

        public async Task UpdateListsAsync()
        {
            using var client = new HttpClient();

            var easyList = await client.GetStringAsync(
                "https://easylist.to/easylist/easylist.txt"
            );

            foreach (var line in easyList.Split('\n'))
            {
                var rule = line.Trim();

                if (rule.StartsWith("||"))
                {
                    var domain = rule.Substring(2) .Split('^')[0];

                    _blockedDomains.Add(domain);
                }
            }
        }

        public bool ShouldBlock(string url)
        {
            return _blockedDomains.Any(url.Contains);
        }
    }

}
