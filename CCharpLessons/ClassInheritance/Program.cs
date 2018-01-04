using System;
using System.Collections.Generic;
using System.Text;

namespace ClassInheritance
{
    /// <summary>
    /// Base class
    /// </summary>
    class C4WelParam
    {
        private int m_ProgramNumber;

        /// <summary>
        /// Virtual method used to reset all parameters of derived classes
        /// </summary>
        public virtual void Reset()
        {
            m_ProgramNumber = 0;
        }

        #region Public Properties
        /// <summary>
        /// Program number
        /// </summary>
        public int ProgramNumber
        {
            get { return m_ProgramNumber; }
            set { m_ProgramNumber = value; }
        }
        #endregion
    }

    class C5WelParam : C4WelParam
    {
        #region Private Members
        private  double m_relDist;
        private  double m_teachDist;
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public C5WelParam()
        {
            m_relDist = 0;
            m_teachDist = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_relDist = 0;
            m_teachDist = 0;
        }

        /// <summary>
        /// A method that uses internal parameters to do some calculations
        /// </summary>
        public void DoSomeMath()
        {
            double tmpVal = m_relDist + m_teachDist;
        }
    }


    class C4Uploader
    {
        private C4WelParam m_c4weldParam;

        public C4WelParam WeldParam
        {
            get { return m_c4weldParam as C4WelParam; }
        }

        public C4Uploader()
        {
            m_c4weldParam = new C4WelParam();
        }
    }

    class C5Uploader : C4Uploader
    {
        private C5WelParam m_c5WeldParam;

        public C5Uploader() : base()
        {
            m_c5WeldParam = new C5WelParam();
        }

        public void UploadLocation()
        {
            m_c5WeldParam.Reset();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            C5Uploader upload = new C5Uploader();

            upload.UploadLocation();
        }
    }
}
