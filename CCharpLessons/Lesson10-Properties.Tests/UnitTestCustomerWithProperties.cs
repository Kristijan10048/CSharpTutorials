namespace Lesson10_Properties.Tests;

using Lesson10Properties;


public class UnitTestCustomerWithProperties
{
    [Fact]
    public void TestCustumerProp()
    {
        Lesson10Properties.CustomerWithProperties cust1 = new Lesson10Properties.CustomerWithProperties();

        cust1.ID = 1;
        cust1.Name = "John Doe";
       
        Assert.Equal(1, cust1.ID);
        Assert.Equal("John Doe", cust1.Name);
    }


    [Fact]
    public void TestCustumerProp1()
    {
        Lesson10Properties.CustomerWithProperties cust1 = new Lesson10Properties.CustomerWithProperties();

        cust1.ID = -11;
        cust1.Name = "John Doe";

        Assert.NotEqual(1, cust1.ID);
        Assert.Equal("John Doe", cust1.Name);
    }
}

