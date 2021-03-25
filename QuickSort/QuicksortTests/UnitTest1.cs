using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickSort;

namespace QuicksortTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IntTests()
        {
            int[] arr1 = { 120, 108, 96, 84, 72, 60, 48, 36, 24, 12 };
            Program.quickSort(arr1, 0, arr1.Length - 1);
            CollectionAssert.AreEqual(new int[] { 12, 24, 36, 48, 60, 72, 84, 96, 108, 120 }, arr1);

            int[] arr2 = { 5, 4, 3, 1 };
            Program.quickSort(arr2, 0, arr2.Length - 1);
            CollectionAssert.AreEqual(new int[] { 1, 3, 4, 5 }, arr2);
        }

        [TestMethod]
        public void StringTests()
        {
            string[] arr1 = { "hej", "ikkehej", "bassboost" };
            Program.quickSort(arr1, 0, arr1.Length - 1);
            CollectionAssert.AreEqual(new string[] { "bassboost", "hej", "ikkehej" }, arr1);
        }
    }
}
