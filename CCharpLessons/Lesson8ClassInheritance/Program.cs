using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson8ClassInheritance
{
    class ParentClass
    {
        public      string publStr1;
        protected   string protStr1;
        private     string privateStr1;

        public ParentClass()
        {
            Console.WriteLine("Parent Constructor");
            this.privateStr1 = "private:Parent";
            this.protStr1    = "protected:Parent";
            this.publStr1    = "public:Parent";
        }
        public void print()
        {
            Console.WriteLine("~~~~~~~~~~~I am a Parent Class~~~~~~~~~~");
            Console.WriteLine(this.privateStr1);
            Console.WriteLine(this.protStr1);
            Console.WriteLine(this.publStr1);

        }
        ~ParentClass()
        {
            Console.WriteLine("Parent destructor");
        }
    }

    class ChildClass : ParentClass
    {
        public ChildClass()
        {
            Console.WriteLine("Child Constructor");
        }
        public void print()
        {
            Console.WriteLine("---------I am a Child Class-------");
            Console.WriteLine("child inhr protected:{0}",this.protStr1);
            Console.WriteLine("child inhr public:{0}",   this.publStr1);
        }
        ~ChildClass()
        {
            Console.WriteLine("Child destructor");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==================Child class out:====================");
            ChildClass ch1 = new ChildClass();           
            ch1.print();
           // ch1.publStr1;
          
            Console.WriteLine("=================Parent class out:====================");
            ParentClass p1 = new ParentClass();
            p1.print();
            //p1.publStr1;

            //cast
            Console.WriteLine("===============casting: child to parent================");
            ((ParentClass)ch1).print();
            Console.ReadKey();
        }
    }
}
