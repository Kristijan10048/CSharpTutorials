using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemosWPF
{
    public class CAppPerson : CAppMyApp
    {
        #region Private Members
        private string m_sFirstName;
        private string m_sLastName;

        private string m_sAddress;
        private string m_sCountry;
        private string m_sCity;
        #endregion

        #region Constructors

        #endregion

        #region Public Properties
        /// <summary>
        /// 
        /// </summary>
        public string FirstName
        {
            get { return m_sFirstName; }
            set { m_sFirstName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string LastName
        {
            get { return m_sLastName; }
            set { m_sLastName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            get { return m_sAddress; }
            set { m_sAddress = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Country
        {
            get { return m_sCountry; }
            set { m_sCountry = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string City
        {
            get { return m_sCity; }
            set { m_sCity = value; }
        }
        #endregion
    }
}
