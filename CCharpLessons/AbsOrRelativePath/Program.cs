using System;
using System.IO;

namespace AbsOrRelativePath
{
    class Program
    {
        /// <summary>
        /// Checks if a path is absolute or not. 
        /// (Only Works if Path is available in local fie system)
        /// </summary>
        /// <param name="path">A path to check</param>
        /// <returns></returns>
        public static bool IsPathAbs(string path)
        {
            if (string.Compare(path.Trim(), Path.GetFullPath(path), true) == 0)
                return true;
            else
                return false;
        }

        static void Main(string[] args)
        {
            string[] paths = {
                @"C:\windows\",
                @"\win32\",
                @"F:\win32\",
                @"LIBS\\MFG",
                @"E:\SYS_ROOT\LIBS " };

            foreach (string path in paths)
            {
                Console.WriteLine("Path: {0} is absolute : {1}", path, IsPathAbs(path));
            }

            Console.ReadKey();
        }
    }
}
