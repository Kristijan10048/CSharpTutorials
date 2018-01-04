using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace LinqXml
{
    /// <summary>
    /// //TODO
    /// </summary>
    public class Entities
    {
        //TODO
    }

    /// <summary>
    /// 
    /// </summary>
    public class LinqNumbers
    {
        /// <summary>
        /// Simple axampe of how to use linq
        /// </summary>
        public void ParseNumbers()
        {
            int[] numbers = { 2, 4, 6, 8, 21, 9, 2, 0 };

            //usage of Linq. Get numbers within given range
            var result = from n in numbers where n < 5 select n;

            //print it
            foreach (int number in result)
                Console.WriteLine(number);

            //Use lambda expression to do same operation
            var result2 = numbers.Where(n => n < 5).Select(n => n);

            Console.WriteLine("--------------");

            //print it
            foreach (int number in result2)
                Console.WriteLine(number);

            Console.ReadKey();
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReadDataFromDB()
        {
            //var result = from c in . select 
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LinqXml
    {
        /// <summary>
        /// 
        /// </summary>
        public void ReadXmlDocumet()
        {
            XDocument xdoc = null;

            //using (XmlReader xr = XmlReader.Create("LinqXml.books.xml"))
            //{
            xdoc = XDocument.Load(@"..\..\Books.xml");

            List<string> titles = GetBooks(xdoc, "Sundered");

            foreach (string s in titles)
                Console.WriteLine(s);

            Console.ReadKey();
            //}


            //Assembly asm = Assembly.GetAssembly(typeof(XmlHelper));
            //using (Stream stream = asm.GetManifestResourceStream("Common.XML.books.xml"))
            //{
            //    xdoc = XDocument.Load(;
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xdoc"></param>
        /// <param name="titleSearch"></param>
        /// <returns></returns>
        public List<string> GetBooks(XDocument xdoc, string titleSearch)
        {
            var query = from t in xdoc.Descendants("title")
                        where t.Value.ToLower().Contains(titleSearch.ToLower())
                        select t.Value;

            return query.ToList<string>();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CaLinqStaubliDataFileReader
    {
        #region Private Members
        /// <summary>
        /// 
        /// </summary>
        private XDocument m_xmlDoc;
        #endregion

        #region PublicMethods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool LoadXmlFile()
        {
            m_xmlDoc = XDocument.Load(@"..\..\XmlData\DataStaubli.xml");

            return m_xmlDoc != null ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<XElement> GetDatabaseElement()
        {
            IEnumerable<XElement> database = m_xmlDoc.Elements();
            //foreach (var data in database)
            //{
            //    Console.WriteLine(data);
            //}
            return database;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> GetJointData()
        {

            IEnumerable<XElement> database = GetDatabaseElement();

            List<string> pointData = new List<string>();


            var jointData = from nm in database.Elements("Datas")
                            where (string)nm.Element("Value") == "jointRs"
                            select nm;

            // Console.WriteLine("Details of Female Employees:");

            foreach (XElement xEle in jointData)
            {
                Console.WriteLine("--------------------------------------------\n");
                Console.WriteLine(xEle);
            }

            return pointData;
        }
        #endregion
    }

    public static class CAppXmlWithLinq
    {
        public static void Main()
        {
            //LinqNumbers num = new LinqNumbers();
            //num.ParseNumbers();

            //LinqXml booksXml = new LinqXml();
            //booksXml.ReadXmlDocumet();

            CaLinqStaubliDataFileReader dataReader = new CaLinqStaubliDataFileReader();
            dataReader.LoadXmlFile();

            dataReader.GetDatabaseElement();

            dataReader.GetJointData();

            Console.ReadKey();
        }
    }
}