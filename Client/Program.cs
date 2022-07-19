using System;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Commands:" +
                              "\n'n' - MyName request" +
                              "\n'i' - Information request" +
                              "\n's' - Success request" +
                              "\n'r' - Redirection request" +
                              "\n'c' - Error request (Client)" +
                              "\n'e' - Error request (server)" +
                              "\n'h' - GetNameByHeader request" +
                              "\n'o' - GetNameByCookies request" +
                              "\n'q' - quit");

            var client = new Client();

            while (true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.S:
                    {
                        client.SendSuccessRequest();
                    }
                        break;

                    case ConsoleKey.N:
                    {
                        client.SendMyNameRequest();
                    }
                        break;

                    case ConsoleKey.I:
                    {
                        client.SendInformationRequest();
                    }
                        break;

                    case ConsoleKey.C:
                    {
                        client.SendClientErrorRequest();
                    }
                        break;

                    case ConsoleKey.E:
                    {
                        client.SendServerErrorRequest();
                    }
                        break;

                    case ConsoleKey.R:
                    {
                        client.SendRedirectionRequest();
                    }
                        break;
                    case ConsoleKey.H:
                    {
                        client.SendGetMyNameByHeaderRequest();
                    }
                        break;
                    case ConsoleKey.O:
                    {
                        client.SendGetMyNameByCookiesRequest();
                    }
                        break;
                    case ConsoleKey.Q:
                    {
                        break;
                    }
                }
            }
        }
    }
}
