using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace RegularExpressions
{

    //$100.00 reg: \$\d+\.\d+
    //d* number
    //w+ word 
    //? 0 or 1
    //[] - range
    //S* zero or more spaces

    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        void MatchWordsOnly()
        {
            string txt = "ova e eden test. Uste edna recenica";

            //any word = \w+
            string pattern = "\\w+";                    

            Regex reg1 = new Regex(pattern);
            MatchCollection mColl = reg1.Matches(txt);

            foreach (Match m in mColl)
                Console.WriteLine(m.ToString());

        }

        /// <summary>
        /// 
        /// </summary>
        void MatchDigitsOnly()
        {
            string txt = "ova e eden test.125 Uste12 edna 123recenica 1324";

            //any digit = \w+
            string pattern = "\\d+";

            Regex reg1 = new Regex(pattern);
            MatchCollection mColl = reg1.Matches(txt);

            foreach (Match m in mColl)
                Console.WriteLine(m.ToString());

        }

        /// <summary>
        /// 
        /// </summary>
        void MatchTeachDistExp()
        {
            string txt = "ML Spot, Target, Speed, Tool, Frame, ToolMap, Spotdata, /Zone, /Load, /Orientation,/TeachDist( 20.01 ) /TeachDist(10.01), /RelDist(1.001), /Re_Open(xx)";

            //any digit = \w+
            string patternTd = @"/TeachDist\(\s*(\d+[.]?\d*)\s*\)\s*";
            string patternRd = @"/RelDist\(\s*(\d+[.]?\d*)\s*\)\s*";

            Regex reg1 = new Regex(patternTd);
            MatchCollection mc = reg1.Matches(txt);

            foreach (Match m in mc)
            {                
                Console.WriteLine("Dist command:{0}", m.ToString());
                //Console.WriteLine("")
                foreach(Group g in m.Groups)
                    Console.WriteLine("Value: {0}", g.Captures[0]);
            }

            Regex reg2 = new Regex(patternRd);
            MatchCollection mc1 = reg2.Matches(txt);

            foreach (Match m in mc1)
            {
                Console.WriteLine("Rel command:{0}", m.ToString());
            }
                
        }

        //()Captures the matched subexpression and assigns it a zero-based ordinal number.
        void ParseNumber()
        {
            string test = "f(10)";
            string pattern = @"f\((\d+)\)";

            Regex reg1 = new Regex(pattern);
            MatchCollection mc = reg1.Matches(test);
            foreach (Match m in mc)
            {
                Console.WriteLine("Match:{0}", m.ToString());
                for (int i = 0; i < m.Groups.Count; i++ )
                    Console.WriteLine("Group: {0}", m.Groups[i].Value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void parseComma()
        {
            //MC SPOT,
            string text = "wp8, wp9, VJ25, Tool1, F0, Toolmap1, Spot1, /Z=Z75, /L=L0, /O=Euler, /R=TeachDist(10), /R=RelDist(10), /R=Re_Open(10)";
            //remove whitespace
            text = Regex.Replace(text, @"\s*,\s*", ",");

            string pattern = @"\A(\w+),(\w+)";
            Regex rc1 = new Regex(pattern);

           // MatchCollection mc = rc1.

            Match m = rc1.Match(text);

            if (m.Success)
                Console.WriteLine("Matches found!");

            string firstLocationName = m.Groups[1].Value;
            string secondLocationName = m.Groups[2].Value;
            Console.WriteLine("Loc1:{0}", m.Groups[1].Value);
            Console.WriteLine("Loc2:{0}", m.Groups[2].Value);

            string text1 = Regex.Replace(text, pattern, firstLocationName);
            string text2 = Regex.Replace(text, pattern, secondLocationName);

            Console.WriteLine(text);
            Console.WriteLine(text1);
            Console.WriteLine(text2);

        }

        /// <summary>
        /// 
        /// </summary>
        void MatchSignalNextMotionStep()
        {
            string text = "r2_104NextMotionStep";
            string pattern = @"\b\w+NextMotionStep\b";

            Regex reg1 = new Regex(pattern);
            MatchCollection mColl = reg1.Matches(text);

            foreach (Match m in mColl)
                Console.WriteLine(m.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool MatchInMotionSignal()
        {
            string text = "r2_10411testInMotion12test";
            string inMotionPattern = @"\b\w+InMotion\b";
            Regex regInMotion = new Regex(inMotionPattern);
            Match m = regInMotion.Match(text);

            if (m != null && m.Success)
            {
                Console.WriteLine("Match:{0}",m.ToString());
                return true;
            }
            Console.WriteLine("No Match");
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool MatchSlaveMotionsCompleatedSignal()
        {
            string text = "r1_104rwasrSlave`MotionsCompleted";
            string slaveMotionsCompleatedPattern = @"\b\w+SlaveMotionsCompleted\b";
            Regex regSlaveMotionsCompleated = new Regex(slaveMotionsCompleatedPattern);
            Match m = regSlaveMotionsCompleated.Match(text);

            if (m != null && m.Success)
            {
                Console.WriteLine("Match:{0}", m.ToString());
                return true;
            }

            Console.WriteLine("No Match");
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        void MatchXmlBodyTag()
        {
            string txt = "<source> <code>begin ~Body~ end </code> </source>";

            string start = @"\~";
            string stop = @"\~";
            string tag = "Body";

            //Tag format       
            string tmpFMT = "@{0}{1}{2}";
            string C_STR_TAG_REGEX_FMT = string.Concat(start, @"{0}\b.*", stop);

            string pattern = string.Format(C_STR_TAG_REGEX_FMT, tag);

            Regex reg1 = new Regex(pattern);
            MatchCollection mColl = reg1.Matches(txt);

            foreach (Match m in mColl)
                Console.WriteLine(m.ToString());

        }

        /// <summary>
        /// 
        /// </summary>
        void MatchForLoopStaubli()
        {
            Regex C_REGEX_FOR_STEP = new Regex(@"^\s*for\s+(.+)\s*\=\s*(-?\d+)\s+to\s+(-?\d+)\s+step\s+(-?\d+)\b");
         string text = "for i = 90 to -90 step -10";
       
        
         Match m = C_REGEX_FOR_STEP.Match(text);
         if (m != null && m.Success)
         {
             Console.WriteLine("Match:{0}", m.ToString());            
         }

         Console.WriteLine("No Match");       
        }

        /// <summary>
        /// 
        /// </summary>
        void MatchIoSetStaubli()
        {
            Regex C_REGEX_IOSET_SIGNAL_VALUE_CALL = new Regex(@"^(a|s|g)ioSet\s*\(\s*(\w+)\s*\,\s*(\w+)\s*\)\s*", RegexOptions.IgnoreCase);
            string text = "aioSet(test, 12)";
            
            Match m = C_REGEX_IOSET_SIGNAL_VALUE_CALL.Match(text);
            if (m != null && m.Success)
            {
                Console.WriteLine("Match:{0}", m.ToString());
            }

            Console.WriteLine("No Match");
        }

        /// <summary>
        /// 
        /// </summary>
        void MatchFanucPaintTeachDist()
        {

            string text = @"Field: $LNSCH[1].$TEACH_DIST Access: RW: INTEGER = -616134";

            Regex C_REGEX_TEACH_DIST_START_BLOCK = new Regex(@"^Field\:\s*\$LNSCH\[(\d+)\]\.\$TEACH_DIST\s*Access:\s*RW\:\s*INTEGER\s*=\s*([+-]?\d+)\b");

            Match m = C_REGEX_TEACH_DIST_START_BLOCK.Match(text);
            if (m != null && m.Success)
            {
                int pntSch = -12;
                int value = -12;

                Int32.TryParse(m.Groups[1].Value, out pntSch);

                Int32.TryParse(m.Groups[2].Value.ToString(), out value);

                Console.WriteLine("Paint schedule: {0}", pntSch);
                Console.WriteLine("Match:{0}", m.ToString());
            }
            else
                Console.WriteLine("No Match");
        }

        /// <summary>
        /// 
        /// </summary>
        void MatchFanucPaintTrackFrame()
        {

            string text = @"Field: $LNSCH[1].$TRK_FRAME Access: RW: POSITION = ";

            Regex C_REGEX_TRACK_FRAME_START_BLOCK = new Regex(@"^Field\s*\:\s*\$LNSCH\[(\d+)\]\.\$TRK_FRAME\s*Access\s*\:\s*RW\s*\:\s*POSITION\s*=\s*");//\=\s*\b

            Match m = C_REGEX_TRACK_FRAME_START_BLOCK.Match(text);
            if (m != null && m.Success)
            {
                int pntSch = -12;
                int value = -12;

                Int32.TryParse(m.Groups[1].Value, out pntSch);

                Int32.TryParse(m.Groups[2].Value.ToString(), out value);

                Console.WriteLine("Paint schedule: {0}", pntSch);
                Console.WriteLine("Match:{0}", m.ToString());
            }
            else
                Console.WriteLine("No Match");
        }

        //void MatchFanucPaintTrackFrame()
        //{

        //    string text = @"Field: $LNSCH[1].$TRK_FRAME Access: RW: POSITION = ";

        //    Regex C_REGEX_TRACK_FRAME_START_BLOCK = new Regex(@"^Field\s*\:\s*\$LNSCH\[(\d+)\]\.\$TRK_FRAME\s*Access\s*\:\s*RW\s*\:\s*POSITION\s*=\s*");//\=\s*\b

        //    Match m = C_REGEX_TRACK_FRAME_START_BLOCK.Match(text);
        //    if (m != null && m.Success)
        //    {
        //        int pntSch = -12;
        //        int value = -12;

        //        Int32.TryParse(m.Groups[1].Value, out pntSch);

        //        Int32.TryParse(m.Groups[2].Value.ToString(), out value);

        //        Console.WriteLine("Paint schedule: {0}", pntSch);
        //        Console.WriteLine("Match:{0}", m.ToString());
        //    }
        //    else
        //        Console.WriteLine("No Match");
        //}

        void MatchFanucPaintTrackUserFrame()
        {

            string text = @"Field: $LNSCH[1].$TRK_UFRAME Access: RW: POSITION = ";

            Regex C_REGEX_TRACK_FRAME_START_BLOCK = new Regex(@"^Field\s*\:\s*\$LNSCH\[(\d+)\]\.\$TRK_UFRAME\s*Access\s*\:\s*RW\s*\:\s*POSITION\s*=\s*");//\=\s*\b

            Match m = C_REGEX_TRACK_FRAME_START_BLOCK.Match(text);
            if (m != null && m.Success)
            {
                int pntSch = -12;               

                Int32.TryParse(m.Groups[1].Value, out pntSch);              

                Console.WriteLine("Paint schedule: {0}", pntSch);
                Console.WriteLine("Match:{0}", m.ToString());
            }
            else
                Console.WriteLine("No Match");
        }

        /// <summary>
        /// 
        /// </summary>
        void MatchFanucPaintFrameData()
        {

            string text = @"Group: 1   Config: F U T, 1, 2,3
                           X:     0.000   Y:     6.848   Z:   -15.111
                           W:      -1.046   P:      2.177   R:  -7.000";

            string test1 = "W:      111.506   P:      .383   R:   -.929";

            //group line mathc
            Regex C_REGEX_TRACK_FRAME_GROUP_BLOCK = new Regex(@"^Group\s*\:\s*(\d+)\s*Config\:\s*(\w)\s*(\w)\s*(\w)\s*\,\s*(\d+)\s*\,\s*(\d+)\s*\,\s*(\d)\s*");

            //translation line match
            Regex C_REGEX_FRAME_TRANSL_BLOCK = new Regex(@"\s*X\s*\:\s*([+-]?\d+(\.\d+)?)\s*Y\s*\:\s*([+-]?\d+(\.\d+)?)\s*Z\s*\:\s*([+-]?\d+(\.\d+)?)\s*");

            //rotation frame block
            Regex C_REGEX_FRAME_ROT_BLOCK = new Regex(@"\s*W\s*\:\s*([+-]?\d*(\.\d+))\s*P\s*\:\s*([+-]?\d*(\.\d+))\s*R\s*\:\s*([+-]?\d*(\.\d+))\s*"); //([+-]?[\d+]?(\.\d+))\s*

            // group paramter line
            Match m = C_REGEX_TRACK_FRAME_GROUP_BLOCK.Match(text);
            if (m != null && m.Success)
            {
                int pntSch = -12;

                Console.WriteLine("Group conf 1 : {0}", m.Groups[2].Value);
                Console.WriteLine("Group conf 2 : {0}", m.Groups[3].Value);
                Console.WriteLine("Group conf 3 : {0}", m.Groups[4].Value);

                Console.WriteLine("Group conf 4 : {0}", m.Groups[5].Value);
                Console.WriteLine("Group conf 5 : {0}", m.Groups[6].Value);
                Console.WriteLine("Group conf 6 : {0}", m.Groups[7].Value);

                Console.WriteLine("Paint schedule: {0}", pntSch);
                Console.WriteLine("Match:{0}", m.ToString());
            }
            else
                Console.WriteLine("No Match");


            //translation parameter line
            m = C_REGEX_FRAME_TRANSL_BLOCK.Match(text);
            if (m != null && m.Success)
            {

                Console.WriteLine("X : {0}", m.Groups[3].Value);
                Console.WriteLine("Y : {0}", m.Groups[3].Value);
                Console.WriteLine("Z : {0}", m.Groups[4].Value);
                Console.WriteLine("Match:{0}", m.ToString());
            }
            else
                Console.WriteLine("No Match");

            //rot parameter line
            m = C_REGEX_FRAME_ROT_BLOCK.Match(test1);
            if (m != null && m.Success)
            {

                Console.WriteLine("W : {0}", m.Groups[1].Value);
                Console.WriteLine("P : {0}", m.Groups[3].Value);
                Console.WriteLine("R : {0}", m.Groups[5].Value);
                Console.WriteLine("Match:{0}", m.ToString());
            }
            else
                Console.WriteLine("No Match");
        }

        /// <summary>
        /// 
        /// </summary>
        void MatchFanucPaintBounds()
        {
            string text = @"Field: $LNSCH[1].$BOUND1  ARRAY[10] OF REAL
                                [1] = 0.000000e+00
      [2] = 0.000000e+00
      [3] = 0.000000e+00
      [4] = 0.000000e+00
      [5] = 0.000000e+00
      [6] = 0.000000e+00
      [7] = 0.000000e+00
      [8] = 0.000000e+00
      [9] = 0.000000e+00
      [10] = 0.000000e+00
     Field: $LNSCH[1].$BOUND2  ARRAY[10] OF REAL
      [1] = 1.300000e+04
      [2] = 0.000000e+00
      [3] = 0.000000e+00
      [4] = 0.000000e+00
      [5] = 0.000000e+00
      [6] = 0.000000e+00
      [7] = 0.000000e+00
        [8] = 0.000000e+00
        [9] = 0.000000e+00
        [10] = 0.000000e+00";

            Regex C_REGEX_BOUND_BLOCK = new Regex(@"Field\s*\:\s*\$LNSCH\[(\d+)\]\.\$BOUND([1|2])\s*ARRAY\s*\[(\d+)\]\s*OF\s*REAL\s*");

            Regex C_REGEX_BOUND_ELEMENT_BLOCK = new Regex(@"\s*\[\d+\]\s*\=\s*(.)\s*");

            // group paramter line
            Match m = C_REGEX_BOUND_BLOCK.Match(text);
            if (m != null && m.Success)
            {
                int pntSch = -12;

                Int32.TryParse(m.Groups[1].Value, out pntSch);
                Console.WriteLine("Schedule:{0}", pntSch);
                Console.WriteLine("Bound 1 or 2 : {0}", m.Groups[2].Value);
                Console.WriteLine("Length : {0}", m.Groups[3].Value);                

                Console.WriteLine("Paint schedule: {0}", pntSch);
                Console.WriteLine("Match:{0}", m.ToString());
            }
            else
                Console.WriteLine("No Match");

            // group paramter line
            m = C_REGEX_BOUND_ELEMENT_BLOCK.Match(text);

            //C_REGEX_BOUND_ELEMENT_BLOCK.Matches
            if (m != null && m.Success)
            {
                int pntSch = -12;
                Int32.TryParse(m.Groups[1].Value, out pntSch);

                Console.WriteLine("Schedule:{0}", pntSch);

                Console.WriteLine("Index : {0}", m.Groups[1].Value);
                Console.WriteLine("Value : {0}", m.Groups[2].Value);

                Console.WriteLine("Match:{0}", m.ToString());
            }
            else
                Console.WriteLine("No Match");
        }

        /// <summary>
        /// 
        /// </summary>
        void MatchFanucPaintTrackUserFrameFlag()
        {

            string text = @"Field: $LNSCH[2].$USE_TRK_UFM Access: RW: BOOLEAN = FALSE";

            Regex C_REGEX_TRACK_FRAME_START_BLOCK = new Regex(@"^Field\s*\:\s*\$LNSCH\[(\d+)\]\.\$USE_TRK_UFM\s*Access\s*\:\s*RW\s*\:\s*BOOLEAN\s*\=\s*(TRUE|FALSE)\s*");//\=\s*\b

            Match m = C_REGEX_TRACK_FRAME_START_BLOCK.Match(text);
            if (m != null && m.Success)
            {
                int pntSch = -12;

                Int32.TryParse(m.Groups[1].Value, out pntSch);

                Console.WriteLine("Paint schedule: {0}", pntSch);
                Console.WriteLine("Match:{0}", m.ToString());
            }
            else
                Console.WriteLine("No Match");
        }

        static void Main(string[] args)
        {
            //match numbers
            Program p = new Program();

            //p.MatchDigitsOnly();
            //p.MatchTeachDistExp();
            //p.ParseNumber();
            //p.parseComma();
            //p.MatchSignalNextMotionStep();
            //p.MatchInMotionSignal();
            //p.MatchXmlBodyTag();
            //p.MatchSlaveMotionsCompleatedSignal();
            //p.MatchForLoopStaubli();
            //p.MatchIoSetStaubli();
            //p.MatchFanucPaintTeachDist();

            //p.MatchFanucPaintTrackFrame();
            //p.MatchFanucPaintTrackUserFrame();
            //p.MatchFanucPaintBounds();

            //p.MatchFanucPaintFrameData();
            p.MatchFanucPaintTrackUserFrameFlag();

            Console.ReadKey();
        }
    }
}
