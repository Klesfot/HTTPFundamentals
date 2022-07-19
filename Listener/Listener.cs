using System;
using System.Net;

namespace Listener
{
    public class Listener
    {
        private string _port = "8888";
        private HttpListener _listener;

        public void Start()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:" + _port + "/");
            _listener.Start();
            Console.WriteLine($"Listener started on port {_port}");
            Receive();
        }

        public void Stop()
        {
            _listener.Stop();
        }

        private void HandleMyNameGetRequest(HttpListenerContext context)
        {
            var response = context.Response;
            response.ContentType = "text/plain";
            var buffer = System.Text.Encoding.UTF8.GetBytes("Sonelie");
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }

        private void HandleInformationGetRequest(HttpListenerContext context)
        {
            var response = context.Response;
            response.StatusCode = 100;
            response.ContentType = "text/plain";
            response.OutputStream.Write(new byte[] { }, 0, 0);
            response.OutputStream.Close();
        }

        private void HandleSuccessGetRequest(HttpListenerContext context)
        {
            var response = context.Response;
            response.StatusCode = 200;
            response.ContentType = "text/plain";
            response.OutputStream.Write(new byte[] { }, 0, 0);
            response.OutputStream.Close();
        }

        private void HandleRedirectionGetRequest(HttpListenerContext context)
        {
            var response = context.Response;
            response.StatusCode = 300;
            response.ContentType = "text/plain";
            response.OutputStream.Write(new byte[] { }, 0, 0);
            response.OutputStream.Close();
        }

        private void HandleClientErrorGetRequest(HttpListenerContext context)
        {
            var response = context.Response;
            response.StatusCode = 400;
            response.ContentType = "text/plain";
            response.OutputStream.Write(new byte[] { }, 0, 0);
            response.OutputStream.Close();
        }

        private void HandleServerErrorGetRequest(HttpListenerContext context)
        {
            var response = context.Response;
            response.StatusCode = 500;
            response.ContentType = "text/plain";
            response.OutputStream.Write(new byte[] { }, 0, 0);
            response.OutputStream.Close();
        }

        private void HandleGetMyNameByHeaderRequest(HttpListenerContext context)
        {
            var response = context.Response;
            response.ContentType = "text/plain";
            response.OutputStream.Write(new byte[] { }, 0, 0);
            response.AddHeader("X-MyName", "Sonelie");
            response.OutputStream.Close();
        }

        private void HandleGetMyNameByCookiesRequest(HttpListenerContext context)
        {
            var response = context.Response;
            response.ContentType = "text/plain";
            response.AppendCookie(new Cookie("MyName", "Sonelie"));
            response.OutputStream.Close();
            response.Close();
        }

        private void Receive()
        {
            _listener.BeginGetContext(new AsyncCallback(ListenerCallback), _listener);
        }

        private void ListenerCallback(IAsyncResult result)
        {
            if (_listener.IsListening)
            {
                var context = _listener.EndGetContext(result);
                var request = context.Request;

                Console.WriteLine($"Received {request.HttpMethod} request on {request.Url} URL");

                if (request.RawUrl.EndsWith("/MyName/"))
                {
                    HandleMyNameGetRequest(context);
                }
                else if (request.RawUrl.EndsWith("/Information/"))
                {
                    HandleInformationGetRequest(context);
                }
                else if (request.RawUrl.EndsWith("/Success/"))
                {
                    HandleSuccessGetRequest(context);
                }
                else if (request.RawUrl.EndsWith("/Redirection/"))
                {
                    HandleRedirectionGetRequest(context);
                }
                else if (request.RawUrl.EndsWith("/ClientError/"))
                {
                    HandleClientErrorGetRequest(context);
                }
                else if (request.RawUrl.EndsWith("/ServerError/"))
                {
                    HandleServerErrorGetRequest(context);
                }
                else if (request.RawUrl.EndsWith("/MyNameByHeader/"))
                {
                    HandleGetMyNameByHeaderRequest(context);
                }
                else if (request.RawUrl.EndsWith("/MyNameByCookies/"))
                {
                    HandleGetMyNameByCookiesRequest(context);
                }

                Receive();
            }
        }
    }
}