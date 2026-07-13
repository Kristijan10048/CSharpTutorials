public class DelegateDemo
{

    #region Public delegates
    public delegate void MyVoidDelegate();
    public delegate int MyIntDelegate();
    public delegate int MyIntDelegateWithParams(int a, int b);
    #endregion


    public static void Main(string[] args)
    {
        //created a pointer to the HelloWorld method
        MyVoidDelegate del = new MyVoidDelegate(HelloWorld);
        del();

        //created a pointer to the Add method. Returns an int value
        MyIntDelegate del2 = new MyIntDelegate(getVal);

        //created a pointer to the Add method. Returns an int value and takes two parameters
        MyIntDelegateWithParams del3 = new MyIntDelegateWithParams(Add);

        del3(1, 2);
    }


    public static int getVal()
    { return 5; }
    public static int Add(int a, int b)
    {
        return a + b;
    }

    public static void HelloWorld()
    { }
}