using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayRemoveInsert
{
    class Program
    {
        /// <summary>
        /// Removes element from array and automatically resize the array
        /// </summary>
        /// <param name="index"></param>
        /// <param name="array"></param>
        public static void RemoveAt(int index, ref char[] array)
        {
            //overwrite element at pos: index
            for (int i = index; i < array.Length - 1; i++)
                array[i] = array[i + 1];

            Array.Resize(ref array, array.Length - 1);
        }

        /// <summary>
        /// Adds element to the array and automatically resize the array
        /// </summary>
        /// <param name="index"></param>
        /// <param name="array"></param>
        /// <param name="element"></param>
        public static void InsertAt(int index, ref char[] array, char element)
        {
            //resize the array for one more element
           // Console.WriteLine("length: {0}", array.Length);
            Array.Resize(ref array, array.Length + 1);
            //Console.WriteLine("length: {0}", array.Length);

            for (int i = array.Length - 1; i > index; i--)
                array[i] = array[i - 1];
          
            array[index] = element;
        }

        /// <summary>
        /// Rounds float numbers
        /// </summary>
        /// <param name="n"></param>
        /// <param name="places"></param>
        /// <returns></returns>
        static float Round(float n, int places)
        {
            int digitPlace = 1;
            float d;
            int i;

            for (i = 1; i <= places; i++)
                digitPlace *= 10;

            /* rescale 123.45678 to 12345.678 */
            d = n * digitPlace;

            /* round off: 12345.678 + 0.5 = 12346.178 -> 12346 */
            i = (int)(d + 0.5);

            /* restore to its original scale: 12346 -> 123.46 */
            d = (float)i / digitPlace;

            return d;
        }

        static void Main(string[] args)
        {
            float val = 975.68123f;
            Console.WriteLine(val);
            Console.WriteLine(Round(val, 2));

            char[] myArray = new char[4];
            myArray[0] = 't';
            myArray[1] = 'e';
            myArray[2] = 's';
            myArray[3] = 't';

            Console.WriteLine("Before delete");
            Console.WriteLine(myArray);

            int insertPos = 4;
            int deletePos = 4;

            //delete element
            Program.RemoveAt(deletePos, ref myArray);
            Console.WriteLine(myArray);

            // insert element
            Program.InsertAt(insertPos, ref myArray, 'x');
            Console.WriteLine(myArray);

            Console.ReadKey();            
        }
    }
}
