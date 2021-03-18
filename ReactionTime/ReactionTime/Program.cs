using System;
using System.Diagnostics;
using System.Threading;

namespace ReactionTime
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start:");
            Console.ReadKey();
            Console.WriteLine("Press ENTER when O appears on the screen");
            Thread.Sleep(2000);
            Console.Clear();

            Random random = new Random();
            Stopwatch stopWatch = new Stopwatch();
            TimeSpan time;

            Thread.Sleep(random.Next(1000, 8001));
            Console.WriteLine("O");
            stopWatch.Start();

            ConsoleKeyInfo key = Console.ReadKey();
            while (key.Key != ConsoleKey.Enter)
                key = Console.ReadKey();
            stopWatch.Stop();
            time = stopWatch.Elapsed;

            Console.Clear();
            Console.WriteLine($"It took you: {time} seconds to press ENTER.");
            Console.ReadLine();

        }
    }
}
