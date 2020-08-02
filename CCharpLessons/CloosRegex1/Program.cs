using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace CloosRegex1
{

    class CloosReader
    {

        #region Private Constants
        public static readonly Regex C_REGEX_LIST_DATA_ELEMENT = new Regex(@"\s*LIST\s+(\d+)\s+=\s*\((.+)\)\s*", RegexOptions.IgnoreCase);

        public static readonly Regex C_REGEX_PROG_DATA_ELEMENT = new Regex(@"\s*(\d+)\s*\,\s*(.+)\s*", RegexOptions.IgnoreCase);
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void MatchListBlock(string text)
        {
            Match m;
            if ((m = C_REGEX_LIST_DATA_ELEMENT.Match(text)).Success)
            {
                string listNo = m.Groups[1].Value;
                string paramStr = m.Groups[2].Value;

                //Console.WriteLine("Text:{0}", text);
                Console.WriteLine("List no:{0}", listNo);
                Console.WriteLine("List params:{0}", paramStr);
                Console.WriteLine("--------------------------------");
                string[] listParams = paramStr.Split(',');
                int count = 0;
                foreach (string s in listParams)
                {
                    Console.WriteLine("{0 }Param: {1}", count, s);
                    count++;
                }
                Console.WriteLine("--------------------------------");
            }
            else
            {
                Console.WriteLine("No match");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void MatchDataBlock(string text)
        {
            Match m;
            if ((m = C_REGEX_PROG_DATA_ELEMENT.Match(text)).Success)
            {
                string listNo = m.Groups[1].Value;
                string paramStr = m.Groups[2].Value;

                //Console.WriteLine("Text:{0}", text);
                Console.WriteLine("List no:{0}", listNo);
                Console.WriteLine("List params:{0}", paramStr);
                Console.WriteLine("--------------------------------");
                string[] listParams = paramStr.Split(',');
                int count = 0;
                foreach (string s in listParams)
                {
                    Console.WriteLine("{0 }Param: {1}", count, s);
                    count++;
                }
                Console.WriteLine("--------------------------------");
            }
            else
            {
                Console.WriteLine("No match");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public bool ExtractListData(Match m)
        {           
            //.true.if ((m = C_REGEX_LIST_DATA_ELEMENT.Match(text)).Success)
            //{

            Console.WriteLine("---------------ExtractListData-----------------");
                string listNo = m.Groups[1].Value;
                string paramStr = m.Groups[2].Value;

                //Console.WriteLine("Text:{0}", text);
                Console.WriteLine("List no:{0}", listNo);
                Console.WriteLine("List params:{0}", paramStr);
                Console.WriteLine("--------------------------------");
                string[] listParams = paramStr.Split(',');
                int count = 0;
                foreach (string s in listParams)
                {
                    Console.WriteLine("{0 }Param: {1}", count, s);
                    count++;
                }
                Console.WriteLine("--------------------------------");
           // }
            //else
           // {
            //    Console.WriteLine("No match");
            //    return false;
           // }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public bool ExtractProgramData(Match m)
        {
            Console.WriteLine("---------------Point Data-----------------");
            string pointNo = m.Groups[1].Value;
            string paramStr = m.Groups[2].Value;

            //Console.WriteLine("Text:{0}", text);
            Console.WriteLine("Point no:{0}", pointNo);
            Console.WriteLine("Point params:{0}", paramStr);
            Console.WriteLine("--------------------------------");

            string[] listParams = paramStr.Split(',');
            int count = 0;
            foreach (string s in listParams)
            {
                Console.WriteLine("{0 }Param: {1}", count, s);
                count++;
            }
            Console.WriteLine("--------------------------------");
          
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sDataBuffer"></param>
        /// <returns></returns>
        public bool ReadBuff(string sDataBuffer)
        {
            // read position
            MatchCollection mColl = C_REGEX_LIST_DATA_ELEMENT.Matches(sDataBuffer);

            foreach (Match m in mColl)
            {
               // s//tring dataElement = m.Value;
                if (!ExtractListData(m))
                    return false;
            }            
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sDataBuffer"></param>
        /// <returns></returns>
        public bool ReadDataBuff(string sDataBuffer)
        {
            // read position
            MatchCollection mColl = C_REGEX_PROG_DATA_ELEMENT.Matches(sDataBuffer);
            foreach (Match m in mColl)
            {
                // s//tring dataElement = m.Value;
                if (!ExtractProgramData(m))
                    return false;
            }
            return true;
        }
        #endregion
    }


    class Program
    {
        static void Main(string[] args)
        {

            CloosReader reader = new CloosReader();

            //input signals
            string[] listData = {  
                "LIST 1 = (5611,3,0,70,80,150,550,0,330,10,50,22,380,0,0,0,0,0,35,30,0,     1)",
                "LIST 2 = (5611,3,0,65,100,210,550,0,330,15,50,22,380,0,0,0,0,0,35,30,0,    2)",
                "LIST 3 = (5611,3,1000,65,90,180,550,0,330,0,50,22,380,0,0,0,0,0,35,30,0,   3)",
                "LIST 4 = (5611,3,750,65,110,210,550,0,330,0,50,22,380,0,0,0,0,0,35,30,0,   4)",
                "LIST 5 = (5611,3,0,60,110,210,550,0,330,15,50,22,380,0,0,0,0,0,35,30,0,    5)",
                "LIST 6 = (5611,3,0,50,105,210,550,0,330,15,50,22,380,0,0,0,0,0,35,30,0,6)",
                "LIST 7 = (5611,3,0,50,115,230,550,0,330,15,50,22,380,0,0,0,0,0,35,30,0,7)",
                "LIST 8 = (5611,3,0,60,115,230,550,0,330,15,50,22,380,0,0,0,0,0,35,30,0,8)",
                "LIST 9 = (5611,3,0,50,115,230,550,0,330,15,50,22,380,0,0,0,0,0,35,30,0,9)",
                "LIST 10 = (5611,3,0,70,90,190,550,0,330,15,50,22,380,0,0,0,0,0,35,30,0,10)",
                "LIST 11 = (5611,3,0,50,100,200,550,0,330,0,50,22,380,0,0,0,0,0,35,30,0,11)",
                "LIST 12 = (5611,3,0,50,100,200,550,0,330,10,50,22,380,0,0,0,0,0,35,30,0,12)",
                "LIST 13 = (5611,3,0,60,90,165,550,0,330,0,50,22,380,0,0,0,0,0,35,10,0,13)",
                "EXTERNAL PROC POS0,POS180,SYN2,SBR FROM MASTER"
                                         };



            //input signals
            string listBuff =
                @"RESTART

!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
!!!!!!!!!!!!!!!!!!                      !!!!!!!!!!!!!!!!!!!!!!!!!!
!!!!!!!!!!!!!!!!!!   GL 4/5 STANDARD    !!!!!!!!!!!!!!!!!!!!!!!!!! 
!!!!!!!!!!!!!!!!!!     TABLE 2          !!!!!!!!!!!!!!!!!!!!!!!!!! 
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


LIST 1 = (5611,3,0,70,80,150,550,0,330,10,50,22,380,0,0,0,0,0,35,30,0,0)
LIST 2 = (5611,3,0,65,100,210,550,0,330,15,50,22,380,0,0,0,0,0,35,30,0,0)
LIST 3 = (5611,3,1000,65,90,180,550,0,330,0,50,22,380,0,0,0,0,0,35,30,0,0)
LIST 4 = (5611,3,750,65,110,210,550,0,330,0,50,22,380,0,0,0,0,0,35,30,0,0)
LIST 5 = (5611,3,0,60,110,210,550,0,330,15,50,22,380,0,0,0,0,0,35,30,0,0)
LIST 6 = (5611,3,0,50,105,210,550,0,330,15,50,22,380,0,0,0,0,0,35,30,0,0)
LIST 7 = (5611,3,0,50,115,230,550,0,330,15,50,22,380,0,0,0,0,0,35,30,0,0)
LIST 8 = (5611,3,0,60,115,230,550,0,330,15,50,22,380,0,0,0,0,0,35,30,0,0)
LIST 9 = (5611,3,0,50,115,230,550,0,330,15,50,22,380,0,0,0,0,0,35,30,0,0)
LIST 10 = (5611,3,0,70,90,190,550,0,330,15,50,22,380,0,0,0,0,0,35,30,0,0)
LIST 11 = (5611,3,0,50,100,200,550,0,330,0,50,22,380,0,0,0,0,0,35,30,0,0)
LIST 12 = (5611,3,0,50,100,200,550,0,330,10,50,22,380,0,0,0,0,0,35,30,0,0)

LIST 13 = (5611,3,0,60,90,165,550,0,330,0,50,22,380,0,0,0,0,0,35,10,0,0)
EXTERNAL PROC POS0,POS180,SYN2,SBR FROM MASTER

VAR I,SPD,TYP,STAT,X1,Y1,Z1,A1,B1,G1,E1,E2,E3,E4,E5


MAIN



SETTIME (10;0)
SETTIME (20;0)

!!!CALL POS180
!MANAX (0,10)
!MANAX (0,11)
FUNCON ARCCON
FUNCOFF ONLCON
FUNCON ENDCON
CALL SYN2
SET (5)
DECH
ROF (7)
PTPMAX (100)
STV (25)
STON

RESET (16)

GP (1000)
! POINTAGE TUBE

GP (10,11,12)
$ (1)
GC (13)
GP (14)

GP (20)
$ (2)
SSPD (10,10)
GC (21)
GP (22)

!GP (30,31)
!$ (3)
!SSPD (0,0)
!GC (32)
!GP (33)";


string dataBuff = @"
( Robot     : 310 )
( Serial Nr : 2253772 )
( Achszahl  : 8 )
( Resolution: 2:2:2:2:2:2:2:2: Konfigend)
00000,00000,00000,00000,000000000,000000000,000000000,000000000,000000000,000000000,000000000,000000000
00001,00100,00000,00001,000242957,000183878,000230026,000137531,000095173,000374631,001048568,000524288
00002,00100,00000,00001,000248128,000186047,000228611,000137406,000097235,000374054,001048568,000524288
00003,00100,00000,00001,000246959,000185502,000228972,000137423,000096769,000374188,001048568,000524288
00004,00100,00000,00001,000242957,000183878,000230026,000137531,000095173,000374631,001048568,000524288
00005,00100,00000,00001,000236925,000185964,000218560,000128196,000092968,000382855,001048568,000524288
00006,00100,00000,00001,000236924,000188863,000211451,000127031,000093265,000386154,001048568,000524288
00007,00100,00000,00001,000236924,000188128,000213163,000127309,000093202,000385378,001048568,000524288
00008,00100,00000,00001,000236925,000185964,000218560,000128196,000092968,000382855,001048568,000524288
00009,00100,00000,00001,000241388,000197544,000198102,000115430,000091961,000391783,001048568,000524288
00010,00100,00000,00001,000243634,000200581,000193139,000113910,000092508,000393769,001048568,000524288
00011,00100,00000,00001,000247114,000188418,000202208,000112237,000093659,000394524,001048568,000524288
00012,00100,00000,00001,000250972,000179587,000207590,000109999,000094754,000396091,001048568,000524288
00013,00100,00000,00001,000248023,000176650,000211763,000111934,000094024,000394386,001048568,000524288
00014,00100,00000,00001,000252454,000169071,000215324,000109145,000095178,000396608,001048568,000524288
00015,00100,00000,00001,000255799,000172159,000211299,000106905,000095965,000398261,001048568,000524288
00016,00100,00000,00001,000261911,000165954,000213784,000102718,000097326,000400949,001048568,000524288
00017,00100,00000,00001,000258158,000162663,000217700,000105317,000096510,000399315,001048568,000524288
00018,00100,00000,00001,000267922,000156147,000219452,000098386,000098514,000403475,001048568,000524288
00019,00100,00000,00001,000272090,000159734,000215653,000095322,000099292,000405111,001048568,000524288
00020,00100,00000,00001,000281287,000156411,000216363,000088282,000100804,000408660,001048568,000524288
00021,00100,00000,00001,000277068,000152607,000220085,000091554,000100142,000407043,001048568,000524288
00022,00100,00000,00001,000367729,000145168,000220504,000177358,000159459,000341747,001048568,000524288
00023,00100,00000,00001,000364713,000149356,000217060,000179930,000159280,000340654,001048568,000524288
00024,00100,00000,00001,000409501,000172966,000210904,000124952,000174690,000365813,001048568,000524288
00025,00100,00000,00001,000408443,000176652,000209115,000125375,000174304,000366391,001048568,000524288
00026,00100,00000,00001,000411781,000174484,000212829,000123787,000175391,000368005,001048568,000524288 ";
            //foreach (string t in listData)
            //{
            //    reader.MatchListBlock(t);
            //}

            reader.ReadBuff(listBuff);

            reader.ReadDataBuff(dataBuff);

            Console.ReadKey();           
        }
    }
}
