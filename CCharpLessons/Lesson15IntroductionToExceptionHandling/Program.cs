using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lesson15IntroductionToExceptionHandling
{

    public class MyTryCatchDemo
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

        public bool canTryCatch(int iNumb1 = 0, int iNumb2 = 0)
        {
            try
            {
                return iNumb1 == iNumb2;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General exception:\n" + ex.ToString());
                return false;
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Error handling demo");
            MyTryCatchDemo tyDemo = new MyTryCatchDemo();
            tyDemo.Start();
            Console.ReadKey();
        }
    }
}
