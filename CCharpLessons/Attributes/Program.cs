using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attributes
{
    class Program
    {
        [AttributeUsage(AttributeTargets.All)]
        public class HelpAttribute : System.Attribute
        {
            public readonly string Url;

            public string Topic               // Topic is a named parameter
            {
                get
                {
                    return topic;
                }
                set
                {

                    topic = value;
                }
            }

            public HelpAttribute(string url)  // url is a positional parameter
            {
                this.Url = url;
            }

            private string topic;
        }

        [HelpAttribute("http://localhost/MyClassInfo")]
        class MyClass
        {
        }

        static void Main(string[] args)
        {
            //Accessing Attributes Through Reflection
            System.Reflection.MemberInfo info = typeof(MyClass);
            object[] attributes = info.GetCustomAttributes(true);
            for (int i = 0; i < attributes.Length; i++)
            {
                System.Console.WriteLine(attributes[i]);
            }

            Console.ReadKey();
        }
    }
}
