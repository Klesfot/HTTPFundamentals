using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    public class Client
    {
        private string requestUri = "http://localhost:8888/";

        public async void SendMyNameRequest()
        {
            var myNameUri = requestUri + "MyName/";

            var response = await SendRequest(myNameUri, false);
            var resultString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(resultString);
        }

        public async void SendInformationRequest()
        {
            var infoUri = requestUri + "Information/";
            await SendRequest(infoUri);
        }

        public async void SendSuccessRequest()
        {
            var successUri = requestUri + "Success/";
            await SendRequest(successUri);
        }

        public async void SendRedirectionRequest()
        {
            var redirUri = requestUri + "Redirection/";
            await SendRequest(redirUri);
        }

        public async void SendClientErrorRequest()
        {
            var clientErrorUri = requestUri + "ClientError/";
            await SendRequest(clientErrorUri);
        }

        public async void SendServerErrorRequest()
        {
            var serverErrorUri = requestUri + "ServerError/";
            await SendRequest(serverErrorUri);
        }

        public async void SendGetMyNameByHeaderRequest()
        {
            var myNameByHeaderUri = requestUri + "MyNameByHeader/";
            var response = await SendRequest(myNameByHeaderUri, false);
            
            var resultString = string.Empty;

            if (response.Headers.TryGetValues("X-MyName", out var values))
            {
                resultString = values.First();
            }

            Console.WriteLine(resultString);
        }

        public async void SendGetMyNameByCookiesRequest()
        {
            var myNameByCookiesUri = requestUri + "MyNameByCookies/";
            var cookieContainer = new CookieContainer();
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.UseCookies = true;
            httpClientHandler.CookieContainer = cookieContainer;
            var systemUri = new Uri(myNameByCookiesUri);
            cookieContainer.Add(systemUri, new Cookie("MyName", ""));

            var httpClient = new HttpClient(httpClientHandler);
            await httpClient.GetAsync(myNameByCookiesUri);

            var responseCookie = cookieContainer.GetCookies(systemUri)
                .FirstOrDefault(x => x.Name == "MyName");

            Console.WriteLine(responseCookie.Value);
        }

        private async Task<HttpResponseMessage> SendRequest(string uri, bool shouldOutputCode = true)
        {
            using var client = new HttpClient();

            Console.WriteLine($"Sending GET request to {uri}");
            var result = await client.GetAsync(uri);

            if (shouldOutputCode)
            {
                Console.WriteLine($"Got a response, status code: {(int)result.StatusCode}");
            }

            return result;
        }
    }
}