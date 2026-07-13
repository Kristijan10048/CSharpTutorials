using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson5_1Methods
{
    public class RefMethodCl
    {

        internal static bool IsEven(int iNumb)
        {
            return iNumb % 2 == 0;
        }

        public static bool CanMultiply(int iNumb1, int iNumb2)
        {
            return iNumb1 != 0 && iNumb2 != 0;
        }

        public void MethondNoRef(string str)
        {
            Console.WriteLine($"Input string {str}");
            //Note value can be assignned to the parameter but its not mandatory
            str = " test";
            Console.WriteLine($"Input string after assignment {str}");
        }


        /// <summary>
        /// Method with reference parameter. The parameter is changd in the method
        /// </summary>
        /// <param name="str"></param>
        static public void RefMethod(ref string str)
        {
            //Note value can be assignned to the ref paramter but its not mandatory
            str = " test";
        }

        /// <summary>
        /// Method with out parameter
        /// </summary>
        /// <param name="buffer"></param>
        static public void OutMethod(out string buffer)
        {
            //when keyword out is used the parameter must be assigned with a value
            buffer = "OutMethod";
        }

        static void Main(string[] args)
        {
            string sLocal = "Something new!";

            Console.WriteLine("The string before reference call: {0}", sLocal);

            //call to RefMethod
            RefMethod(ref sLocal);

            Console.WriteLine("The string after reference call: {0}", sLocal);

            Console.WriteLine("Calling out method:");

            var testOutput = "None";
            Console.WriteLine("The string before out call: {0}", testOutput);
            OutMethod(out testOutput);
            Console.WriteLine("The string after out call: {0}", testOutput);

            //wait the user to press key
            Console.ReadKey();
        }
    }
}
