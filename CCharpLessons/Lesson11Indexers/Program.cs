using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson11Indexers
{

    //Indexers allow your class to be used just like an array. On the inside of a class, you manage a collection of
    //values any way you want. These objects could be a finite set of class members, another array, 
    //or some complex data structure. 
    class IntIndexer
    {
        private string[] myData;

        public IntIndexer(int size)
        {
            myData = new string[size];
            for (int i=0; i<size; i++)
                myData[i] = "empty";
        }
        public string this[int pos]
        {
            get
            {
                return myData[pos];
            }
            set
            {
                myData[pos] = value;
            }
        }

    }
    //Overloaded Indexers: OvrIndexer.cs


    class OvrIndexer
    {
        private string[] myData;
        private uint arrSize;

        public OvrIndexer(uint size)
        {
            arrSize = size;
            myData = new string[size];
            for (int i = 0; i < size; i++)
                myData[i] = "empty";

        }

        public string this[uint pos]
        {
            get
            {
                return myData[pos];
            }
            set
            {
                myData[pos] = value;
            }
        }

        public string this[string data]
        {
            get
            {
                uint count = 0;
                for(int i=0; i<arrSize; i++)
                    count++;
                return count.ToString();
            }
            set
            {
                for (int i = 0; i < arrSize; i++)
                    if (myData[i] == data)
                        myData[i] = value;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------Indexers -------------");
            int size = 10;
            IntIndexer myInd = new IntIndexer(size);
            myInd[9] = "Value";
            for (int i = 0; i < size; i++)
                Console.WriteLine(myInd[i]);

            Console.WriteLine("-------------Indexers overloaded-------------");
            uint usize = 10;
            OvrIndexer myOvrInd = new OvrIndexer(usize);
            Console.WriteLine("Before adding data:");
            for (uint i = 0; i < size; i++)
                Console.WriteLine(myOvrInd[i]);

            myOvrInd[9] = "tralala";
            myOvrInd["empty"] = "Zvuci na iljada violini";

            Console.WriteLine("After adding data:");
            for (uint i = 0; i < size; i++)
                Console.WriteLine(myOvrInd[i]);
            Console.ReadKey();
        }
    }
}
