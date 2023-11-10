// See https://aka.ms/new-console-template for more information

void ParamsFunction(params int[] numbers)
{
    if (numbers == null)
    {
        Console.WriteLine("No params!!");
    }
    else
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            Console.WriteLine("Param[{0}]={1}",i,numbers[i]); 
        }
    }
}
static int Main(string[] args)
{
    Console.WriteLine("Main");
    return 0;
}

Console.WriteLine("Calling params!");
ParamsFunction(1,2,3,4,5,6,7,8,9,10);
var inp = Console.ReadLine();