using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemosWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Default Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();


            //MyApp myApp = new MyApp("Testovski1");

            ApPerson tmpPerson = new ApPerson();
            tmpPerson.FirstName = "Test";

            tmpPerson.LastName = "Testovski";

            tmpPerson.City = "Knowhere";
            tmpPerson.Address = "Somewhere";
            this.DataContext = tmpPerson;

            // bind the Date to the UI
            //EventDate.SetBinding(DatePicker.SelectedDateProperty, new Binding("Date")
            //{
            //    Source = _event,
            //    Mode = BindingMode.TwoWay
            //});

            //// bind the Title to the UI
            //EventTitle.SetBinding(TextBox.TextProperty, new Binding("Title")
            //{
            //    Source = _event,
            //    Mode = BindingMode.TwoWay
            //});
        }
        #endregion

        #region Private Callbacks
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
