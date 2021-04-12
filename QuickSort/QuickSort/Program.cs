using System;

namespace QuickSort
{
    public static class Program
    {

        // A utility functions to swap two elements using tuples
        static void swap(ref int a, ref int b) => (a, b) = (b, a);
        static void swap(ref string a, ref string b) => (a, b) = (b, a);

        /* This function takes last element as pivot, places
        the pivot element at its correct position in sorted
        array, and places all smaller (smaller than pivot)
        to left of pivot and all greater elements to right
        of pivot */
        static int partition(int[] arr, int low, int high)
        {
            int pivot = arr[high]; // pivot
            int i = low - 1; // Index of smaller element and indicates the right position of pivot found so far

            for (int j = low; j <= high - 1; j++)
            {
                // If current element is smaller than the pivot
                if (arr[j] < pivot)
                {
                    i++; // increment index of smaller element
                    swap(ref arr[i], ref arr[j]);
                }
            }
            swap(ref arr[i + 1], ref arr[high]);
            return (i + 1);
        }

        static int partition(string[] arr, int low, int high)
        {
            // Compare two strings by iterating until we find a character with a lower ascii value.
            bool stringCompare(string str1, string str2)
            {
                int shortestLength = str1.Length < str2.Length ? str1.Length : str2.Length;
                for (int i = 0; i < shortestLength; i++)
                {
                    if (str1[i] < str2[i])
                        return true;
                    else if (str1[i] > str2[i])
                        return false;
                }
                return true;
            }

            string pivot = arr[high]; // pivot
            int i = low - 1; // Index of smaller element and indicates the right position of pivot found so far

            for (int j = low; j <= high - 1; j++)
            {
                // If current element is smaller than the pivot
                if (stringCompare(arr[j], pivot))
                {
                    i++; // increment index of smaller element
                    swap(ref arr[i], ref arr[j]);
                }
            }
            swap(ref arr[i + 1], ref arr[high]);
            return (i + 1);
        }

        /* The main function that implements QuickSort
        arr[] --> Array to be sorted,
        low --> Starting index,
        high --> Ending index */
        public static void quickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                /* pi is partitioning index, arr[p] is now
                at right place */
                int pi = partition(arr, low, high);

                // Separately sort elements before
                // partition and after partition
                quickSort(arr, low, pi - 1);
                quickSort(arr, pi + 1, high);
            }
        }

        /// <summary>
        /// Overloaded quickSort to sort string array alphabetically.
        /// </summary>
        /// <param name="arr"> Array to sort. </param>
        /// <param name="low"> Starting index. </param>
        /// <param name="high"> Ending index. </param>
        public static void quickSort(string[] arr, int low, int high)
        {
            if (low < high)
            {
                int pi = partition(arr, low, high);

                quickSort(arr, low, pi - 1);
                quickSort(arr, pi + 1, high);
            }
        }

        static void Main(string[] args)
        {
            // Quicksort algorithm has been converted from C++ and expanded from https://www.geeksforgeeks.org/quick-sort/

            int[] arr = { 10, 7, 8, 9, 1, 5 };
            Console.WriteLine($"Unsorted array: [{string.Join(", ", arr)}]");
            quickSort(arr, 0, arr.Length - 1);
            Console.WriteLine($"Sorted array: [{string.Join(", ", arr)}]");

            string[] strarr = { "hej", "hvadså", "gårdet", "kandu", "ikkenej", };
            Console.WriteLine($"Unsorted string array: [{string.Join(", ", strarr)}]");
            quickSort(strarr, 0, strarr.Length - 1);
            Console.WriteLine($"Sorted string array: [{string.Join(", ", strarr)}]");

            Console.ReadLine();
        }
    }
}
