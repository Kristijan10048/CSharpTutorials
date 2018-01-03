using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson12Structs
{
    //Structs can't have destructors, but classes can have destructors. 
    //Another difference between a struct and class is that a struct can't 
    //have implementation inheritance, but a class can

    struct Rectangle
    {
        private uint width;
        private uint height;
        public uint Width
        {
            set
            {
                width = value;
            }
            get
            {
                return width;
            }
        }

        public uint Height
        {
            set
            {
                height = value;
            }
            get
            {
                return height;
            }
        }
    }
    //Overloading struct Constructors
struct RectangleV1
{    
    private int m_width;
    private int m_height;
     
    public int Width 
    {
        get
        {
            return m_width;
        }
        set
        {
            m_width = value;
        }
    }      
    
    public int Height
    {
        get
        {
            return m_height;
        }
        set
        {
            m_height = value;
        }
    } 
    public RectangleV1(int width, int height) 
    { m_width = width; m_height = height; }
}
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Struct Rectangle: Default constructor");
            Rectangle myRec = new Rectangle();
            myRec.Height = 10;
            myRec.Width = 200;
            Console.WriteLine("width:{0} height:{1}",myRec.Width, myRec.Height);
            ///????
            //Rectangle rect1 = new Rectangle
            //{
            //   Height = 12,
            //   Width = 15
            //};
            Console.WriteLine("Struct Rectangle: Overloaded constructor");
            RectangleV1 myRecv1 = new RectangleV1(10,10);
            Console.WriteLine("width:{0} height:{1}", myRecv1.Width, myRecv1.Height);
            Console.ReadKey();

        }
    }
}
