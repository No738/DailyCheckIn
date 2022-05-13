using Serilog;

namespace GenshinCheckIn
{
    public sealed class AccountInfoRequest : MihoyoRequest
    {
        private static readonly string AccountInfoUrl = "https://api-os-takumi.mihoyo.com/binding/api";
        private readonly string _userAgent;

        public AccountInfoRequest(AuthenticationData authenticationData, string userAgent) :
            base(authenticationData)
        {
            _userAgent = userAgent;
        }

        protected override HttpRequestMessage BuildRequestMessage()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{AccountInfoUrl}/getUserGameRolesByCookie?{AdditionalMetaParameters}");

            requestMessage.Headers.Add("User-Agent", _userAgent);
            requestMessage.Headers.Add("Accept", "*/*");
            requestMessage.Headers.Add("Host", "hk4e-api-os.mihoyo.com");
            requestMessage.Headers.Add("Accept-Encoding", "br");
            requestMessage.Headers.Add("Connection", "keep-alive");

            return requestMessage;
        }
    }
}
