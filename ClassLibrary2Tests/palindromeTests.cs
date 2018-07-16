using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.Tests
{
    [TestClass()]
    public class PalindromeTests
    {
        [TestMethod()]
        public void TestPalTest()
        {
            Palindrome Pal1 = new Palindrome();

            if (Pal1.TestPal("tacocat"))
                Console.WriteLine("The word is a palindrome!");
            else
                Console.WriteLine("The word is not a palindrome...");

            Console.WriteLine(Pal1.TestPal("bill"));
            Console.WriteLine(Pal1.TestPal(""));

            Console.Read();
            Assert.Fail();
        }
    }
}