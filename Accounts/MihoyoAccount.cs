using Serilog;

namespace GenshinCheckIn
{
    public class MihoyoAccount
    {
        private readonly AuthenticationData _authenticationData;

        private MihoyoAccount(AuthenticationData authenticationData, string userAgent)
        {
            _authenticationData = authenticationData;
            UserAgent = userAgent;
        }

        public string UserAgent { get; set; }

        public static MihoyoAccount? CreateInstance(string rawCookies, string userAgent = "")
        {
            Log.Debug("Creating new instance of MihoyoAccount.\n" +
                      $"Raw cookie data: {rawCookies}");

            var authenticationData = AuthenticationData.CreateInstance(rawCookies);

            if (authenticationData == null)
            {
                Log.Debug("AuthenticationData is invalid!");

                return null;
            }

            var account = new MihoyoAccount(authenticationData, userAgent);

            if (account.TryGetAccountInfo() == false)
            {
                Log.Debug("Failed to parse account data");

                return null;
            }

            new RewardInfoRequest(authenticationData, account.UserAgent).TrySend(out string result1);
            Log.Debug($"Current reward info result: {result1}");

            new ClaimRewardRequest(authenticationData, account.UserAgent).TrySend(out string result2);
            Log.Debug($"Current reward request result: {result2}");

            return account;
        }

        private bool TryGetAccountInfo()
        {
            return new AccountInfoRequest(_authenticationData, UserAgent)
                .TrySend(out string result);
        }
    }
}