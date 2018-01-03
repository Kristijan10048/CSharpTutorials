using System;
using System.Collections.Generic;
using System.Text;

namespace MyNameSpace
{
    class Car
    {
        #region Public Properties
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Number { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        public void Print()
        {
            Console.WriteLine("Brand: {0} \nModel:{1}\nNumber:{2}", Brand, Model, Number);
        }
        #endregion
    }

    namespace Tutorial
    {
        /// <summary>
        /// 
        /// </summary>
        class Lessons
        {
            #region Public Properties
            public string Chapter { get; set; }
            public string Title { get; set; }
            #endregion

            #region Public Methods
            /// <summary>
            /// Prints class properties
            /// </summary>
            public void Print()
            {
                Console.WriteLine("Chapter: {0} \nTitle:{1}", Chapter, Title);
            }
            #endregion;
        }
    }
}