using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indexers
{
    class Data
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the underlying double value. </summary>
        ///
        /// <value> The underlying double value. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        double Value { get; set; }

        private string m_text = "Gets or sets the value";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the character at the specified zero-based index of the stored text. </summary>
        ///
        /// <param name="index">    Zero-based index of the entry to access. </param>
        ///
        /// <returns>   The indexed item. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public char this[int index]
        {
            get { return m_text[index]; }
            //set { m_text[index] = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the number of characters in the stored text. </summary>
        ///
        /// <value> The length. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public int Length
        {
           get {return m_text.Length;}
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Data myData = new Data();


            for (int idx = 0; idx < myData.Length; idx++)
            {
                Console.Write(myData[idx]);
            }
            Console.ReadKey();
        }

    }
}
