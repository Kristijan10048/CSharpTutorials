using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Reflection
{
    /// <summary>
    /// Abstract class applicatuon layer
    /// </summary>
    abstract class ApT1
    {
        abstract public void ToDo();
    }

    /// <summary>
    /// Test1 class
    /// </summary>
    class Test1 : ApT1
    {
        #region Public Members
        public int m_number;
        #endregion

        #region Public Properties
        /// <summary>
        /// Number property
        /// </summary>
        public int Number 
        {
            get { return m_number; }
            set { m_number = value; }
        }
        #endregion

        #region Override Methods
        /// <summary>
        /// 
        /// </summary>
        override public void ToDo()
        {
            Console.WriteLine("Somthing to do");
        }
        #endregion
    }

    /// <summary>
    /// Test2 Class
    /// </summary>
    class Test2
    {
        #region Public Properties
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        #endregion
    }
    
    /// <summary>
    /// Main program class
    /// </summary>
    class Program
    {
        #region Private Static Methods
        /// <summary>
        /// Prints assembly name
        /// </summary>
        private static void PintAssemblyName()
        {
            //get curret assembly
            var assembly = Assembly.GetExecutingAssembly();
            Console.WriteLine("Assembly name is: {0}", assembly.FullName);
        }

        /// <summary>
        /// Prints assembly types
        /// </summary>
        private static void PrintAssemblyTypes()
        {
            var asm = Assembly.GetExecutingAssembly();
            var types = asm.GetTypes();

            foreach (var t in types)
            {
                Console.WriteLine("Type name : {0} Base type :{1}", t.Name, t.BaseType);
                Console.WriteLine("Is abstract :{0}", t.IsAbstract);

                var methods = t.GetMethods();
                foreach (var m in methods)
                    Console.WriteLine("\tMethod name :{0}", m.Name);

                var props = t.GetProperties();
                foreach (var p in props)
                    Console.WriteLine("\tProperty name :{0}", p.Name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void CallMethodsViaReflection()
        {
            // get current assembly
            var asm = Assembly.GetExecutingAssembly();

            //Get 
            var typeT1 = asm.GetType(typeof(ApT1).ToString());

            Console.WriteLine("Type :", typeT1.Name);

            var methods = typeT1.GetMethods();
            foreach (var m in methods)
            {
                Console.WriteLine("\tMethods :{0}", m.Name);
                Console.WriteLine("\tMethod Return parameter :{0}", m.ReturnParameter.ParameterType.ToString());
            }

            var method = typeT1.GetMethod("ToDo");
            Test1 t1 = new Test1();
            //string s = "sdf";
            method.Invoke(t1, null);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Program.PintAssemblyName();
            Program.PrintAssemblyTypes();
            Program.CallMethodsViaReflection();

            //Don't kill comand promt
            Console.ReadKey();
        }
    }
}
