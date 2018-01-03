using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace Lesson19HybridDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            HybridDictionary hb = new HybridDictionary();

            hb.Add("M", "Male");
            hb.Add("S", "Scot");
            hb.Add("F", "Female");

            Console.WriteLine("M:{0}", hb["S"]);

            Console.ReadKey();
        }
    }
}
