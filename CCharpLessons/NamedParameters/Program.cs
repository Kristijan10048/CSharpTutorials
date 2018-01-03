using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NamedParameters
{
    public class Person
    {
        #region Constructors/Destructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sFirstName"></param>
        /// <param name="sLastName"></param>
        /// <param name="sAddress"></param>
        public Person(string sFirstName, string sLastName, string sAddress)
        {
            this.FirstName = sFirstName;
            this.LastName = sLastName;
            this.Address = sAddress;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Address { get; set; }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {

            Person p = new Person(sFirstName: "John",
                                  sLastName: "Doe",
                                  sAddress: "Unknown");
        }
    }
}
