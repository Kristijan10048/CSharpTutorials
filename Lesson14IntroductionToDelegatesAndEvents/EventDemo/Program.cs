using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EventDemo
{
    //(are you missing an assembly reference?)	 visual studio 2005 error
    //solution add reference

    public delegate void StartDelegate();
    class EventDemo : Form
    {
        public event StartDelegate StartEvent;

        public EventDemo()
        {
            Button clickMe = new Button();

            clickMe.Parent   = this;
            clickMe.Text     = "Click me";
            clickMe.Location = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
            //The += syntax registers a delegate with an event. 
            //To unregister with an event, use the -= with the same syntax
            clickMe.Click   += new EventHandler(OnClick);
            StartEvent      += new StartDelegate(OnStartEvent);  
        }
        public void OnClick(object sender, EventArgs ea)
        {
            MessageBox.Show("You clicked my botton!!!");
        }

        public void OnStartEvent()
        {
            MessageBox.Show("Delegate call!!!");
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EventDemo());
        }
    }
}