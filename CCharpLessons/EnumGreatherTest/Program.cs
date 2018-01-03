using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;



namespace EnumGreatherTest
{

    class Program
    {
        public enum EnumComauPdlLibControllerVersion
        {
            
            [Description("C3G")] 
            C3G = 0,
            [Description("C4G")]
            C4G = 1,
            [Description("C5G")]
            C5G = 2,
        }

        static void Main(string[] args)
        {
            EnumComauPdlLibControllerVersion version = EnumComauPdlLibControllerVersion.C5G;
            if (version >= EnumComauPdlLibControllerVersion.C4G)
                Console.WriteLine("Version >=C4G");
            else if(version <= EnumComauPdlLibControllerVersion.C3G)
                Console.WriteLine("Version <=C3G");
            Console.ReadKey();
        }
    }
}
