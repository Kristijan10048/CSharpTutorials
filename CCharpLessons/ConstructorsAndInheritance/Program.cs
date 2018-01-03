using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstructorsAndInheritance
{
    public class Baseclass
    {
        public int X;
        public Baseclass() { }
        public Baseclass(int x) { this.X = x; }
    }
    public class Subclass : Baseclass { }

    public class B
    {
        int x = 1; // Executes 3rd
        public B(int x)
        {
            // Executes 4th
        }
    }
    public class D : B
    {
        int y = 1; // Executes 1st
        public D(int x)
            : base(x + 1) // Executes 2nd
        {
            // Executes 5th
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
