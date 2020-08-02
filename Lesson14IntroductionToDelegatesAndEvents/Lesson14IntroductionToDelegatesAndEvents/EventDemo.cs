using System;
using System.Drawing;
using System.Windows.Forms;


//(are you missing an assembly reference?)	 visual studio 2005 error
//solution add reference

public delegate void StartDelegate();
class EventDemo : Form
{
    public event StartDelegate startEvent;

    public EventDemo()
    {
        Button clickMe = new Button();

        clickMe.Parent = this;
        clickMe.Text = "Click me";
        clickMe.Location = new Point(ClientSize.Width /2, ClientSize.Height/2);
        clickMe.Click += new EventHandler(OnClick);
       // startEvent += new StartDelegate(OnStartEvent);  
    }

    public void OnClick(object sender, EventArgs ea)
    {
        MessageBox.Show("You clicked my botton!!!");
    }
}