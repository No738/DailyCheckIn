using System.Net;
using Serilog;

namespace GenshinCheckIn
{
    public abstract class MihoyoRequest
    {
        private readonly AuthenticationData _authenticationData;

        protected readonly string AdditionalMetaParameters =
            "mhy_auth_required=true&mhy_presentation_style=fullscreen&utm_source=tools&lang=ru-ru&bbs_theme=dark&bbs_theme_device=1";

        protected MihoyoRequest(AuthenticationData authenticationData)
        {
            _authenticationData = authenticationData;
        }

        protected abstract HttpRequestMessage BuildRequestMessage();

        public bool TrySend(out string result)
        {
            Log.Debug("Sending request to Mihoyo...");

            var cookieContainer = new CookieContainer();
            
            foreach (Cookie cookie in _authenticationData.AuthenticationCookies)
            {
                cookieContainer.Add(cookie);
            }

            using var handler = new HttpClientHandler
            {
                CookieContainer = cookieContainer
            };

            using var client = new HttpClient(handler);

            try
            {
                HttpResponseMessage responseMessage = client.SendAsync(BuildRequestMessage()).Result;
                result = responseMessage.Content.ReadAsStringAsync().Result;
            }
            catch(Exception ex)
            {
                Log.Error("Failed to send https request!\n" +
                          $"Exception: {ex}");

                result = string.Empty;
                return false;
            }

            Log.Debug($"Received response: {result}");
            return true;
        }
    }
}