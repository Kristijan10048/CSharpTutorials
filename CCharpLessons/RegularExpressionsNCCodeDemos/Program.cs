using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;

namespace RegularExpressionsNCCodeDemos
{

    public class OlpParser
    {
        public void ParseTool()
        {
            NumberFormatInfo myInv = NumberFormatInfo.InvariantInfo;

            string text = "TOOL10 : 1 2 -350 6 181 182";
            string toolPattern = @"TOOL(\d+)\s+:\s+(-?\d+)\s+(-?\d+)\s+(-?\d+)\s+(-?\d+)\s+(-?\d+)\s+(-?\d+)"; //\s+(d+)\s+(d+)\s(d+)

            Regex regTool = new Regex(toolPattern);
            Match mTool = regTool.Match(text);
            int toolNumber;
            double x;
            double y;
            double z;
            double Rx;
            double Ry;
            double Rz;
            string s;
            if (mTool.Success)
            {
                Console.WriteLine("Match!!");

                toolNumber = int.Parse(mTool.Groups[1].Value);

                s = mTool.Groups[2].Value;
                x = double.Parse(s, myInv);

                s = mTool.Groups[3].Value;
                y = double.Parse(s, myInv);

                s = mTool.Groups[4].Value;
                z = double.Parse(s, myInv);

                s = mTool.Groups[5].Value;
                Rx = double.Parse(s, myInv);

                s = mTool.Groups[6].Value;
                Ry = double.Parse(s, myInv);

                s = mTool.Groups[7].Value;
                Rz = double.Parse(s, myInv);

                Console.WriteLine("Tool number:{0}", toolNumber);
                Console.WriteLine("X:{0}", x);
                Console.WriteLine("Y:{0}", y);
                Console.WriteLine("Z:{0}", z);

                Console.WriteLine("RX:{0}", Rx);
                Console.WriteLine("RY:{0}", Ry);
                Console.WriteLine("RZ:{0}", Rz);
            }

            toolNumber = 1;

        }

        public void PareseOrgins()
        {
            NumberFormatInfo myInv = NumberFormatInfo.InvariantInfo;

            string text = "G53 : 1 2 3 5 6 7";
            string orignPattern = @"G(\d+)\s+:\s+(-?\d+)\s+(-?\d+)\s+(-?\d+)\s+(-?\d+)\s+(-?\d+)\s+(-?\d+)"; //\s+(d+)\s+(d+)\s(d+)

            Regex regTool = new Regex(orignPattern);
            Match mTool = regTool.Match(text);
            int frameNumber;
            double x;
            double y;
            double z;
            double Rx;
            double Ry;
            double Rz;
            string s;
            if (mTool.Success)
            {
                Console.WriteLine("Match!!");

                frameNumber = int.Parse(mTool.Groups[1].Value);

                s = mTool.Groups[2].Value;
                x = double.Parse(s, myInv);

                s = mTool.Groups[3].Value;
                y = double.Parse(s, myInv);

                s = mTool.Groups[4].Value;
                z = double.Parse(s, myInv);

                s = mTool.Groups[5].Value;
                Rx = double.Parse(s, myInv);

                s = mTool.Groups[6].Value;
                Ry = double.Parse(s, myInv);

                s = mTool.Groups[7].Value;
                Rz = double.Parse(s, myInv);

                Console.WriteLine("Frame number:{0}", frameNumber);
                Console.WriteLine("X:{0}", x);
                Console.WriteLine("Y:{0}", y);
                Console.WriteLine("Z:{0}", z);

                Console.WriteLine("RX:{0}", Rx);
                Console.WriteLine("RY:{0}", Ry);
                Console.WriteLine("RZ:{0}", Rz);
            }          
        }
    }

    class Program
    {
        //key joint name
        //value: pattern
        private HybridDictionary m_jointPatterns = new HybridDictionary();
        //store joint values     
        private HybridDictionary m_prevJointValues = new HybridDictionary();

