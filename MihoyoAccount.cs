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

        public static MihoyoAccount? CreateInstance(string rawCookies)
        {
            Log.Debug("Creating new instance of MihoyoAccount.\n" +
                      $"Raw cookie data: {rawCookies}");

            var authenticationData = AuthenticationData.CreateInstance(rawCookies);

            if (authenticationData == null)
            {
                Log.Debug("AuthenticationData is invalid!");

                return null;
            }

            var account = new MihoyoAccount(authenticationData);

            if (account.TryGetAccountInfo() == false)
            {
                Log.Debug("Failed to parse account data");
                return null;
            }

            return account;
        }

        private bool TryGetAccountInfo()
        {
            if (new AccountInfoRequest(_authenticationData,
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:100.0) Gecko/20100101 Firefox/100.0")
                    .TrySend(out string result) == false)
            {
                return false;
            }

            Log.Information(result);
            return true;
        }
    }
}