using Serilog;

namespace GenshinCheckIn
{
    public sealed class AccountInfoRequest : MihoyoRequest
    {
        private static readonly string AccountInfoUrl = "https://api-os-takumi.mihoyo.com/binding/api/getUserGameRolesByCookie";

        public AccountInfoRequest(AuthenticationData authenticationData, string userAgent) : base(authenticationData, userAgent) { }

        protected override HttpRequestMessage RequestMessage
        {
            get
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{AccountInfoUrl}?{AdditionalMetaParameters}");

                return requestMessage;
            }
        }
    }
}
