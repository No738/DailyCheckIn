using System.Net;
using Serilog;

namespace GenshinCheckIn
{
    public abstract class MihoyoRequest
    {
        private readonly AuthenticationData _authenticationData;
        private readonly string _userAgent;

        protected readonly string AdditionalMetaParameters =
            "mhy_auth_required=true&mhy_presentation_style=fullscreen&utm_source=tools&lang=ru-ru&bbs_theme=dark&bbs_theme_device=1";

        protected MihoyoRequest(AuthenticationData authenticationData, string userAgent)
        {
            _authenticationData = authenticationData;
            _userAgent = userAgent;
        }

        protected abstract HttpRequestMessage RequestMessage { get; }

        public bool TrySend(out string result)
        {
            Log.Debug("Sending request to Mihoyo...");

            using var client = new HttpClient(new HttpClientHandler
            {
                CookieContainer = BuildCookieContainer()
            });

            HttpRequestMessage requestMessage = RequestMessage;

            if (requestMessage.RequestUri == null)
            {
                Log.Fatal("Request message URI is null!");

                throw new ArgumentNullException(nameof(requestMessage.RequestUri), "Request message URI is null!");
            }

            if (_userAgent != string.Empty)
            {
                requestMessage.Headers.Add("User-Agent", _userAgent);
            }

            requestMessage.Headers.Add("Accept", "*/*");
            requestMessage.Headers.Add("Host", requestMessage.RequestUri.Host);
            requestMessage.Headers.Add("Accept-Encoding", "br");
            requestMessage.Headers.Add("Connection", "keep-alive");

            try
            {
                HttpResponseMessage responseMessage = client.SendAsync(requestMessage).Result;
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

        private CookieContainer BuildCookieContainer()
        {
            var cookieContainer = new CookieContainer();

            foreach (Cookie cookie in _authenticationData.AuthenticationCookies)
            {
                cookieContainer.Add(cookie);
            }

            return cookieContainer;
        }
    }
}