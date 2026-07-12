using Lesson19HybridDictionary;
using System.Xml;

namespace Lesson19HybridDictionary.tests
{
    public class UnitTestHybridDictionaryDemo
    {
        [Fact]
        public void HybridDictionaryDemoAddTest()
        {
            HybridDictionaryDemo demo = new HybridDictionaryDemo();

            demo.HybridDictionaryAdd("1", "1");
            demo.HybridDictionaryAdd("2", "2");
            demo.HybridDictionaryAdd("3", "3");


            Assert.Equal("1", demo.HybridDictionaryGet("1") as string);
            Assert.Equal("2", demo.HybridDictionaryGet("2") as string);
            Assert.Equal("3", demo.HybridDictionaryGet("3") as string);
            Assert.DoesNotContain("4", demo.HybridDictionaryGet("4") as string);
        }


        [Fact]
        public void HybridDictionaryDemeRemoveTest()
        {
            HybridDictionaryDemo demo = new HybridDictionaryDemo();

            demo.HybridDictionaryAdd("1", "1");
            demo.HybridDictionaryAdd("2", "2");
            demo.HybridDictionaryAdd("3", "3");


            Assert.Equal("1", demo.HybridDictionaryGet("1") as string);
            Assert.Equal("2", demo.HybridDictionaryGet("2") as string);
            Assert.Equal("3", demo.HybridDictionaryGet("3") as string);
            Assert.DoesNotContain("4", demo.HybridDictionaryGet("4") as string);

            demo.HybridDictionaryRemove("2");
            Assert.DoesNotContain("2", demo.HybridDictionaryGet("2") as string);
            
            demo.HybridDictionaryRemove("3");
            Assert.DoesNotContain("3", demo.HybridDictionaryGet("3") as string);

        }

    }
}
