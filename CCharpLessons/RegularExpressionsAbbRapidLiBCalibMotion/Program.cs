using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;

namespace RegularExpressionsAbbRapidLiBCalibMotion
{
    class Program
    {
        public void MatchCalibVia(string text)
        {
            Regex vialoc = new Regex(@"^\s*(Move[JLC]|MoveAbsJ|Calib(SJ|SL))\s*\b", RegexOptions.IgnoreCase); //|SL
            Match m = vialoc.Match(text);
            if (m.Success)
                Console.WriteLine("Match!!!");
            else
                Console.WriteLine("No!!!");
               
        }

        public void MatchPrePiosParamer(string text)
        {
            Regex prepos = new Regex(@"\\PrePos:=(\d+)");
            Match m = prepos.Match(text);
            int prepos_val = 0;
            if (m.Success)
            {
                Console.WriteLine("Match");
                prepos_val = int.Parse(m.Groups[1].Value);
            }
            else
                Console.WriteLine("No");

        }
        static void Main(string[] args)
        {
            Program p = new Program();

            string text = "CalibSJ via5,v5,Gun311\\ToolChg\\PrePos:=1,z1,Gun311TCP\\Wobj:=wobj1\\TLoad:=Load";

            string text1 = "CalibSL via5,v5,Gun311\\ToolChg\\PrePos:=12,z1,Gun311TCP\\Wobj:=wobj1\\TLoad:=Load";

            p.MatchCalibVia(text1);

            //p.MatchPrePiosParamer(text);
        }
    }
}
