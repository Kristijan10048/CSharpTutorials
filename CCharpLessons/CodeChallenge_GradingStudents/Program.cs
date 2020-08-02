using System;
using System.Collections.Generic;

namespace GradingStudents
{
    class Program
    {
        static void DbgPrint(string sLine, params object[] ags)
        {
            //set it to false to turn it off
            bool bDebugMode = true;


            if (bDebugMode)
                Console.WriteLine(sLine, ags);
        }

        static int[] solve(int[] grades)
        {
            // Complete this function       
            List<int> output = new List<int>();
            foreach (int grade in grades)
            {
                int rem = grade % 5;
                int res = grade / 5;

                if (grade < 38)
                {
                    output.Add(grade);
                    continue;
                }

                //increase result
                int tmpVal = res;
                if (rem > 0)
                    tmpVal++;

                int nextMultiple = tmpVal * 5;
                DbgPrint("Grade={0} Result={1} Remainder={2}, nextMultiple = {3}", grade, res, rem, nextMultiple);

                if ((nextMultiple - grade) < 3)
                {
                    //grades[pos] = nextMultiple;
                    output.Add(nextMultiple);
                }
                else
                    output.Add(grade);

            }
            return output.ToArray();
        }

        static void Main(string[] args)
        {

            //int n = Convert.ToInt32(Console.ReadLine());
            //int[] grades = new int[n];
            //for (int grades_i = 0; grades_i < n; grades_i++)
            //{
            //    grades[grades_i] = Convert.ToInt32(Console.ReadLine());
            //}

            int n = 100;
            int[] grades = new int[n];

            for (int i = 0; i < n; i++)
            {
                grades[i] = i;
            }

            int[] result = solve(grades);
            //Console.WriteLine(String.Join("\n", result));
            Console.ReadKey();
        }
    }
}
