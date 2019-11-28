using System;

namespace Lesson13Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    interface IParentInterface
    {
        void ParentInterfaceMethod();
        int ParentIntMethod();
    }

    /// <summary>
    /// 
    /// </summary>
    class InterfaceInplementer : IParentInterface
    {
        public void ParentInterfaceMethod()
        {
            Console.WriteLine("ParentInterfaceMethod Implementation");
        }
        public int ParentIntMethod()
        {
            Console.WriteLine("ParentIntMethod");
            return 0;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            InterfaceInplementer impl = new InterfaceInplementer();
            impl.ParentInterfaceMethod();
            Console.WriteLine("Out:{0}", impl.ParentIntMethod());
            Console.ReadKey();
        }
    }
}
