using System;

namespace HidingInheritedMembers
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        public class A { public int Counter = 1; }

        /// <summary>
        /// 
        /// </summary>
        public class B : A { public new int Counter = 2; }

        static void Main(string[] args)
        {
        }
    }
}