        private void SetRegExTranslationCoordinatesPattern()
        {
            //parameters
            string line = "X=2386.85 Y=-32.426 Z=-2335.638 A=1180.156 C1-12.576 B11.335";
            string[] jointNames = {"X", "Y", "Z", "A", "B", "C1"};
            string C_STR_LOCATION_PATTERN_FORMAT = @"{0}=?(-?\d+(\.\d+)*)";
            string pattern;
            //
            NumberFormatInfo myInv = NumberFormatInfo.InvariantInfo;

            foreach(string name in jointNames)
            {
                pattern = string.Format(C_STR_LOCATION_PATTERN_FORMAT, name);
                m_jointPatterns.Add(name, pattern);
            }

            Regex regexLocPosParser;
            //print values
            string jointName;           
            foreach (DictionaryEntry entry in m_jointPatterns)
            {                
                jointName = entry.Key as string;
                pattern = entry.Value as string;
                
                Console.WriteLine("key:{0}, pattern:{1}", jointName, pattern);

                regexLocPosParser = new Regex(pattern);
                Match m = regexLocPosParser.Match(line);
                double value = -9999;
                if (m.Success)
                {
                    value = double.Parse(m.Groups[1].Value, myInv);                  
                    Console.WriteLine("Match!!!");
                    Console.WriteLine("Line:{0}", line);
                    Console.WriteLine("Joint:{0}, value{1}", jointName, value);
                    Console.WriteLine("------------------------");
                    line = regexLocPosParser.Replace(line, "");
                }
            }
        }

        private void RemoveNCCodeComments(ref string line)
        {
            string commentsPatternNoN = @"N\d+\((.)*\)(\r\n)?";
            string commentsPattern    = @"\A\((.)*\)(\r\n)?";

            Regex regComments  = new Regex(commentsPattern);
            Regex regCommentsN = new Regex(commentsPatternNoN);          

            //remove comments (text)
            Match mComments = regComments.Match(line);
            if (mComments.Success)
            {
                line = regComments.Replace(line, "");
            }
            else
            {
                //remove comments N<numb> (text)
                Match mCommentsN = regCommentsN.Match(line);
                if (mCommentsN.Success)
                {
                    line = regCommentsN.Replace(line, "");
                }
            }
            
        }

        public int GetProgramNumber(ref string line)
        {
            int programNumber = -999;
            string programNumberPattern = @"%(\d+)(\r\n)?";

            Regex regProgramNumber = new Regex(programNumberPattern);
            Match m = regProgramNumber.Match(line);

            if (m.Success)
            {
                programNumber = Int32.Parse(m.Groups[1].Value);
                line = regProgramNumber.Replace(line, "");               
            }
            return programNumber;
        }

        public void GetMotion(ref string line)
        {
            //motion
            string motionPTPPattern      = @"G00";
            string motionLINPattern      = @"G01";
            string motionCIRC_CWPattern  = @"G02";
            string motionCIRC_CCWPattern = @"G03";

            Regex regMotionPTP      = new Regex(motionPTPPattern);
            Regex regMotionLIN      = new Regex(motionLINPattern);
            Regex regMotionCIRC_CW  = new Regex(motionCIRC_CWPattern);
            Regex regMotionCIRC_CCW = new Regex(motionCIRC_CCWPattern);

            if (regMotionPTP.Match(line).Success)
            {
                Console.WriteLine("Motion: PTP");
                line = regMotionPTP.Replace(line, "");
            }
            else if (regMotionLIN.Match(line).Success)
            {
                Console.WriteLine("Motion: LINEAR");
                line = regMotionLIN.Replace(line,"");
            }
            else if (regMotionCIRC_CW.Match(line).Success)
            {
                Console.WriteLine("Motion: Circular CW");
                line = regMotionCIRC_CW.Replace(line, "");
            }
            else if (regMotionCIRC_CCW.Match(line).Success)
            {
                Console.WriteLine("Motion: Circular CCW");
                line = regMotionCIRC_CW.Replace(line,"");
            }            
        }

