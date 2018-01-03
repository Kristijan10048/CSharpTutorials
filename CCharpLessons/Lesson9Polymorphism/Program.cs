using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson9Polymorphism
{
    class DrawingObject
    {
        public virtual void draw()
        {
            Console.WriteLine("I am just a generecic drawing object.");
        }

    }

    class Line : DrawingObject
    {
        public override void draw()
        {
            //pristap na osnovna klasa base.
            Console.WriteLine("I am a Line");
        }
    }

    class Circle : DrawingObject
    {
        public override void draw()
        {
            Console.WriteLine("I am a circle");
        }
    }

    class Square : DrawingObject
    {
        public override void draw()
        {
            Console.WriteLine("I am a square");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            DrawingObject[] objects = new DrawingObject[4];
            objects[0] = new DrawingObject();
            objects[1] = new Line();
            objects[2] = new Circle();
            objects[3] = new Square();

            foreach (DrawingObject obj in objects)
                obj.draw();

            Console.ReadKey();
        }
    }
}
