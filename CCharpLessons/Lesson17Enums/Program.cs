using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lesson17Enums
{
    public enum Volume: byte
    {
        [Description("Low")]
        Low     = 1,
        [Description("Medium")]
        Medium  = 2,
        [Description("High")]
        High    = 3 
    }
    class Program
    {
        static void Main(string[] args)
        {
            Volume myVolume = Volume.Medium;
            switch (myVolume)
            {
                case Volume.Low:
                    Console.WriteLine("The volume has been turn down!");
                    break;
                case Volume.Medium:
                    Console.WriteLine("The volume is in the middle!");
                    break;
                case Volume.High:
                    Console.WriteLine("The volume has been turned up!!");
                    break;
                default:
                    Console.WriteLine("Invalid volume selection!");
                    break;
            }
            Console.ReadKey();
        }
    }
}
