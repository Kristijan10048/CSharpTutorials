using System;

namespace Lesson12Structs
{
    //Structs can't have destructors, but classes can have destructors. 
    //Another difference between a struct and class is that a struct can't 
    //have implementation inheritance, but a class can

    /// <summary>
    /// 
    /// </summary>
    struct Rectangle
    {
        p'ublic uint Width { set; get; }

        public uint Height { set; get; }
    }

    //Overloading struct Constructors
    struct RectangleV1
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
