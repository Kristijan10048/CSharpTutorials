using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Reflection
{
    /// <summary>
    /// A small reflection tutorial that demonstrates inspecting and invoking types, methods, and properties at runtime with System.Reflection.
    /// </summary>
    /// <summary>
    /// Abstract base type used to show inheritance and method overriding — the members here are what reflection enumerates and invokes.
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
        /// <summary>
        /// Private backing field for the Number property.
        /// </summary>
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
        /// Overrides ApT1.ToDo and prints a message at runtime; invoked via reflection in CallMethodsViaReflection.
        /// </summary>
        override public void ToDo()
        {
            Console.WriteLine("Something to do");
        }
        #endregion
    }

    /// <summary>
    /// A plain (non-inheriting) class with a simple auto-property, used to show that reflection works on any type.
    /// </summary>
    class Test2
    {
        #region Public Properties
        /// <summary>
        /// Simple auto-property demonstrating property enumeration via reflection.
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
            //get current assembly
            var assembly = Assembly.GetExecutingAssembly();
            Console.WriteLine("Assembly name is: {0}", assembly.FullName);
        }

        /// <summary>
        /// Enumerates every type in the current assembly with GetTypes(). For each type, prints its name and base type, whether it is abstract, plus the names of all its methods and properties.
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
        /// Invokes a method at runtime through reflection. Resolves the ApT1 type from its string representation, lists its methods with return types, then instantiates Test1 and calls ToDo() via Invoke with no arguments.
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
        /// Program entry point that runs the three reflection demos in order: print the assembly name, list all its types, then invoke a method via reflection.
        /// </summary>
        /// <param name="args">Command-line arguments; intentionally unused by this demo.</param>
        static void Main(string[] args)
        {
            Program.PintAssemblyName();
            Program.PrintAssemblyTypes();
            Program.CallMethodsViaReflection();

            //Don't kill command prompt
            Console.ReadKey();
        }
    }
}
