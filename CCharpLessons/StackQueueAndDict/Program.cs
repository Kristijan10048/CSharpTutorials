using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackQueueAndDict
{
    class Program
    {
        #region Private Static Methods
        /// <summary>
        /// Ilustrate queue 
        /// </summary>
        private static void QueueExample()
        {
            //create new queue FIRST IN LAST OUT  
            var myQueue = new Queue<int>();

            //add items in the queue
            myQueue.Enqueue(1);
            myQueue.Enqueue(2);
            myQueue.Enqueue(3);
            myQueue.Enqueue(4);
            myQueue.Enqueue(5);

            //print the queue
            for (int i = 0; i < myQueue.Count; i++)
                Console.WriteLine(myQueue.ElementAt<int>(i));

            int tmpVal = myQueue.Dequeue();

            Console.WriteLine("Dequeue {0}", tmpVal);

        }

        /// <summary>
        /// Illustrate stack
        /// </summary>
        private static void StackExample()
        {
            var myStack = new Stack<int>();

            //add items on the stack
            myStack.Push(1);
            myStack.Push(2);
            myStack.Push(3);
            myStack.Push(4);

            //prit stack content
            for (int i = 0; i < myStack.Count; i++)
                Console.WriteLine(myStack.ElementAt<int>(i));

            //get element from stack
            int tmp = myStack.Pop();

            //print the value
            Console.WriteLine("Stack value: {0}", tmp);
        }

        /// <summary>
        /// Ilustrate dictionary
        /// </summary>
        private static void DictionaryExample()
        {
            var myDic = new Dictionary<int, string>();

            string[] names = {"Test",
                             "Testovski",
                              "Michale",
                              "Konan"};

            // add names in to the hash table. 
            //  -Key: hash code
            //  -Value: String
            for (int i = 0; i < names.Length; i++)
                myDic.Add(names[i].GetHashCode(), names[i]);

            foreach (string n in names)
                Console.WriteLine(n);
        }
        #endregion

        static void Main(string[] args)
        {
            //run queue example
            QueueExample();

            //run stack example
            StackExample();

            //run dictionary (hash table)
            DictionaryExample();

            Console.ReadKey();
        }
    }
}