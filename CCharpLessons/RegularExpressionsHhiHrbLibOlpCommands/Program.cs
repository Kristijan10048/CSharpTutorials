using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RegularExpressionsHhiHrbLibOlpCommands
{
    class HhiHrbOlpCommands
    {
        #region Private Constants
        //==========Variable assignation============
        private static readonly Regex C_REGEX_VAR_ASSIGN = new Regex(@"^(L?V\d+(%|!|$))\s*=\s*(.+)\Z", RegexOptions.IgnoreCase);
        //support binary and hexa representation of integer numbers
        private static readonly Regex C_REGEX_BIN_NUMBER = new Regex(@"&B([0-1]+)", RegexOptions.IgnoreCase);
        private static readonly Regex C_REGEX_HEX_NUMBER = new Regex(@"&H([0-9A-F]+)", RegexOptions.IgnoreCase);
        private static readonly Regex C_REGEX_PROGRAM_NO = new Regex(@"([0-9][0-9][0-9][0-9])", RegexOptions.IgnoreCase);
        
        //==========signals============
        //TODO Check adress format
        private static readonly Regex C_REGEX_OUTPUT_SIGNAL = new Regex(@"^(DO|DOB|DOW|DOL|DOF|AO)(\d+)=((\d+(\.\d+)?)|(\&.+))$", RegexOptions.IgnoreCase);
        private static readonly Regex C_REGEX_INPUT_SIGNAL  = new Regex(@"^(DI|DIB|DIW|DIL|DIF|AI)\d?=((\d+(\.\d+)?)|(&.+))", RegexOptions.IgnoreCase);

        //WAIT
        private static readonly Regex C_REGEX_WAIT = new Regex(@"WAIT\s+(.+)\s+(\d+(.\d+)?)\s+(.+)", RegexOptions.IgnoreCase);

        //==========flow control============
        //goto
        private static readonly Regex C_REGEX_GOTO  = new Regex(@"GOTO\s+\t*((\d+)|(S\d+)|(\*\w+))", RegexOptions.IgnoreCase);
        //jump
        private static readonly Regex C_REGEX_JUMP = new Regex(@"JMPP\s+\t*(\d+)",RegexOptions.IgnoreCase);
        //if then
        private static readonly Regex C_REGEX_SIMPLE_IF_THEN = new Regex(@"IF\s+(.+)\s+THEN\s+((\d+|\*\w+|L?V\d+(%|!|$)|CALL|JMPP|GOSUB)(.+))", RegexOptions.IgnoreCase);
        private static readonly Regex C_REGEX_SIMPLE_IF_THEN_ELSE = new Regex(@"IF\s+(.+)\sTHEN$(.+)\s+\t*ELSE\s+(.+)", RegexOptions.IgnoreCase);
        //for
        private static readonly Regex C_REGEX_FOR_NEXT = new Regex(@"^FOR\s+(.+)\s+TO\s+(\d+)\s+STEP\s+(-?\d+)\b", RegexOptions.IgnoreCase);
        private static readonly Regex C_REGEX_NEXT = new Regex(@"^NEXT\b", RegexOptions.IgnoreCase);

        //call
        private static readonly Regex C_REGEX_CALL = new Regex(@"CALL\s+\t*([0-9][0-9][0-9][0-9])(\s+\t*END)?\z", RegexOptions.IgnoreCase);
        private static readonly Regex C_REGEX_DELAY = new Regex(@"DELAY\s\t*(\d+(.\d+)?)", RegexOptions.IgnoreCase);

        //variable
        private static readonly Regex C_REGEX_VARIABLE = new Regex(@"(L)?V(\d+)(%|!|$)", RegexOptions.IgnoreCase);

        //cls upload
        private Regex C_REGEX_SPEED_PROE = new Regex(@"^FEDRAT\s*\/\s*(\d+(\.\d+)?)\s*\,\s*MMPM$");

        /// <summary>
        /// register assignation
        /// </summary>
        private static readonly Regex C_REGEX_FREQ_REGISTER = new Regex(@"(_RN\[?(\d+)\]?)\s*=\s*(.+)", RegexOptions.IgnoreCase);
        #endregion

        #region Public Methods
        public void MatchOutputSignal(string text)
        {
            Match m;
            if ((m=C_REGEX_OUTPUT_SIGNAL.Match(text)).Success)
            {
                string signalName = m.Groups[1].Value;
                string signalIndex = m.Groups[2].Value;
                string signalValue = m.Groups[3].Value;
                Console.WriteLine("Output signal:{0}", text);
                Console.WriteLine("signal name:{0}", signalName);
                Console.WriteLine("signal value:{0}", signalValue);
                Console.WriteLine("--------------------------------");
            }
        }

        public void MatchGoTo(string text)
        {
            Match m;
            if ((m=C_REGEX_GOTO.Match(text)).Success)
            {                
                string address = m.Groups[1].Value;
                Console.WriteLine("GO TO:{0}", text);
                Console.WriteLine("Go to address:{0}", address);
                Console.WriteLine("--------------------------------");
            }
        }

        public void MatchJump(string text)
        {
            Match m;
            if ((m=C_REGEX_JUMP.Match(text)).Success)
            {                
                string programNo = m.Groups[1].Value;
                Console.WriteLine("JUMP :{0}", text);
                Console.WriteLine("Jump to programNo :{0}", programNo);
                Console.WriteLine("--------------------------------");
            }
        }

        public void MatchDelay(string text)
        {
            Match m;
            if ((m = C_REGEX_DELAY.Match(text)).Success)
            {
                string delayTime = m.Groups[1].Value;
                Console.WriteLine("DELAY :{0}", text);
                Console.WriteLine("Dalay for {0}", delayTime);
                Console.WriteLine("--------------------------------");
            }
        }

        public void MatchIF(string text)
        {

            Match m;
            if ((m = C_REGEX_SIMPLE_IF_THEN.Match(text)).Success)
            {
                string ifCondition = m.Groups[1].Value;
                string ifBody = m.Groups[2].Value;           
                
                Console.WriteLine("IF: {0}", text);
                Console.WriteLine("If condition: {0}", ifCondition);
                Console.WriteLine("If body: {0}", ifBody);
                Console.WriteLine("--------------------------------");
            }
        }

        public void MatchWait(string text)
        {
            Match m;
            if ((m = C_REGEX_WAIT.Match(text)).Success)
            {
                string condition = m.Groups[1].Value;
                double timeOut = -1;
                try { double.TryParse(m.Groups[2].Value.ToString(), out timeOut); }  catch { }
                string address = m.Groups[4].Value;
                Console.WriteLine("");
                Console.WriteLine("Condition: {0}", condition);
                Console.WriteLine("Timeout: {0}", timeOut);
                Console.WriteLine("Jump addres: {0}", address);
                Console.WriteLine("--------------------------------");
            }
        }

        public void MatchVarAsing(string text)
        {
            Match m;
            if ((m = C_REGEX_VAR_ASSIGN.Match(text)).Success)
            {
                string var = m.Groups[1].Value;               
                string rightSide = m.Groups[2].Value;
                Console.WriteLine("Var assign");
                Console.WriteLine("Left side: {0}", var);
                Console.WriteLine("Right side: {0}", rightSide);               
                Console.WriteLine("--------------------------------");
            }
        }

        public void MatchBinNumb(string text)
        {
            Match m;
            if ((m = C_REGEX_BIN_NUMBER.Match(text)).Success)
            {
                string binVAl = m.Groups[1].Value;
                int numb = -1;

                numb = Convert.ToInt32(binVAl, 2);
                string address = m.Groups[4].Value;
                Console.WriteLine("Bin number:{0}",text);
                Console.WriteLine("Decimal val: {0}", numb);                
                Console.WriteLine("--------------------------------");
            }
        }

        public void MatchProgramNumb(string text)
        {
            Match m;
            if ((m = C_REGEX_PROGRAM_NO.Match(text)).Success)
            {
                string val = m.Groups[1].Value;                       
                Console.WriteLine("Hex number:{0}",val);                           
                Console.WriteLine("--------------------------------");
            }
        }

        public void MatchHexNumb(string text)
        {
            Match m;
            if ((m = C_REGEX_HEX_NUMBER.Match(text)).Success)
            {
                string hexVAl = m.Groups[1].Value;
                int numb = -1;
                numb = Convert.ToInt32(hexVAl, 16);
                Console.WriteLine("Prog number:{0}", text);
                Console.WriteLine("Decimal val: {0}", numb);
                Console.WriteLine("--------------------------------");
            }
        }

        public void MatchCall(string text)
        {
            Match m;
            if ((m = C_REGEX_CALL.Match(text)).Success)
            {
                string programNo = m.Groups[1].Value;
                //int numb = -1;
                //numb = Convert.ToInt32(hexVAl, 16);
                Console.WriteLine("----Call----");
                Console.WriteLine("Prog number:{0}", programNo);
                Console.WriteLine("--------------------------------");
            }
        }

        public void MatchVarName(string text)
        { 
            Match m;
            if ((m = C_REGEX_VARIABLE.Match(text)).Success)
            {
                string varVisib = m.Groups[1].Value;
                string varNo = m.Groups[2].Value;
                string varType = m.Groups[3].Value;
               
                //int numb = -1;
                //numb = Convert.ToInt32(hexVAl, 16);
                Console.WriteLine("----var name----");
                Console.WriteLine("Var name:{0}", text);
                Console.WriteLine("Var visibility:{0}", varVisib);
                Console.WriteLine("Var type:{0}", varType);
                Console.WriteLine("Var number:{0}", varNo);
                Console.WriteLine("--------------------------------");
            }
        }

        public void MathcForNext(string text)
        {
            Match m;
            if ((m = C_REGEX_FOR_NEXT.Match(text)).Success)
            {
                string initVal = m.Groups[1].Value;
                string endVal = m.Groups[2].Value;
                string step = m.Groups[3].Value;

                //int numb = -1;
                //numb = Convert.ToInt32(hexVAl, 16);
                Console.WriteLine("------------for loop------------");
                Console.WriteLine("Init var:{0}", initVal);
                Console.WriteLine("End var:{0}", endVal);
                Console.WriteLine("Step:{0}", step);              
                Console.WriteLine("--------------------------------");
            }
        }

        public void MatchInputSignal(string text)
        {
            Match m;
            if ((m = C_REGEX_INPUT_SIGNAL.Match(text)).Success)
            {
                string signalName = m.Groups[1].Value;
                string signalValue = m.Groups[2].Value;
                Console.WriteLine("Input signal:{0}", text);
                Console.WriteLine("signal name:{0}", signalName);
                Console.WriteLine("signal value:{0}", signalValue);
                Console.WriteLine("--------------------------------");
            }
        }

        public void MatchClsSpeed(string text)
        {
            Match m;
            if ((m = C_REGEX_SPEED_PROE.Match(text)).Success)
            {
                string value = m.Groups[1].Value;                
                Console.WriteLine("Speed value:{0}", value);             
                Console.WriteLine("--------------------------------");
            }
        }

        public void MatchFrequencyRegister(string text)
        {
            Match m;
            Console.WriteLine("---------------Freq reg-----------------");
            Console.WriteLine("Reg:{0}", text);
            if ((m = C_REGEX_FREQ_REGISTER.Match(text)).Success)
            {
                string regVar = m.Groups[1].Value;
                string registerNo = m.Groups[2].Value;
                string value = m.Groups[3].Value;
                Console.WriteLine("Frequency register var:{0}", regVar);
                Console.WriteLine("Frequency register numb:{0}", registerNo);
                Console.WriteLine("Frequency register value:{0}", value);
                Console.WriteLine("--------------------------------");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void MatchTemplateUserDataFormat(string text)
        {
            Match m;
            Console.WriteLine("----------------MatchTemplateUserDataFormat----------------");
            Console.WriteLine("Reg:{0}", text);
            Regex C_REGEX_L_TOOL_USERDATA = new Regex(@"IRC5\sL(\w+)XXX_(\w+)\s-\sUserData.tmod");

            if ((m = C_REGEX_L_TOOL_USERDATA.Match(text)).Success)
            {
                string regVar = m.Groups[1].Value;
                string registerNo = m.Groups[2].Value;
                string value = m.Groups[3].Value;
                Console.WriteLine("Name:{0}", regVar);
                Console.WriteLine("Version:{0}", registerNo);                
                Console.WriteLine("--------------------------------");
            }
        }
        #endregion
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            HhiHrbOlpCommands olp = new HhiHrbOlpCommands();

            ////output signals
            //string[] signals = { "DO1=3", "SO=3", "DOB2=3.1", "DOW2=3.1", "DOB2=3.2", "DOB2=&H75", "AO2=3.4" };
            //foreach (string t in signals)
            //    olp.MatchOutputSignal(t);

            ////go to
            //string[] gotos = { "GOTO 99", "GOTO *ERR", "GOTO S100"};
            //foreach (string t in gotos)
            //    olp.MatchGoTo(t);

            ////jump
            //string[] jumps = {"JMPP 909", "JMP 01", "JMPP 501" };
            //foreach(string j in jumps)
            //    olp.MatchJump(j);

            ////delay
            //string[] delays = {"DELAY 0.5", "DELLAY01","DELAY0.1", "DELAY 500" };
            //foreach (string j in delays)
            //    olp.MatchDelay(j);

            ////delay
            //string[] waits = { "WAIT a=10 0.5 *ERR", "WAIT a=10 0.5 *ERR", "WAIT b=11 0.15 *ERR1", "WAIT a=12 0.25 *ERR2" };
            //foreach (string j in waits)
            //    olp.MatchWait(j);            

            ////bin
            //string[] binno = { "&B101", "&B111", "&B11111111", "&B01", "&B1010" };
            //foreach (string j in binno)
            //    olp.MatchBinNumb(j);

            ////hex
            //string[] hexno = { "&H101", "&HFFF", "&H10FA", "&HFA14", "&Hffff" };
            //foreach (string j in hexno)
            //    olp.MatchHexNumb(j);

            ////vars
            //string[] varAssing = {"LV1%=600", "LV2%=21", "V1%=600", "V2%=21", "V1! = 15.151324", "V2! = 1678.2345" };
            //foreach (string v in varAssing)
            //    olp.MatchVarAsing(v);

            ////program no
            //string[] prognos = { "0001", "0002", "0022", "992", "999272", "1199979" };
            //foreach (string v in prognos)
            //    olp.MatchProgramNumb(v);
            
            ////program no
            //string[] calls = { "CALL 0001 END", "CALL 0002", "CALL 0022", "CALL 992", "CALL 999272END ", "CALL 1199979" };
            //foreach (string v in calls)
            //    olp.MatchCall(v);
          
            ////var names
            //string[] varnames = { "LV1%", "LV2%", "V45%", "V33%", "V33%", "LV1!", "LV2!", "V45!", "V33!", "V33!" };
            //foreach (string j in varnames)
            //    olp.MatchVarName(j);

            ////if then endif
            //string[] ifconds = { "IF V2!>V50! THEN JMPP 501", "IF V2!>V50! THEN CALL 501", "IF V2!>=V50! THEN GOSUB 501","IF V2!>=V50! THEN 150","IF V2!>=V50! THEN LV1%", "JMP 01", "JMPP 501" };
            //foreach (string j in ifconds)
            //    olp.MatchIF(j);

            ////if then endif
            //string[] forloops = { "FOR V1!=300 TO 0 STEP -33.3 P1=P1+R1 CALL 0002 NEXT", 
            //                      "FOR V1!=0 TO 110 STEP 10 P1=P1+R1 CALL 0002 NEXT", 
            //                      "FOR V1!=0 TO 110 STEP -10 P1=P1+R1 CALL 0002 NEXT", "", "", "", ""};

            //foreach (string j in forloops)
            //    olp.MathcForNext(j);

            ////input signals
            //string[] inpSignals = { "DI=3", "DIL2=3.1", "DIB2=3.2", "DIB2=&H75", "AO2=3.4" };
            //foreach (string t in inpSignals)
            //    olp.MatchInputSignal(t);

            //string proe = "FEDRAT/1000.555500,MMPM";
            //olp.MatchClsSpeed(proe);
            
            ////input signals
            //string[] freqRegister = { "_RN{1}=3", "_RN[2] =3.1", "_RN3=3.2", "_RN4=&H75", "_RN6=20" };
            //foreach (string t in freqRegister)
            //    olp.MatchFrequencyRegister(t);


            //input signals
            string[] userDataFileNames = { "IRC5 LDoserXXX_D_5V01 - UserData.tmod", "IRC5 LDoserXXX_TCD_5V01 - UserData.tmod", "IRC5 LDoserXXX_D_5V01", "IRC5 LDoserXXX_HD_PUR_5V01.tmod", "IRC5 LStudXXX_T_5V01 - UserData.tmod" };
            foreach (string t in userDataFileNames)
                olp.MatchTemplateUserDataFormat(t);
            Console.ReadKey();
        }
    }
}