using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson20PartialClasses
{
    partial class Tester
    {
        public void InProgram()
        {
            Console.WriteLine("In Program.cs file");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Tester t = new Tester();
            t.InTestFile();
            t.InProgram();
            Console.ReadKey();
        }
    }
}
