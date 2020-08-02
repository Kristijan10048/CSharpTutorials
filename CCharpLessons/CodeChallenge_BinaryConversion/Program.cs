using System;
using System.Collections.Generic;
namespace CodeChallenge_BinaryConversion
{
    class Program
    {
        static void DbgPrint(string sLine, params object[] ags)
        {
            //set it to false to turn it off
            bool bDebugMode = false;
            
            
            if (bDebugMode)
                Console.WriteLine(sLine, ags);
        }

        /// <summary>
        /// Convert integer to binary and count number of 1s and display the position 
        /// of binary 1s starting from most significant bit
        /// </summary>
        /// <param name="iDecNumber"></param>
        static void IntToBin(int iDecNumber)
        {

            //int[] output;
            int iRem = 0;
            int result = 0;
            int iOnes = 0;
            int i=0;
            DbgPrint("--------------------------------------------");
            DbgPrint("Number : {0}", iDecNumber);
            Stack<int> buffer = new Stack<int>();
            do
            {
                result = iDecNumber / 2;
                iRem = iDecNumber % 2;
                iDecNumber = result;

                if (iRem == 1)
                    iOnes++;
                buffer.Push(iRem);
                DbgPrint("{0}. Result {1} remainder {2}", i, result, iRem);
                i++;
            } while (result > 0);


            int bitPos = 1;
            //int[] one
            while (buffer.Count > 0)
            {
                int bit = buffer.Pop();
                if (bit == 1)
                    DbgPrint("Significant bit position : {0}", bitPos);

                bitPos++;
            }

            DbgPrint("Number of 1s: {0}", iOnes);
            DbgPrint("--------------------------------------------");
        }

        static void Main(string[] args)
        {
            int[] iNumbers = { 11, 12, 123, 45, 66 };

            foreach (int i in iNumbers)
            {
                IntToBin(i);
            }
            Console.ReadKey();
        }
    }
}
