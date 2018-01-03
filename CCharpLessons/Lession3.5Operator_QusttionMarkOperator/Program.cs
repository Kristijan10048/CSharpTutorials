using System;
using System.Collections.Generic;
using System.Text;

namespace Lession3._Operator_QusttionMarkOperator
{
    class Program
    {
        public string ChekcNumber(int number)
        {
            //The conditional operator (?:) returns one of two values depending on the value of a Boolean expression. 
            //Following is the syntax for the conditional operator.
            //condition ? first_expression(condition == true) : second_expression ((condition == false));
            return (number > 0) ? "positive" : "negative";
        }

        public bool IsPositiveNumber(int number)
        {
            return (number > 0) ? true : false;
        }

        // 1 = true
        // 2 = false
        int BoolToInt;

        static void Main(string[] args)
        {
            int number;
            Program p = new Program();
            Console.Write("Number:");
            number = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Your number is: {0}", p.ChekcNumber(number));
            Console.WriteLine("Is positve:{0}", p.IsPositiveNumber(number));
            Console.ReadKey();
        }
    }
}
