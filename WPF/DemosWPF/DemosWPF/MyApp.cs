using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemosWPF
{
    public class CAppMyApp
    {
        #region Private Members
        /// <summary>
        /// Name of what???
        /// </summary>
        private string m_sName;

        /// <summary>
        /// Current time date
        /// </summary>
        private DateTime m_currDate;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public CAppMyApp()
        {
            m_currDate = new DateTime();
            m_sName = string.Empty;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sName"></param>
        public CAppMyApp(string sName)
        {
            this.m_sName = sName;
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Returns current Time
        /// The format is in : 
        /// </summary>
        public DateTime CurrentTime
        {
            get
            {               
                return DateTime.Now;               
            }
           
        }

        /// <summary>
        /// Date time (read/write)
        /// </summary>
        public DateTime DayTimeSaved
        {
            get
            {
                return m_currDate;
            }
            set
            {
                m_currDate = value;
            }
        }
        #endregion
    }
}
