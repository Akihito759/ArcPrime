using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Web;
using ArcPrime.WPF.Communication;
using ArcPrime.WPF.Model;
using ArcPrime.WPF.Actions;


namespace ArcPrime.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            Display.SaveToArray((MainWindow)Application.Current.MainWindow); //Save all data to values
            IExperimentClient client = new ExperimentClient();
            var emailAddress = "GregCudok@yahoo.co.uk";
            var token = "F673BBA832C6E961A0174BD158D81717";
            var restart = client.Execute(emailAddress, token, "restart", "0"); //Restart on every runprogram for good working loger
            var response = client.Describe(emailAddress, token); //take information from server
            Display.Update(response); //update all info from server to the view

        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) //Making textbox empty after focus with no input
        {
            TextBox tb = (TextBox)sender;
            if(tb.Text == "Value") tb.Text = string.Empty;

        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //IExperimentClient client = new ExperimentClient();
            //var emailAddress = "GregCudok@yahoo.co.uk";
            //var token = "F673BBA832C6E961A0174BD158D81717";
            //var response = client.Describe(emailAddress, token);
            //Display.Update(response);
        //    Logger.Write();

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            IExperimentClient client = new ExperimentClient();
            var emailAddress = "GregCudok@yahoo.co.uk";
            var token = "F673BBA832C6E961A0174BD158D81717";
            string order = Display.Order(); //Choose order from box, 
            if (order == "Restart") textBox1.Text = ""; //Clean the textbox if Restart;
            string value = Display.Value(); 
            if(order != null ) // prevent from using without choosen order
            {
                var err = client.Execute(emailAddress, token, order, value);
                var response = client.Describe(emailAddress, token);
                Display.Update(response);
                Logger.Write(); //write to log file all data
            }

        }

   
    }
}
