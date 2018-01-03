using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson10Properties
{
    public class Customer
    {
        private int id;
        private string name;
        public void SetName(string n)
        {
            this.name = n;
        }
        public void SetID(int id)
        {
            this.id = id;
        }

        public int GetId()
        {
            return this.id;
        }

        public string GetName()
        {
            return this.name;
        }
    }

    //The practice of accessing field data via methods was good because it supported the object-oriented concept 
    //of encapsulation. For example, if the type of m_id or m_name changed from an int type to byte, calling code
    //would still work. Now the same thing can be accomplished in a much smoother fashion with properties, 
    //as shown in Listing 10-2.

    public class CustomerWithProperties
    {
        private int id;
        private string name;
        public void SetName(string n)
        {
            this.name = n;
        }
        public int ID
        {
           get { return id; }
           set { this.id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

    class CustomerWithPropertiesReadOnly
    {
        private int id;
        private string name;
        private int pin;
        private int mySecret;
        public CustomerWithPropertiesReadOnly()
        {
            pin = 1234;
        }
        public void SetName(string n)
        {
            this.name = n;
        }
        public int ID
        {
            get { return id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //read only
        public int Pin
        {
            get { return pin; }
        }

        public int secret
        {
            set { mySecret = value; }
        }


       // public int IDv2 { get; set; } 
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------Customer--------------");
            Customer c1 = new Customer();
            c1.SetID(1);
            c1.SetName("Test testovski"); 
            Console.WriteLine("Name:{0}\nId:{1}", c1.GetName(), c1.GetId());

            Console.WriteLine("------------Customer with properties--------------");
            CustomerWithProperties c2 = new CustomerWithProperties();
            c2.ID = 10;
            c2.Name ="Nekoj";
            Console.WriteLine("Name:{0}\nId:{1}", c2.Name, c2.ID);
            
            Console.WriteLine("------------Customer with properties read only--------------");
            CustomerWithPropertiesReadOnly c3 = new CustomerWithPropertiesReadOnly();
            c3.ID = 10;
            c3.Name = "Smotle";
            c3.secret  = 5665;
            Console.WriteLine("Name:{0}\nId:{1}\nPin:{2}", c3.Name, c3.ID,c3.Pin);
            Console.ReadKey();
        }
    }
}
