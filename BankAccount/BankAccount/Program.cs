using System;
using System.IO;
using System.Text.Json;

namespace BankAccount
{
    class Program
    {
        /// <summary>
        /// Takes an account and message, and logs it to a file.
        /// </summary>
        /// <param name="account">Account which balance should be used.</param>
        /// <param name="logMessage">Message to log.</param>
        public static void Log(BankAccount account, string logMessage)  // Adapted from: https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-open-and-append-to-a-log-file
        {
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                w.WriteLine("  :");
                w.WriteLine($"  :{logMessage}");
                w.WriteLine($"  :Balance is now {account.getBalance}");
                w.WriteLine("-------------------------------");
            }
        }

        static void Main(string[] args)
        {
            BankAccount account;

            while (true)
            {
                Console.WriteLine("How much money are you starting out with?");
                try
                {
                    account = new BankAccount(Convert.ToDouble(Console.ReadLine()));
                    break;
                } catch (Exception e)
                {
                    Console.WriteLine($"Account cannot be created. {e}");
                }
            }

            while (true)
            {
                Console.WriteLine($"You have {account.getBalance} funds in your account.");
                Console.WriteLine("Press 1 to withdraw,\npress 2 to deposit.");

                ConsoleKeyInfo pressedKey = Console.ReadKey();
                if (pressedKey.Key != ConsoleKey.D1 && pressedKey.Key != ConsoleKey.D2)
                {
                    Console.WriteLine("That was not a valid option, please try again.");
                    continue;
                }

                try
                {
                    Console.WriteLine();
                    switch (pressedKey.Key)
                    {
                        case ConsoleKey.D1:
                            Console.WriteLine("How much money would you like to withdraw?");
                            account.withdraw(Convert.ToDouble(Console.ReadLine()));
                            break;
                        case ConsoleKey.D2:
                            Console.WriteLine("How much money would you like to deposit?");
                            account.deposit(Convert.ToDouble(Console.ReadLine()));
                            break;
                        default:
                            throw new Exception("switch default encountered, something bad happend!");
                    }
                } catch (Exception e)
                {
                    Console.WriteLine($"Something went wrong. {e}\nPlease try again.");
                }
            }
        }
    }

    public class BankAccount
    {
        private double balance;

        /// <param name="startingBalance">Amount of money the account should start out with.</param>
        public BankAccount(double startingBalance)
        {
            if (startingBalance < 0) 
                throw new Exception("Balance cannot be negative");
            balance = startingBalance;
            Program.Log(this, $"Account created with {startingBalance} funds.");
        }

        /// <summary>
        /// Withdraws a specified amount of money from the account. Throws an exception if balance goes negative.
        /// </summary>
        /// <param name="amount">The amount of money to withdraw.</param>
        /// <returns>The amount of money withdrawn.</returns>
        public double withdraw(double amount)
        {
            if (balance - amount < 0)
                throw new Exception("Balance cannot be negative");
            balance -= amount;
            Console.WriteLine($"You withdraw {amount} from the account.");
            Program.Log(this, $"{amount} withdrawn from account.");
            return amount;
        }

        /// <summary>
        /// Deposits a specified amount of money. Throws an exception if negative value is provided.
        /// </summary>
        /// <param name="amount">The value to deposit into the account.</param>
        public void deposit(double amount)
        {
            if (amount < 0)
                throw new Exception("Cannot deposit negative value.");
            Console.WriteLine($"You deposit {amount} into the account.");
            balance += amount;
            Program.Log(this, $"{amount} deposited into account.");
        }

        public double getBalance
        {
            get { return balance; }
        }
    }
}
