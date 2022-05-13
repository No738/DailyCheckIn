using Serilog;

namespace GenshinCheckIn
{
    public sealed class RewardsThisMonthRequest : MihoyoRequest
    {
        private static readonly string AccountInfoUrl = "https://sg-hk4e-api.hoyolab.com/event/sol/home";

        public RewardsThisMonthRequest(AuthenticationData authenticationData, string userAgent) :
            base(authenticationData, userAgent) { }

        protected override HttpRequestMessage BuildRequestMessage()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{AccountInfoUrl}?act_id=e202102251931481&{AdditionalMetaParameters}");

            return requestMessage;
        }
    }
}
