using System;

namespace ExtensionMethods
{
    /// <summary>
    /// Generic interface that exposes visibility information for an object.
    /// </summary>
    public interface IMyObject
    {
        bool IsVisible();
    }

    /// <summary>
    /// Concrete implementation of IMyObject that provides robot-related operations.
    /// </summary>
    public class CAppUtil : IMyObject
    {
        #region Public Methods
        /// <summary>
        /// Determines whether the object is currently visible.
        /// </summary>
        /// <returns><c>true</c> if the object is visible; otherwise, <c>false</c>.</returns>
        public bool IsVisible()
        {
            return true;
        }

        /// <summary>
        /// Gets the name of the robot managed by this utility.
        /// </summary>
        /// <returns>The robot name (e.g. "R1").</returns>
        public string GetRobotName()
        {
            return "R1";
        }
        #endregion
    }

    /// <summary>
    /// Provides extension methods for the IMyObject interface.
    /// </summary>
    public static class MyObjectHelper
    {
        /// <summary>
        /// Determines whether the object can be shown for the given parameter.
        /// </summary>
        /// <param name="o">The object to evaluate.</param>
        /// <param name="param">A parameter that influences the visibility decision.</param>
        /// <returns><c>true</c> if the object can be shown; otherwise, <c>false</c>.</returns>
        public static bool CanShow(this IMyObject o, string param)
        {
            return false;
        }
    }

    //Extension methods for Util class
    /// <summary>
    /// Provides extension methods for the CAppUtil class.
    /// </summary>
    public static class CommandWrappers
    {
        /// <summary>
        /// Gets the robot name as displayed in the user interface.
        /// </summary>
        /// <param name="r">The utility instance to extend.</param>
        /// <returns>The UI robot name (e.g. "R2").</returns>
        public static string GetRobotNameFromUI(this CAppUtil r)
        {
            if (r == null)
            {
                throw new ArgumentNullException(nameof(r));
            }

            return "R2";
        }

        /// <summary>
        /// Gets a label for the robot identified by the given id.
        /// </summary>
        /// <param name="r">The utility instance to extend.</param>
        /// <param name="id">The robot identifier.</param>
        /// <returns>A string of the form "Param : {id}".</returns>
        public static string GetRobotFromId(this CAppUtil r, string id)
        {
            return "Param : " + id;
        }
    }    

    //Extension methods for string
    //Note: extension class must be placed in same name-space
    /// <summary>
    /// Provides extension methods for the string type.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Determines whether the first character of the string is uppercase.
        /// </summary>
        /// <param name="s">The string to evaluate.</param>
        /// <returns><c>true</c> if the first character is an uppercase letter; otherwise, <c>false</c>.</returns>
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