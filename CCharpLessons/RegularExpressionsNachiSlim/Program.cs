using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace RegularExpressionsNachiSlim
{
    class NachiMadaReader
    {
        #region Private Constants
        private static readonly Regex C_REGEX_TOOLS_START_BLOCK = new Regex(@"^\[TOL_USERLOAD(\d+)\]");

        private static readonly Regex C_REGEX_TOOL_NUMBER = new Regex(@"^TOOL\s*=\s*TOOL(\d+)");
        private static readonly Regex C_REGEX_LENGTH = new Regex(@"LENGTH\s*=\s*(.+)");
        private static readonly Regex C_REGEX_ANGLE = new Regex(@"ANGLE\s*=\s*(.+)");
        private static readonly Regex C_REGEX_COG = new Regex(@"CG\s*=\s*(.+)");
        private static readonly Regex C_REGEX_WEIGHT = new Regex(@"WEIGHT\s*=\s*(.+)");
        private static readonly Regex C_REGEX_INERTIA = new Regex(@"J\s*=\s*(.+)");
        private static readonly Regex C_REGEX_MAX_RADIUS = new Regex(@"MAXR\s*=\s*(.+)");
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void MatchToolsStartBlock(string text)
        {
            Match m;
            if ((m = C_REGEX_TOOLS_START_BLOCK.Match(text)).Success)
            {
                string toolNo = m.Groups[1].Value;
                string signalIndex = m.Groups[2].Value;
                string signalValue = m.Groups[3].Value;
                Console.WriteLine("Text:{0}", text);
                Console.WriteLine("Tool no:{0}", toolNo);                
                Console.WriteLine("--------------------------------");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void MatchToolNumber(string text)
        {
            Match m;
            if ((m = C_REGEX_TOOL_NUMBER.Match(text)).Success)
            {
                string toolNo = m.Groups[1].Value;              
                Console.WriteLine("Text:{0}", text);
                Console.WriteLine("Tool no:{0}", toolNo);
                Console.WriteLine("--------------------------------");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void MatchLength(string text)
        {
            Match m;
            if ((m = C_REGEX_LENGTH.Match(text)).Success)
            {
                string value = m.Groups[1].Value;
                Console.WriteLine("Text:{0}", text);
                Console.WriteLine("Value length:{0}", value);
                Console.WriteLine("--------------------------------");
            }
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void MatchAngle(string text)
        {
            Match m;
            if ((m = C_REGEX_ANGLE.Match(text)).Success)
            {
                string value = m.Groups[1].Value;
                Console.WriteLine("Text:{0}", text);
                Console.WriteLine("Value angle:{0}", value);
                Console.WriteLine("--------------------------------");
            }
        } 
            
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void MatchCoG(string text)
        {
            Match m;
            if ((m = C_REGEX_COG.Match(text)).Success)
            {
                string value = m.Groups[1].Value;
                Console.WriteLine("Text:{0}", text);
                Console.WriteLine("Value cg:{0}", value);
                Console.WriteLine("--------------------------------");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void MatchWeight(string text)
        {
            Match m;
            if ((m = C_REGEX_WEIGHT.Match(text)).Success)
            {
                string value = m.Groups[1].Value;
                Console.WriteLine("Text:{0}", text);
                Console.WriteLine("Value weight:{0}", value);
                Console.WriteLine("--------------------------------");
            }
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void MatchInertia(string text)
        {
            Match m;
            if ((m = C_REGEX_INERTIA.Match(text)).Success)
            {
                string value = m.Groups[1].Value;
                Console.WriteLine("Text:{0}", text);
                Console.WriteLine("Value inertia:{0}", value);
                Console.WriteLine("--------------------------------");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void MatchMaxRadius(string text)
        {
            Match m;
            if ((m = C_REGEX_MAX_RADIUS.Match(text)).Success)
            {
                string value = m.Groups[1].Value;
                Console.WriteLine("Text:{0}", text);
                Console.WriteLine("Value max radius:{0}", value);
                Console.WriteLine("--------------------------------");
            }
        }
        #endregion
    }


    class Program
    {
        static void Main(string[] args)
        {
            NachiMadaReader reader = new NachiMadaReader();

            //input signals
            string[] userDataFileNames = {  "[TOL_HEADER]",
                                            "FILENAME= T00TOL01.INI",
                                            "VER= 1.00.00",
                                            "DATE= 2/28/2002",
                                            "EMESS= This is the default file for Tool(Toolcons).",
                                            "JMESS= ƒc[ƒ‹—p‰Šú’lƒtƒ@ƒCƒ‹‚Å‚·B",
                                            "PROTECT= 0x4",    
                                            "[TOL_OPERATION]",
                                            "CARTECIEN= 0",
                                            "[TOL_USERLOAD1]",
                                            "TOOL= TOOL1",
                                            "LENGTH=1.000000, 2.000000, 3.000000",
                                            "ANGLE=4.000000, 5.000000, 6.000000",
                                            "CG=90.0000, 90.0000, 360.000",
                                            "WEIGHT=113.000",
                                            "J=11.0000, 12.0000, 13.0000",
                                            "BFAXIS=0",
                                            "MAXR=11.0000" 
                                         };

            foreach (string t in userDataFileNames)
            {               
                reader.MatchToolsStartBlock(t);
                reader.MatchToolNumber(t);
                reader.MatchLength(t);
                reader.MatchAngle(t);
                reader.MatchCoG(t);
                reader.MatchWeight(t);
                reader.MatchInertia(t);
                reader.MatchMaxRadius(t);
            }
            Console.ReadKey();
        }
    }
}
