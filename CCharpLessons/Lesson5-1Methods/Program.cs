using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson5_1Methods
{
    class RefMethodCl
    {
        /// <summary>
        /// Method with reference parameter
        /// </summary>
        /// <param name="str"></param>
        static public void RefMethod(ref string str)
        {
           
            str = " test";
        }

        /// <summary>
        /// Method with out parameter
        /// </summary>
        /// <param name="buffer"></param>
        static public void OutMethod(out string buffer)
        {
            //when keyword ref is used the parameter must be assigned with a value
            buffer = "OutMethod";
        }

        static void Main(string[] args)
        { 
            string sLocal = "Something new!";

            Console.WriteLine("The string before reference call {0}", sLocal);

            //call to RefMethod
            RefMethod(ref sLocal);

            Console.WriteLine("The string after reference call {0}", sLocal);

            //wait the user to press key
            Console.ReadKey();
        }
    }
}
