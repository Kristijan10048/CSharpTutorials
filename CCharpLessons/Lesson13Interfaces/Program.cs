using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson13Interfaces
{
    interface IParentInterface
    {
        void ParentInterfaceMethod();
        int ParentIntMethod();
    }
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
