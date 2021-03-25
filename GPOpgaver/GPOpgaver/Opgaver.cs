using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GPOpgaver
{
    public static class Opgaver
    {
        /*
        * Introduktion til Algoritmer
        * Exercise 1. 
        * Describe an algorithm that interchanges the values of the variables x and y, using only assignment statements.
        * What is the minimum number of assignment statements needed to do so?
        */
        public static void Interchange(ref int x, ref int y)
        {
            //Write your solution here

            // As of .Net Framework 4.7.1, it's possible to swap tuples. Swapping variables therfore require only a single assignment.
            (x, y) = (y, x);
        }
        /*
        * Introduktion til Algoritmer
        * Exercise 2. 
        * 2.Describe an algorithm that uses only assignment statements to replace thevalues of the triple(x, y, z) with (z, x, y).
        * What is the minimum number of assignment statements needed?
        */
        public static void InterchangeTriple(ref int a, ref int b, ref int c)
        {
            //Write your solution here

            // Based on the same principle as the method above.
            (a, b, c) = (c, a, b);
        }
        /*
         * Introduktion til Algoritmer
         * Exercise 3.
         * A palindrome is a string that reads the same forward and backward.
         * Describe an algorithm for determining whether a string of n characters is a palindrome.
         */
        public static bool IsPalindrome(string s)
        {
            //Write your solution here

            // Class based solution
            char[] temparr = s.ToCharArray();
            Array.Reverse(temparr);  // Multiple statements are sadly needed as: 'Array.Reverse()' doesn't return a reversed array.
            Console.WriteLine(Enumerable.SequenceEqual(s.ToCharArray(), temparr));

            // Iterative solution
            for (int i = 0; i < s.Length / 2; i++)
            {
                if (s[i] != s[s.Length - i - 1])  // Compare characters at the end and start.
                {
                    return false;
                }
            }
            return true;  // Passing the loop means we are dealing with a palindrome.
        }
        /*
         * Introduktion til Algoritmer
         * Exercise 4.a.
         * List all the steps used to search for 9 in the sequence 1, 3, 4, 5, 6, 8, 9, 11
         * Do this by completing the unit test for 4A
         */
        public static int StepsInLinearSearch(int searchFor, int[] intergerArray)
        {
            //Write your solution here

            // Class based solution
            return intergerArray.ToList().IndexOf(searchFor) + 1; // +1 needed to pass unit tests. ¯\_( ͡° ͜ʖ ͡°)_/¯

            // Iterative solution
            for (int i = 0; i < intergerArray.Length; i++)
            {
                if (intergerArray[i] == searchFor)
                {
                    return i + 1;  // +1 needed to pass unit tests. ¯\_( ͡° ͜ʖ ͡°)_/¯
                }
            }
            return -1; // Element is not present in the array.
        }
        /*
         * Introduktion til Algoritmer
         * Exercise 4.b.
         * List all the steps used to search for 9 in the sequence 1, 3, 4, 5, 6, 8, 9, 11
         * Do this by completing the unit test for 4B
         */
        public static int StepsInBinarySearch(int[] integerArray, int arrayStart, int arrayEnd, int searchFor, int depth=1)
        {
            //Write your solution here

            // Adapted from: https://www.geeksforgeeks.org/binary-search/
            if (arrayEnd >= arrayStart)
            {
                int mid = arrayStart + (arrayEnd - arrayStart) / 2;

                // If the element is present at the middle itself.
                if (integerArray[mid] == searchFor)
                    return depth;

                // If the element is smaller than mid, then it can only be present in the left side of the array.
                if (integerArray[mid] > searchFor)
                    return StepsInBinarySearch(integerArray, arrayStart, mid - 1, searchFor, depth + 1);

                // Else the element can only be present in the right side of the array.
                return StepsInBinarySearch(integerArray, mid + 1, arrayEnd, searchFor, depth + 1);
            }

            // We reach here; when the element is not present in the array 
            return depth;
        }
        /*
         * Introduktion til Algoritmer
         * Exercise 5.
         * Describe an algorithm based on the linear search for de-termining the correct position in which to insert a new element in an already sorted list.
         */
        public static int InsertSortedList(List<int> sortedList, int insert)
        {
            //Write your solution here

            // Iterate over the list until we find the index to insert our number at.
            for (int i = 0; i < sortedList.Count - 2; i++)
            {
                if (sortedList[i + 1] > insert) // Insert when the next index is larger.
                {
                    sortedList.Insert(i, insert);
                    return i + 1;  // +1 needed to pass unit tests. ¯\_( ͡° ͜ʖ ͡°)_/¯
                }
            }
            sortedList.Add(insert);  // Append to the end when no matches are found.
            return sortedList.Count;
        }
        /*
         * Exercise 6.
         * Arrays
         * Create a function that takes two numbers as arguments (num, length) and returns an array of multiples of num up to length.
         * Notice that num is also included in the returned array.
         */
        public static int[] ArrayOfMultiples(int num, int length)
        {
            //Write your solution here

            int[] temparr = new int[length]; // Create our array using the provided length.

            for (int i = 0; i < length; i++)  // Populate it with multiples.
            {
                temparr[i] = num * (i + 1);
            }
            return temparr;
        }
        /*
         * Exercise 7.
         * Create a function that takes in n, a, b and returns the number of values raised to the nth power that lie in the range [a, b], inclusive.
         * Remember that the range is inclusive. a < b will always be true.
         */
        public static int PowerRanger(int power, int min, int max)
        {
            //Write your solution here
            int counter = 0;

            for (int i = 1; i < max + 1; i++)
            {
                double result = Math.Pow(i, power);
                if (result - 1 > max) // Return the current counter if the power of value is above the max.
                {
                    return counter;
                }
                else if (result >= min) // Otherwise increment the counter if it is above or equal to our min.
                {
                    counter++;
                }
            }
            return counter; // Returns counter ;)
        }
        /*
         * Exercise 8.
         * Create a Fact method that receives a non-negative integer and returns the factorial of that number.
         * Consider that 0! = 1.
         * You should return a long data type number.
         * https://www.mathsisfun.com/numbers/factorial.html
         */
        public static long Factorial(long n)
        {
            //Write your solution here

            if (n != 0) // Exclude 0 from the normal operation.
            {
                for (long i = n - 1; i > 1; i--)
                {
                    n *= i;
                }
            }
            return n != 0 ? n : 1;
        }
        /*
         * Exercise 9.
         * Write a function which increments a string to create a new string.
         * If the string ends with a number, the number should be incremented by 1.
         * If the string doesn't end with a number, 1 should be added to the new string.
         * If the number has leading zeros, the amount of digits should be considered.
         */
        public static string IncrementString(string txt)
        {
            //Write your solution here

            // We create a list to store character representations of the numbers we find.
            List<char> lst = new List<char> { };

            char[] txtarr = txt.ToCharArray();
            Array.Reverse(txtarr); // Reverse the array, such that the foreach will iterate from the end.

            // Search the string for trailing numbers.
            foreach (char ch in txtarr)
            {
                if (char.IsNumber(ch))
                    lst.Add(ch);
                else
                    break;
            }

            lst.Reverse(); // Reverse the list, such that we get the number in the right order.

            // Combine the string (excluding found numbers) with a string from the found numbers + 1.
            return lst.Count > 0 ? txt.Substring(0, txt.Length - lst.Count) + (Convert.ToInt32(new string(lst.ToArray())) + 1).ToString(new string('0', lst.Count)) : txt + '1';
        }
        /*
         * Exercise 10.
         * Write a method to validate a sercure password.
         *     The password must contain at least one uppercase character.
         *     The password must contain at least one lowercase character.
         *     The password must contain at least one number.
         *     The password must contain at least one special character ! @ # $ % ^ & * ( ) + = - { } [ ] : ; " ' ? < > , . _
         *     The password must be at least 8 characters in length.
         *     The password must not be over 25 characters in length.
         */
        public static bool ValidatePassword(string password)
        {
            //Write your solution here

            // Save ourselves some statements, by assigning all the values at once.
            bool upcase, lowcase, num, spe = 
                upcase = lowcase = num = false;

            // We don't bother iterating if the length is invalid.
            if (password.Length >= 25 || password.Length < 8)
                return false;

            // Unlisted special characters apparently invalidate the password!! (ノ゜Д゜)ノ ︵ ┻━┻
            Regex hasAccentedChar = new Regex(@"[âãäåæçèéêëìíîïðñòóôõøùúûüýþÿı]"); // Shamelessly stolen from Rasmus :)
            if (hasAccentedChar.IsMatch(password))
                return false;

            // We use a HashSet, as they ought to feature near O(1) lookup time complexity... They do in Python anyway... ¯\_( ͡° ͜ʖ ͡°)_/¯
            HashSet<char> specials = new HashSet<char> { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '+', '=', '-', '{', '}', '[', ']', ':', ';', '\"', '\'', '?', '<', '>', ',', '.', '_' };

            foreach (char ch in password)
            {
                // Check for lacking requirements.
                if (!upcase && char.IsUpper(ch))
                    upcase = true;
                if (!lowcase && char.IsLower(ch))
                    lowcase = true;
                if (!num && char.IsNumber(ch))
                    num = true;
                if (!spe && specials.Contains(ch))
                    spe = true;

                // Return true when all requirements are met.
                if (upcase && lowcase && num && spe)
                    return true;
            }

            // Return false if we manage to iterate over the entire string without finding all requirements.
            return false;
        }
    }
}