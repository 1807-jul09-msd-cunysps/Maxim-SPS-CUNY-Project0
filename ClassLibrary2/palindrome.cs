using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class Palindrome
    {

        public Boolean TestPal(string pal)
        {
            pal.Replace(" ", "");
            string rev = new string(pal.Reverse().ToArray());

            if (pal == rev)
                return true;
            else
                return false;


        }

    }
}
