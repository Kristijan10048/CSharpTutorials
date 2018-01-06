using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemosWPF
{
    public class MyApp
    {
        #region Private Members
        /// <summary>
        /// NAme
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
        public MyApp()
        {
            m_currDate = new DateTime();
            m_sName = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sName"></param>
        public MyApp(string sName)
        {
            this.m_sName = sName;
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Returns current Time
        /// </summary>
        public DateTime CurrentTime
        {
            get
            {               
                return DateTime.Now;               
            }
           
        }

        /// <summary>
        /// Saved time
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
