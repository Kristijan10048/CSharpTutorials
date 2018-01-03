using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lesson15IntroductionToExceptionHandling
{

    class TryCathcDemo
    {
        public void Start()
        {
            try
            {
                File.OpenRead("nesto");
            }
            catch (FileNotFoundException fnf)
            {

                Console.WriteLine("File not found catch:\n" + fnf.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("General exception:\n" + ex.ToString());
            }
            finally
            {
                Console.WriteLine("finally!!! wuhuuu");
            }

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Error handling demo");
            TryCathcDemo tyDemo = new TryCathcDemo();
            tyDemo.Start();
            Console.ReadKey();
        }
    }
}
