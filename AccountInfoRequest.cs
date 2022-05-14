using Serilog;

namespace GenshinCheckIn
{
    public sealed class AccountInfoRequest : MihoyoRequest
    {
        private static readonly string AccountInfoUrl = "https://api-os-takumi.mihoyo.com/binding/api/getUserGameRolesByCookie";

        public AccountInfoRequest(AuthenticationData authenticationData, string userAgent) : base(authenticationData,
            userAgent)
        {
            RequestMessage = new HttpRequestMessage(HttpMethod.Get, 
                $"{AccountInfoUrl}?{AdditionalMetaParameters}");
        }

        protected override HttpRequestMessage RequestMessage { get; }
    }
}
