using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExtensionMethods2;

namespace ExtensionMethods
{
    /// <summary>
    /// Generic interface
    /// </summary>
    public interface IMyObject
    {
        bool IsVisible();
    }

    /// <summary>
    /// Application class that extents the generic interface
    /// </summary>
    public class CAppOlpUtil : IMyObject
    {
        public bool IsVisible()
        {
            return true;
        }

        public string GetRobotName()
        {
            return "R1";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class MyObjectHelper
    {
        public static bool CanShow(this IMyObject o, string param)
        {
            return false;
        }
    }

    //Extension methods for OlpUtil class
    public static class CommandWrappers
    {
        public static string GetRobotNameFromUI(this CAppOlpUtil r)
        {
            return "R2";
        }

        public static string GetRobotFromId(this CAppOlpUtil r, string id)
        {
            return "Param : " + id;
        }
    }    

    //Extension methods for string
    //Note: extension class must be placed in same namespace
    public static class StringHelper
    {
        public static bool IsCapitalized(this string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            return char.IsUpper(s[0]);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CAppOlpUtil util = new CAppOlpUtil();

            //extension method
            "id".IsCapitalized();

            //exstension method
            util.IsVisible();

            util.InterfaceExtensionMethod("" , 10);

            //interface extension
            util.CanShow("s");
             


            Console.WriteLine(util.GetRobotNameFromUI());
            Console.WriteLine(util.GetRobotFromId("test"));
            Console.ReadKey();
        }
    }
}