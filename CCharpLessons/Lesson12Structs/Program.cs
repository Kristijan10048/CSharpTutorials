using System;

namespace Lesson12Structs
{
    //Structs can't have destructors, but classes can have destructors. 
    //Another difference between a struct and class is that a struct can't 
    //have implementation inheritance, but a class can


    public struct Point
    {
        public int X;
        public int Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    // Structures cannot be inherited from another structure or class. However, they can implement interfaces.
    internal struct Point3d
    {
        private Point Base;
        public int X { get { return Base.X; } }
        public int Y { get { return Base.Y; } }
        public int Z;

        public Point3d(Point basePoint, int z)
        {
            Base = basePoint;
            Z = z;
        }

        Point3d(int x, int y, int z)
        {
            Base = new Point(x, y);
            Z = z;
        }
    }


    public struct Line
    {
        public Point Start;
        public Point End;
        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public struct Rectangle
    {
        public Rectangle(uint width, uint height)
        {
            Width = width;
            Height = height;
        }

        public uint Width { set; get; }

        public uint Height { set; get; }
    }

    //Overloading struct Constructors
    public struct RectangleV1
    {     

        /// <summary>
        /// 
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public RectangleV1(int width, int height)
        { Width = width; Height = height; }

        public int Area()
        { return Width * Height; }
    }

    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Struct Rectangle: Default constructor");
            Rectangle myRec = new Rectangle();
            myRec.Height = 10;
            myRec.Width = 200;
            Console.WriteLine("width:{0} height:{1}", myRec.Width, myRec.Height);
            ///????
            //Rectangle rect1 = new Rectangle
            //{
            //   Height = 12,
            //   Width = 15
            //};
            Console.WriteLine("Struct Rectangle: Overloaded constructor");
            RectangleV1 myRecv1 = new RectangleV1(10, 10);
            Console.WriteLine("width:{0} height:{1}", myRecv1.Width, myRecv1.Height);
            Console.ReadKey();

        }
    }
}
