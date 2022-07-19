using System;

namespace Listener
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var listener = new Listener();
            listener.Start();

            while (true)
            {
                Console.WriteLine("Press 'q' to quit");

                var consoleKeyInfo = Console.ReadKey(true);

                if (consoleKeyInfo.Key == ConsoleKey.Q) break;
            }

            listener.Stop();
        }
    }
}
