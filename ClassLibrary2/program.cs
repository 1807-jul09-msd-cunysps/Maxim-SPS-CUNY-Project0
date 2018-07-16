using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    class Program
    {
        static void Main(string[] args)

        {
            Palindrome Pal1 = new Palindrome();

            if (Pal1.TestPal("tacocat"))
                Console.WriteLine("The word is a palindrome!");
            else
                Console.WriteLine("The word is not a palindrome...");

            Console.Read();

        }
    }
  
}
