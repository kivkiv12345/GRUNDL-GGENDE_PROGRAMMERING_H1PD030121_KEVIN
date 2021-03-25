using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace ReactionTime
{
    class Program
    {
        public static bool ready;
        public static ConsoleKeyInfo pressedKey;

        public static void failState()
        {
            bool again = true;

            if (ready)

            while (again)
            {
                again = false;
                ConsoleKeyInfo keyinfo = Console.ReadKey();

                switch (keyinfo.Key)
                {
                    case ConsoleKey.Spacebar:
                        Console.WriteLine("Player1 pressed the button too early!");
                        break;
                    case ConsoleKey.Enter:
                        Console.WriteLine("Player2 pressed the button too early!");
                        break;
                    default:
                        again = true;
                        break;
                }
            }

            
        }

        public struct playerTime
        {
            public string Name;
            public TimeSpan Time;
            private const string noName = "???";  // How unfilled highscore entries should be represented.

            public override string ToString() => $"{(Name != null ? Name : noName)}: {Time}";
        }

        private class sortPlayerTimes : IComparer  // IComparer to sort highscore entries based on time.
        {
            public int Compare(object x, object y)
            {
                playerTime p1 = (playerTime)x;
                playerTime p2 = (playerTime)y;
                return p1.Time > p2.Time || p1.Time == new TimeSpan() ? 1 : (p1.Time < p2.Time ? -1 : 0);
            }
        }

        static void Main(string[] args)
        {
            bool again = true;

            playerTime[] highScores = new playerTime[10];

            while (again)
            {
                Thread failure = new Thread(new ThreadStart(failState));
                int playerAmount = 0;
                IDictionary<ConsoleKey, string> players = new Dictionary<ConsoleKey, string>();

                Console.WriteLine("+------------------+");
                Console.WriteLine("Current highscores: ");
                foreach (playerTime score in highScores)
                {
                    Console.WriteLine(score);
                }
                Console.WriteLine("+------------------+");

                Console.Write("\nPress any key to start:");
                Console.ReadKey();

                Console.WriteLine("\nHow many players would like to play?");

                // Validate the user input
                while (playerAmount < 1)
                {
                    try
                    {
                        playerAmount = Convert.ToInt32(Console.ReadLine());
                        if (playerAmount < 1 || playerAmount > 10) throw new Exception(); // Spare the players some pain, by not allowing more than 10 players.
                    } catch (Exception e)
                    {
                        Console.WriteLine("That is sadly not a valid number. Please try again: ");
                    }
                }

                for (int i = 1; i <= playerAmount; i++)
                {
                    bool inUse = true;

                    while (inUse)
                    {
                        inUse = false;

                        Console.WriteLine($"Which key would Player{i} like to use?");
                        ConsoleKey playerKey = Console.ReadKey().Key;
                        Console.WriteLine();

                        if (!players.ContainsKey(playerKey))
                            players.Add(playerKey, $"Player{i}");
                        else
                        {
                            Console.WriteLine($"That key is already in use by {players[playerKey]}, please use another.");
                            inUse = true;
                        }
                    }
                }

                Console.WriteLine("\nPlayers; Press your specified key when O appears on the screen!");
                Thread.Sleep(3000);
                Console.Clear();
                //failure.Start();

                Stopwatch stopWatch = new Stopwatch();
                TimeSpan time;

                Thread.Sleep(new Random().Next(1000, 8001));

                //failure.Interrupt();
                //failure.Join();

                Console.WriteLine("O");
                stopWatch.Start();

                ConsoleKeyInfo key = Console.ReadKey();
                while (!players.ContainsKey(key.Key))
                    key = Console.ReadKey();

                stopWatch.Stop();
                time = stopWatch.Elapsed;

                Console.Clear();
                Console.WriteLine($"{players[key.Key]} was fastest, it took them {time} seconds to press {key.Key}.");

                for (int i = 0; i < highScores.Length; i++)
                {
                    if (highScores[i].Name == null || time < highScores[i].Time)
                    {
                        Console.WriteLine($"{players[key.Key]} has reached top {i + 1} score. Would they like to record it? Y/n: ");
                        if (Console.ReadKey().Key == ConsoleKey.Y)
                        {
                            playerTime newScore = new playerTime();
                            Console.WriteLine("\nUnder what name should this high score be recorded?: ");
                            newScore.Name = Console.ReadLine();
                            newScore.Time = time;
                            highScores[i] = newScore;
                            break;
                        }
                    }
                }

                Array.Sort(highScores, new sortPlayerTimes());

                Console.WriteLine("\nWould you like to play again? Y/n: ");
                again = Console.ReadKey().Key == ConsoleKey.Y;
                Console.Clear();
            }
        }
    }
}
