using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace Lesson19HybridDictionary
{

    public class HybridDictionaryDemo
    {

        private HybridDictionary m_hb;

        #region Public Methods
        public HybridDictionaryDemo()
        {
            m_hb = new HybridDictionary();
        }

        public bool HybridDictionaryAdd(string key, string value)
        {
            try
            {
                m_hb.Add(key, value);
                return true;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("An element with the same key already exists in the HybridDictionary.");
                return false;
            }
        }

        public object HybridDictionaryGet(string key)
        {
            return (string)m_hb[key];
        }

        public bool HybridDictionaryRemove(string key)
        {
            if (m_hb.Contains(key))
            {
                m_hb.Remove(key);

                return true;
            }
            else
            {
                Console.WriteLine("The key does not exist in the HybridDictionary.");
                return false;
            }
        }

        public System.Collections.ICollection GetValues()
        {
            return m_hb.Values;
        }
        #endregion

    }

    class Program
    {
        static void Main(string[] args)
        {
            HybridDictionary hb = new HybridDictionary();

            hb.Add("M", "Male");
            hb.Add("S", "Scot");
            hb.Add("F", "Female");

            Console.WriteLine("M:{0}", hb["S"]);

            Console.ReadKey();
        }
    }
}
