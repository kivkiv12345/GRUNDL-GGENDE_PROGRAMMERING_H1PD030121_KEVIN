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

        #region Code4L8ter
        // This method will be expanded to detect early key presses, when Microsoft allow me to abort threads t(ಠ益ಠt)
        /*public static void failState()
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

            
        }*/
        #endregion

        /// <summary>
        /// Used to store highscores of players.
        /// </summary>
        public struct playerTime
        {
            public string Name;
            public TimeSpan Time; // NOTE: Defaults to 0000000'something, rather than NULL
            private const string noName = "???";  // Describes how unfilled highscore entries should be represented.

            public override string ToString() => $"{(Name != null ? Name : noName)}: {Time}";
        }

        /// <summary>
        /// IComparer to sort highscore entries based on time.
        /// </summary>
        private class sortPlayerTimes : IComparer
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

            playerTime[] highScores = new playerTime[10];  // Highscore array which should persist across repetitions.

            while (again)
            {
                // Reference material for later. Seems like it's impossible to call .abort() on a thread, so im not sure why it's there ¯\_(ツ)_/¯
                //Thread failure = new Thread(new ThreadStart(failState));

                int playerAmount = 0;
                // Links player's key, with their name. Also allows us to easily check if a key is in use, and inherently disallows duplicates.
                IDictionary<ConsoleKey, string> players = new Dictionary<ConsoleKey, string>();

                Console.WriteLine("+------------------+");
                Console.WriteLine("Current highscores: ");
                foreach (playerTime score in highScores)
                    Console.WriteLine(score);
                Console.WriteLine("+------------------+");

                Console.Write("\nPress any key to start:");
                Console.ReadKey();

                Console.WriteLine("\nHow many players would like to play?");

                // Continuously prompt for player amount, until a valid number is given.
                while (playerAmount < 1)
                {
                    try
                    {
                        playerAmount = Convert.ToInt32(Console.ReadLine());
                        if (playerAmount < 1 || playerAmount > 10) throw new Exception(); // Spare the players some pain, by not allowing more than 10 players.
                    } catch (Exception) { Console.WriteLine("That is sadly not a valid number. Please try again: "); }
                }

                for (int i = 1; i <= playerAmount; i++)
                {
                    bool inUse = true;

                    while (inUse)
                    {
                        inUse = false;  // Stop looping if all goes well.

                        Console.WriteLine($"Which key would Player{i} like to use?");
                        ConsoleKey playerKey = Console.ReadKey().Key;
                        Console.WriteLine();

                        if (!players.ContainsKey(playerKey))  // Confirm that the pressed key isn't in use by another player.
                            players.Add(playerKey, $"Player{i}");
                        else
                        {
                            Console.WriteLine($"That key is already in use by {players[playerKey]}, please use another.");
                            inUse = true;  // Loop again, since we can't use that key.
                        }
                    }
                }

                Console.WriteLine("\nPlayers; Press your specified key when O appears on the screen!");
                Thread.Sleep(3000);  // Allow the players some time to read the message before the game begins.
                Console.Clear();
                //failure.Start();

                Stopwatch stopWatch = new Stopwatch();
                TimeSpan time;  // Variables used to get the time for the fastest player. Assigned here to increase reaction speed.

                Thread.Sleep(new Random().Next(1000, 8001));  // Wait for a random amount of time.

                //failure.Interrupt();  // ffs Microsoft (ノಠ益ಠ)ノ彡┻━┻
                //failure.Join();

                Console.WriteLine("O");
                stopWatch.Start();

                ConsoleKeyInfo key = Console.ReadKey();  // Continuously propmt for a key, until a valid one is given.
                while (!players.ContainsKey(key.Key))
                    key = Console.ReadKey();

                stopWatch.Stop();
                time = stopWatch.Elapsed;

                Console.Clear();
                Console.WriteLine($"{players[key.Key]} was fastest, it took them {time} seconds to press {key.Key}.");

                for (int i = 0; i < highScores.Length; i++)  // Loop until we find a nonexistent player, or one with a lower score.
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

                Array.Sort(highScores, new sortPlayerTimes());  // Seems to do absolutely nothing, probably Microsoft's fault, so there's nothing we can do about it ¯\_(ツ)_/¯

                Console.WriteLine("\nWould you like to play again? Y/n: ");
                again = Console.ReadKey().Key == ConsoleKey.Y;
                Console.Clear();
            }
        }
    }
}
