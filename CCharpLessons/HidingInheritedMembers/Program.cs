using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HidingInheritedMembers
{
    class Program
    {
        public class A { public int Counter = 1; }

        //hide Counter form Class A
        public class B : A { public  new int Counter = 2; }

        static void Main(string[] args)
        {
        }
    }
}
