using Serilog;

namespace GenshinCheckIn
{
    public class MihoyoAccount
    {
        private readonly AuthenticationData _authenticationData;

        private MihoyoAccount(AuthenticationData authenticationData)
        {
            _authenticationData = authenticationData;
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

            var account = new MihoyoAccount(authenticationData)
            {
                UserAgent = userAgent
            };

            if (account.TryGetAccountInfo() == false)
            {
                Log.Debug("Failed to parse account data");

                return null;
            }

            new ClaimRewardRequest(authenticationData, account.UserAgent).TrySend(out string result);
            Log.Debug($"Current reward request result: {result}");

            return account;
        }

        private bool TryGetAccountInfo()
        {
            if (new AccountInfoRequest(_authenticationData, UserAgent)
                    .TrySend(out string result) == false)
            {
                return false;
            }
            
            return true;
        }
    }
}