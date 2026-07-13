namespace Lesson5_1Methods.tests
{
    using Lesson5_1Methods;
    public class UnitTestsMethods   
    {
        [Fact]
        public void Test1()
        {
            Lesson5_1Methods.RefMethodCl refMethodCl = new Lesson5_1Methods.RefMethodCl();

            //Cant be accesed Assert.Equal(true, Lesson5_1Methods.RefMethodCl.IsEven(2));)

            Assert.True(Lesson5_1Methods.RefMethodCl.CanMultiply(0,0) );
        }
    }
}
