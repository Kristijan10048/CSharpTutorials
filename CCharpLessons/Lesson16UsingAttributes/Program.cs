using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Lesson16UsingAttributes
{
    class BasicAttributeDemo
    {
        [Obsolete]
        public void MyFirstdeprecatedMethod()
        {
            Console.WriteLine("Called my first deprecated method !!!");
        }
        [ObsoleteAttribute]
        public void MySecondDeprecatedMethod()
        {
            Console.WriteLine("Called my second deprecated method !!!");
        }

        [Obsolete("you shouldn't use this method anymore")]
        public void MyThirdDeprecedMethod()
        {
            Console.WriteLine("My third deprecated method");
        }
    }

    //Whenever there is ambiguity in how an attribute is applied, 
    //you can add a target specification to ensure the right language element is decorated properly.
    //An attribute that helps ensure assemblies adhere to the Common Language Specification (CLS) 
    //is the CLSCompliantAttribute attribute. The CLS is the set of standards that enable different 
    //.NET languages to communicate.
    //[assembly:CLSCompliant(true)]
    class AttributeTargetDemo
    {
        public void NonClsCompliantMethod(uint nclsParam)
        {
            Console.WriteLine("Called NonClsComplaintMethod");
        }
    }
    class Program
    {
        [DllImport("User32.dll", EntryPoint = "MessageBox")]
        static extern int MessageDialog(int hWnd, string msg, string caption, int msgType); 
        

         // make the program thread safe for COM
        [STAThread]
        static void Main(string[] args)
        {
            BasicAttributeDemo atrDemo = new BasicAttributeDemo();

            atrDemo.MyFirstdeprecatedMethod();
            atrDemo.MySecondDeprecatedMethod();
            atrDemo.MyThirdDeprecedMethod();
            Console.WriteLine("Press any key to call message Dialog!!!!");
            Console.ReadKey();
            MessageDialog(0, "MessageDialog Called!!", "DllImport demo", 0);

            Console.WriteLine("Attribute target!!!");
            AttributeTargetDemo atrTargetDemo = new AttributeTargetDemo();
            atrTargetDemo.NonClsCompliantMethod(0);
            Console.ReadKey();
        }
    }
}