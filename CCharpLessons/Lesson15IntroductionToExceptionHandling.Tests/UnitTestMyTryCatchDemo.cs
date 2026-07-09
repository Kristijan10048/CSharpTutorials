namespace Lesson15IntroductionToExceptionHandling.Tests;

using Lesson15IntroductionToExceptionHandling;


public class UnitTestMyTryCatchDemo
{
    [Fact]
    public void TestNoParams()
    {
        Lesson15IntroductionToExceptionHandling.MyTryCatchDemo demo = new Lesson15IntroductionToExceptionHandling.MyTryCatchDemo();

        var result = demo.canTryCatch();


        Assert.True(result);

    }

    [Fact]
    public void TestOneParam()
    {
        Lesson15IntroductionToExceptionHandling.MyTryCatchDemo demo = new Lesson15IntroductionToExceptionHandling.MyTryCatchDemo();

        var result = demo.canTryCatch(-1);

        Assert.False(result);

    }

    [Fact]
    public void TestTwoParams()
    {
        Lesson15IntroductionToExceptionHandling.MyTryCatchDemo demo = new Lesson15IntroductionToExceptionHandling.MyTryCatchDemo();

        var result = demo.canTryCatch(-1, -2);

        Assert.False(result);

    }


    [Fact]
    public void TestTwoParamsChar()
    {
        Lesson15IntroductionToExceptionHandling.MyTryCatchDemo demo = new Lesson15IntroductionToExceptionHandling.MyTryCatchDemo();

        var result = demo.canTryCatch('a', -2);

        Assert.False(result);

    }


    [Fact]
    public void TestTwoParamsWithChar()
    {
        Lesson15IntroductionToExceptionHandling.MyTryCatchDemo demo = new Lesson15IntroductionToExceptionHandling.MyTryCatchDemo();

        var result = demo.canTryCatch('a', 'a');

        Assert.True(result);

    }
}
