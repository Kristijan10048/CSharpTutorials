using System;
using System.Collections.Generic;
using System.Text;

namespace Lession18VirtualFunctions
{
    class BaseClass
    {
        public BaseClass()
        {
            Console.WriteLine("****CONSTRUCTOR: BaseClass********\n");
        }
        virtual public void MyFunction()
        {
            Console.WriteLine("Virtual function: BaseClass: MyFunction********\n");
        }
        virtual public void MyFunction1(int a)
        {
            Console.WriteLine("Virtual function: BaseClass: MyFunction1********\n");
            double test = TcpSpeed();
        }

        protected virtual double TcpSpeed()
        {
            return 10;
        }

        protected virtual void GetLocationMotionParameters()
        {
            Console.WriteLine("protected virtual void GetLocationMotionParameters");
        }
        public void TestParameter()
        {
            Console.WriteLine("I'm calling in base class");
            GetLocationMotionParameters();
        }
    }

    class DerivedClass : BaseClass
    {
        public DerivedClass()
        {
            Console.WriteLine("****CONSTRUCTOR: DerivedClass********\n");
        }
        public override void MyFunction()
        {
            Console.WriteLine("Virtual function: DerivedClass: MyFunction********\n");
        }

        public override void MyFunction1(int a)
        {
            double test = TcpSpeed();
            if (a > 1)
                Console.WriteLine("Virtual function: DerivedClass: MyFunction1********\n");
            else
                base.MyFunction1(1);
        }
        protected override double TcpSpeed()
        {
            double b = 50;
            if (b > 10)
                return b;
            else
            return base.TcpSpeed();
        }

        protected override void GetLocationMotionParameters()
        {
            Console.WriteLine("DerivedClass: protected override void GetLocationMotionParameters");
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            //Only if a method is declared virtual, derived classes can override this method 
            //if they are explicitly declared to override the virtual base class method with the override keyword.
            BaseClass a = new BaseClass();
            a.MyFunction();
            a.MyFunction1(1);
            DerivedClass b = new DerivedClass();
            b.MyFunction();
            b.MyFunction1(5);

            BaseClass d = new DerivedClass();

            d.MyFunction();
            d.MyFunction1(2);
            d.TestParameter();
            Console.ReadKey();
        }
    }
}
