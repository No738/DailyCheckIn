using Serilog;

namespace GenshinCheckIn
{
    public sealed class ClaimRewardRequest : MihoyoHttpRequest
    {
        private static readonly string AccountInfoUrl = "https://hk4e-api-os.mihoyo.com/event/sol/sign";

        public ClaimRewardRequest(AuthenticationData authenticationData, string userAgent) : base(authenticationData,
            userAgent)
        {
            RequestMessage = new HttpRequestMessage(HttpMethod.Post,
                $"{AccountInfoUrl}?act_id=e202102251931481&{AdditionalMetaParameters}");
        }

        protected override HttpRequestMessage RequestMessage { get; }
    }
}
