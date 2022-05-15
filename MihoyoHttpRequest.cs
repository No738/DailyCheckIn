using System.Net;
using Serilog;

namespace GenshinCheckIn
{
    public abstract class MihoyoHttpRequest
    {
        private readonly AuthenticationData _authenticationData;
        private readonly string _userAgent;

        protected readonly string AdditionalMetaParameters =
            "mhy_auth_required=true&mhy_presentation_style=fullscreen&utm_source=tools&lang=ru-ru&bbs_theme=dark&bbs_theme_device=1";

        protected MihoyoHttpRequest(AuthenticationData authenticationData, string userAgent)
        {
            _authenticationData = authenticationData;
            _userAgent = userAgent;
        }

        protected abstract HttpRequestMessage RequestMessage { get; }

        public bool TrySend(out string result)
        {
            Log.Debug("Sending request to Mihoyo...");

            AddRequestHeaders();

            try
            {
                using var client = new HttpClient(new HttpClientHandler
                {
                    CookieContainer = BuildCookieContainer()
                });

                HttpResponseMessage responseMessage = client.SendAsync(RequestMessage).Result;
                result = responseMessage.Content.ReadAsStringAsync().Result;

                Log.Debug($"Received response: {result}");
            }
            catch(Exception ex)
            {
                Log.Error("Failed to send https request!\n" +
                          $"Exception: {ex}");
                result = string.Empty;

                return false;
            }

            return true;
        }

        private void AddRequestHeaders()
        {
            if (RequestMessage.RequestUri == null)
            {
                Log.Fatal("Request message URI is null!");

                throw new ArgumentNullException(nameof(RequestMessage.RequestUri), "Request message URI is null!");
            }

            if (_userAgent != string.Empty)
            {
                RequestMessage.Headers.Add("User-Agent", _userAgent);
            }

            RequestMessage.Headers.Add("Accept", "*/*");
            RequestMessage.Headers.Add("Host", RequestMessage.RequestUri.Host);
            RequestMessage.Headers.Add("Accept-Encoding", "br");
            RequestMessage.Headers.Add("Connection", "keep-alive");
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