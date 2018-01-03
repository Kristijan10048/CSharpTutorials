using System;
using System.Collections.Generic;
using System.Text;

namespace ClassInheritance
{
    class C4WelParam
    {
        private double weldNumb;
       
        public virtual void Reset()
        {
            weldNumb = 0;
        }
    }

    class C5WelParam : C4WelParam
    {
        double relDist;
        double teachDist;

        public override void  Reset()
        {
 	        base.Reset();
            relDist = 0;
            teachDist = 0;
        }

    }


    class C4Uploader
    {
        private C4WelParam m_c4weldParam;

        public C4WelParam WeldParam
        {
           get {return m_c4weldParam as C4WelParam;}
        }

        public C4Uploader()
        {
            m_c4weldParam = new C4WelParam();
        }
    }

    class C5Uploader : C4Uploader
    {
        private C5WelParam m_c5WeldParam;

        public C5Uploader():base()
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
