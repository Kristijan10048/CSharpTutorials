using System;
using System.Collections.Generic;
using System.Text;


public delegate int Comparer(object obj1, object obj2);
public delegate void test();

class Name
{
    public string firsName = null;
    public string lastName = null;

    public Name(string first, string last)
    {
        firsName = first;
        lastName = last;
    }

    public static int CompareFirstNames(object name1, object name2)
    {
        string n1 = ((Name)name1).firsName;
        string n2 = ((Name)name2).firsName;
        if (String.Compare(n1, n2) > 0)
            return 1;
        else if (String.Compare(n1, n2) < 0)
            return -1;
        else
            return 0; 
    }

    public static void PrintTest()
    {
        Console.WriteLine("test");
    }


    public override string ToString()
    {
        return firsName + " " + lastName;
    }
}
namespace Lesson14IntroductionToDelegatesAndEvents
{
    class Program
    {
        Name[] names = new Name[5];

        public Program()
        {
            names[0] = new Name("Joe", "Mayo");
            names[1] = new Name("John", "Hancock");
            names[2] = new Name("Jane", "Doe");
            names[3] = new Name("John", "Doe");
            names[4] = new Name("Jack", "Smith");
        }

        public void Sort(Comparer compare)
        {
            object tmp;
            for(int i=0; i<names.Length;i++)
                for(int j=i; j<names.Length;j++)
                    if(compare(names[i], names[j]) > 0)
                    {
                        tmp = names[i];
                        names[i] = names[j];
                        names[j] = (Name)tmp;
                    }
        }

        public void PrintNames()
        {
            foreach (Name n in names)
                Console.WriteLine(n.ToString());
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            // this is the delegate instantiation
            //Name.CompareFirstNames saticka funkcija 
            //pokazuvac kon funkcija = delegate
            Comparer cmp = new Comparer(Name.CompareFirstNames);
            Console.WriteLine("==========Before sort===========");
            p.PrintNames();
            p.Sort(cmp);
            Console.WriteLine("==========After sort===========");
            p.PrintNames();

            Console.WriteLine("==========My delegate tesst===========");
            //pointer declaration
            test t1 = new test(Name.PrintTest);
            //pointer call
            t1();

            EventDemo eventd = new EventDemo();
            //Application.EnableVisualStyles();
           // Application.Run(new EventDemo());
            Console.ReadKey();
            
        }
    }
}
