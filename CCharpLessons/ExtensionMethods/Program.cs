using System;

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
    public class CAppUtil : IMyObject
    {
        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsVisible()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetRobotName()
        {
            return "R1";
        }
        #endregion
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

    //Extension methods for Util class
    public static class CommandWrappers
    {
        public static string GetRobotNameFromUI(this CAppUtil r)
        {
            if (r == null)
            {
                throw new ArgumentNullException(nameof(r));
            }

            return "R2";
        }

        public static string GetRobotFromId(this CAppUtil r, string id)
        {
            return "Param : " + id;
        }
    }    

    //Extension methods for string
    //Note: extension class must be placed in same name-space
    public static class StringHelper
    {
        public static bool IsCapitalized(this string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            return char.IsUpper(s[0]);
        }
    }

    /// <summary>
    /// Main program class
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            CAppUtil util = new CAppUtil();

            //extension method
            "id".IsCapitalized();

            //extension method
            util.IsVisible();

            //util.InterfaceExtensionMethod("" , 10);

            //interface extension
            util.CanShow("s");

            Console.WriteLine(util.GetRobotNameFromUI());
            Console.WriteLine(util.GetRobotFromId("test"));

            Console.ReadKey();
        }
    }
}