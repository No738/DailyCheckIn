﻿using Serilog;

namespace GenshinCheckIn
{
    public sealed class RewardInfoRequest : MihoyoRequest
    {
        private static readonly string AccountInfoUrl = "https://hk4e-api-os.mihoyo.com/event/sol/info";

        public RewardInfoRequest(AuthenticationData authenticationData, string userAgent) : base(authenticationData, userAgent) { }

        protected override HttpRequestMessage BuildRequestMessage()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{AccountInfoUrl}?act_id=e202102251931481&{AdditionalMetaParameters}");

            return requestMessage;
        }
    }
}