        public void GetCordinateMode(ref string line)
        {
            string absModePattern = @"G90";
            string incModePattern = @"G91";

            Regex regAbsMode = new Regex(absModePattern);
            Regex regIncMode = new Regex(incModePattern);

            if (regAbsMode.Match(line).Success)
            {                
                Console.WriteLine("Cordinate mode: ABSOLUTE");
                line = regAbsMode.Replace(line, "");
            }
            else if (regIncMode.Match(line).Success)
            {
                Console.WriteLine("Cordinate mode: INCREMENTAL");
                line = regAbsMode.Replace(line, "");
            }            
        }

        public void GetLocPositionAndOrientation(ref string line)
        {
            string posXPattern      = @"X(-?\d+(\.\d+)*)";
            string posYPattern      = @"Y(-?\d+(\.\d+)*)";
            string posZPattern      = @"Z(-?\d+(\.\d+)*)";

            string orientAPattern   = @"A(-?\d+(\.\d+)*)";
            string orientBPattern   = @"B(-?\d+(\.\d+)*)";
            string orientCPattern   = @"C(-?\d+(\.\d+)*)";

            //string orientationPattern = @"(A\d+(\.\d+)*(B\d+(\.\d+)*(C\d+(\.\d+)*)?)?)?";

            double x = -9999;
            double y = -9999;
            double z = -9999;

            double a = -9999;
            double b = -9999;
            double c = -9999;

            Regex regPositionX = new Regex(posXPattern);
            Regex regPositionY = new Regex(posYPattern);
            Regex regPositionZ = new Regex(posZPattern);

            Regex regOrientationA = new Regex(orientAPattern);
            Regex regOrientationB = new Regex(orientBPattern);
            Regex regOrientationC = new Regex(orientCPattern);
            
            NumberFormatInfo myInv = NumberFormatInfo.InvariantInfo;

            Match mXpos = regPositionX.Match(line);            
            if(mXpos.Success)
            {
                string s = mXpos.Groups[1].Value;
                x = double.Parse(s, myInv);                
                Console.WriteLine("Position X:{0}", x);
                line = regPositionX.Replace(line, ""); //line.Substring(mXpos.Index + mXpos.Length);
            }

            Match mYPos = regPositionY.Match(line);
            if (mYPos.Success)
            {
                y = double.Parse(mYPos.Groups[1].Value, myInv);
                Console.WriteLine("Position Y:{0}", y);
                line = regPositionY.Replace(line, ""); //line.Substring(mYPos.Index + mYPos.Length);
            }

            Match mZPos = regPositionZ.Match(line);
            if (mZPos.Success)
            {
                string s = mZPos.Groups[1].Value;
                z = double.Parse(s, myInv);
                Console.WriteLine("Position Z:{0}", z);
                line = regPositionZ.Replace(line, ""); //line.Substring(mZPos.Index + mZPos.Length);
            }

            Match mAOrientation = regOrientationA.Match(line);
            if (mAOrientation.Success && (line != ""))
            {
                string s = mAOrientation.Groups[1].Value;
                a = double.Parse(s, myInv);
                Console.WriteLine("Orientation A:{0}", a);
                line = regOrientationA.Replace(line, ""); //line.Substring(mAOrientation.Index + mAOrientation.Length);
            }

            Match mBOrientation = regOrientationB.Match(line);
            if (mBOrientation.Success && (line != ""))
            {
                string s = mBOrientation.Groups[1].Value;
                b = double.Parse(s, myInv);
                Console.WriteLine("Orientation B:{0}", b);
                line = line.Substring(mBOrientation.Index + mBOrientation.Length);//regOrientationB.Replace(line, "");
            }

            Match mCOrientation = regOrientationC.Match(line);
            if (mCOrientation.Success && (line != ""))
            {
                string s = mCOrientation.Groups[1].Value;
                c = double.Parse(s, myInv);
                Console.WriteLine("Orientation C:{0}", c);
                line = line.Substring(mCOrientation.Index + mCOrientation.Length);//regOrientationC.Replace(line, "");
            }
        }
        
