using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson2OperatorsTypesAndVariables
{
    //type | bits | range
    //---------------------------------
    //sbyte	 8	 -128 to 127
    //byte	 8	 0 to 255
    //short	 16	 -32768 to 32767
    //ushort 16	 0 to 65535
    //int	 32	 -2147483648 to 2147483647
    //uint	 32	 0 to 4294967295
    //long	 64	 -9223372036854775808 to 9223372036854775807
    //ulong	 64	 0 to 18446744073709551615
    //char	 16	 0 to 65535
    //type | bits | range
    //---------------------------------
    //float	     32	    7 digits	            1.5 x 10-45 to 3.4 x 1038
    //double	 64	    15-16 digits	        5.0 x 10-324 to 1.7 x 10308
    //decimal	 128	28-29 decimal places	1.0 x 10-28 to 7.9 x 1028
    class Booleans
    {
        /// <summary>
        /// 
        /// </summary>
        static void TheArrayType()
        {
            int[] myInts = { 5, 10, 15 };
            bool[][] myBools = new bool[2][];

            //2x2 matrix;
            myBools[0] = new bool[2];
            myBools[1] = new bool[2];

            double[,] myDoubles = new double[2, 2];
            string[] myStrings = new string[3];

            myBools[0][0] = true;
            myBools[0][1] = false;
            myBools[1][0] = false;
            myBools[1][1] = true;

            for (int i = 0; i < myBools.Length; i++)
                for (int j = 0; j < myBools.Length; j++)
                    Console.WriteLine("myBools[{0}][{1}] = {2}", i, j, myBools[i][j]);

            myDoubles[0, 0] = 3.147;
            myDoubles[0, 1] = 7.157;
            myDoubles[1, 1] = 2.117;
            myDoubles[1, 0] = 56.00138917;

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    Console.WriteLine(" myDoubles[{0}][{1}] = {2}", i, j, myDoubles[i, j]);

            Console.WriteLine("myDoubles[0, 0]: {0}, myDoubles[1, 0]: {1}", myDoubles[0, 0], myDoubles[1, 0]);

            myStrings[0] = "Joe";
            myStrings[1] = "Matt";
            myStrings[2] = "Robert";
            Console.WriteLine("myStrings[0]: {0}, myStrings[1]: {1}, myStrings[2]: {2}", myStrings[0], myStrings[1], myStrings[2]);

        }

        static void Main(string[] args)
        {
            bool content = true;
            bool noContent = false;
            Console.WriteLine("It is {0} that C# station C# programming language content", content);
            Console.WriteLine("The statement above is not {0}", noContent);
            Console.ReadKey();
            Console.WriteLine("---------------------------------");
            int unary = 0;
            int preIncrement;
            int preDecrement;
            int postDecrement;
            int postIncrement;
            //int positive;
            //int negative;
            sbyte bitNot;
            //bool logNot;

            preIncrement = ++unary;
            Console.WriteLine("pre-increment: {0} unary = {1}", preIncrement, unary);

            preDecrement = --unary;
            Console.WriteLine("pre-Decrement: {0} unary = {1}", preDecrement, unary);

            postDecrement = unary--;
            Console.WriteLine("post-Decrement {0} unary = {1}", postDecrement, unary);

            postIncrement = unary++;
            Console.WriteLine("post-Increment {0} unary = {1}", preIncrement, unary);

            Console.WriteLine("Final value of unary: {0}", unary);

            bitNot = 0;
            bitNot = (sbyte)(~bitNot);
            Console.WriteLine("Bitwise Not: {0}", bitNot);

            byte theOne = 0;
            theOne = (byte)(~theOne);
            Console.WriteLine("The one bitwise not={0}", theOne);

            Console.WriteLine("---------------------------------");


            TheArrayType();
            Console.ReadKey();
        }
    }
}