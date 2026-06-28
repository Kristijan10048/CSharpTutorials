using Microsoft.Extensions.Logging;

internal static class TestEndpoints
{
    public static string Test() => "Test Message";

    public static string TestCstr() => "Test Const str";

    public static string TestFnCall() => "Test Function call";
}