        public void GetLineNumber(ref string line)
        {
            string lineNumberPattern = @"N(\d+)";
            Regex regLineNumb = new Regex(lineNumberPattern);
            Match m = regLineNumb.Match(line);
            if (m.Success)
            {
                Console.WriteLine("LineNumb:{0}",m.Groups[1].Value);
                line = regLineNumb.Replace(line, "");                
            }
        }
        
        public void GetOrignFrame(ref string line)
        {
            string oringFramePattern  = @"G(5[3-9])";

            NumberFormatInfo myInv = NumberFormatInfo.InvariantInfo;
            Regex regOrignFrame = new Regex(oringFramePattern);

            Match mOrignFrame = regOrignFrame.Match(line);
            if (mOrignFrame.Success)
            {
                string s = mOrignFrame.Groups[1].Value;
                int oringID = int.Parse(s);
                switch (oringID)
                {
                    case 53:
                        Console.WriteLine("Orign: Rss World frame!");
                        break;
                    case 54:
                    case 55:
                    case 56:
                    case 57:
                    case 58:
                    case 59:
                        Console.WriteLine("Orign: Base frame");
                        break;
                }
            }
        }
        
        public void GetZone(ref string line)
        {
            string zoneFinePattern      = "G09";
            string zoneDistPattern      = "DIST";
            string zoneMinPattern       = "MIN";
            string zoneMaxPattern       = "MAX";
            string zoneNoDecelPattern   = "NO_DECEL";

            Regex regZoneFine    = new Regex(zoneFinePattern);
            Regex regZoneDist    = new Regex(zoneDistPattern);
            Regex regZoneMin     = new Regex(zoneMinPattern);
            Regex regZoneMax     = new Regex(zoneMaxPattern);
            Regex regZoneNoDecel = new Regex(zoneNoDecelPattern);

            Match mZoneFine = regZoneFine.Match(line);
            if (mZoneFine.Success)
            {
                Console.WriteLine("Zone: FINE-G09");
                line = regZoneFine.Replace(line, "");
            }

            Match mZoneDist = regZoneDist.Match(line);
            if (mZoneDist.Success)
            {
                Console.WriteLine("Zone: dist");
                regZoneDist.Replace(line, "");
            }

            Match mZoneMin = regZoneMin.Match(line);
            if (mZoneMin.Success)
            {
                Console.WriteLine("Zone: min");
                line = regZoneDist.Replace(line, "");
            }

            Match mZoneMax = regZoneMax.Match(line);
            if (mZoneMax.Success)
            {
                Console.WriteLine("Zone: dist");
                line = regZoneDist.Replace(line, "");
            }

            Match mZoneNoDecel = regZoneNoDecel.Match(line);
            if (mZoneDist.Success)
            {
                Console.WriteLine("Zone: no decel");
                line = regZoneNoDecel.Replace(line, "");
            }
        }

        public void paraseProgramLine(string line)
        {
            GetLineNumber(ref line);
            GetOrignFrame(ref line);
            GetZone(ref line);
            GetMotion(ref line);
            GetCordinateMode(ref line);
            GetLocPositionAndOrientation(ref line);
        }

        static void Main(string[] args)
        {
            StreamReader file   = new StreamReader("..\\..\\test.si");
            string buffer       = file.ReadToEnd();
            file.Close();                       

            Program p = new Program();         

            string[] lines = buffer.Split('\n');
            foreach(string line in lines)
            {
                Console.WriteLine("Line:{0}", line);
                //to uppercase
                string tmpLine = line.ToUpper();
                //remove whitespaces
                tmpLine = Regex.Replace(tmpLine, @"\s", "", RegexOptions.Multiline);

                //remove comments in line
                p.RemoveNCCodeComments(ref tmpLine);
                if (tmpLine != "")
                {
                    p.GetProgramNumber(ref tmpLine);
                    if (tmpLine != "")
                        p.paraseProgramLine(tmpLine);
                }
                Console.WriteLine("---------------------------------");
            }

            OlpParser olp = new OlpParser();
            olp.PareseOrgins();

            p.SetRegExTranslationCoordinatesPattern();

            Console.ReadKey();
        }
    }
}