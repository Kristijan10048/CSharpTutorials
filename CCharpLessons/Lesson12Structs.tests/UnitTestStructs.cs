using Lesson12Structs;

namespace Lesson12Structs.tests
{
    public class UnitTestStructs
    {
        [Fact]
        public void TestPoint()
        {
            Lesson12Structs.Point point = new Lesson12Structs.Point(1, 2);

            Assert.Equal(1, point.X);
            Assert.Equal(2, point.Y);       

        }

        [Fact]

        public void TestLine()
        {
            Lesson12Structs.Point start = new Lesson12Structs.Point(1, 2);
            Lesson12Structs.Point end = new Lesson12Structs.Point(3, 4);
            Lesson12Structs.Line line = new Lesson12Structs.Line(start, end);
            Assert.Equal(1, line.Start.X);
            Assert.Equal(2, line.Start.Y);
            Assert.Equal(3, line.End.X);
            Assert.Equal(4, line.End.Y);
        }          
    }
}
