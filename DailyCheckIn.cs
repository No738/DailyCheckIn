using Serilog;

namespace GenshinCheckIn
{
    internal class DailyCheckIn
    {
        private static readonly string[] BrowserCookies =
        {
            // Here must be your hoyolab cookies
        };

        private static readonly string[] UserAgents =
        {
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 YaBrowser/20.9.0.933 Yowser/2.5 Safari/537.36",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.192 Safari/537.36 OPR/74.0.3911.218",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:92.0) Gecko/20100101 Firefox/92.0",
            "Mozilla/5.0 (Windows NT 6.1; ) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36 ",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.54 Safari/537.36 Edg/101.0.1210.39 Agency/90.8.3027.28",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.54 Safari/537.36",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:100.0) Gecko/20100101 Firefox/100.0"
        };

        private static readonly Random Random = new ();

        internal void Run()
        {
            foreach (string rawCookies in BrowserCookies)
            {
                var account = MihoyoAccount.CreateInstance(rawCookies);

                if (account == null)
                {
                    Log.Debug("Skipping account initializing...");
                    continue;
                }

                account.UserAgent = UserAgents[Random.Next(UserAgents.Length)];
            }
        }
    }
}
