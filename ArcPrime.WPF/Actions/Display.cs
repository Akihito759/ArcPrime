using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcPrime.WPF.Model;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows;

namespace ArcPrime.WPF.Actions
{
    static class Display
    {
        static Label[] Label_arr; //array of labels for values
        static Label[] Label_arr_info;
        static TextBox[] TextBox_arr;
        static ComboBox ComboBox;
        static IList<string> EventList; //Copy from the Result List of Events
        static int[] EventsCount; //Store event count for each month
        static int Month = 0;
        static string ValueRef, text=null, EventsText; // ValueRef for passing value even if Value textbox would be clear
                                                  //text, EventsText are strings for display info in window and to save in file

        static Display()
        {
            Label_arr = new Label[21];
            Label_arr_info = new Label[21];
            TextBox_arr = new TextBox[2];
            EventsCount = new int[30];
        }

        public static void SaveToArray(MainWindow a)
        {
            // max value for Label_arr[0] = 24;
            // max value for Label_arr[9,12,13,14,15,16] = 200
            // max value for Label_arr[10] = 100

            // Value Labels
            Label_arr[0] = a.label_Info_Value0;
            Label_arr[1] = a.label_Info_Value1;
            Label_arr[2] = a.label_Info_Value2;
            Label_arr[3] = a.label_Info_Value3;
            Label_arr[4] = a.label_Info_Value4;
            Label_arr[5] = a.label_Info_Value5;
            Label_arr[6] = a.label_Info_Value6;
            Label_arr[7] = a.label_Info_Value7;
            Label_arr[8] = a.label_Info_Value8;
            Label_arr[9] = a.label_Info_Value9;
            Label_arr[10] = a.label_Info_Value10;
            Label_arr[11] = a.label_Info_Value11;
            Label_arr[12] = a.label_Info_Value12;
            Label_arr[13] = a.label_Info_Value13;
            Label_arr[14] = a.label_Info_Value14;
            Label_arr[15] = a.label_Info_Value15;
            Label_arr[16] = a.label_Info_Value16;
            Label_arr[17] = a.label_O_Info_Values0;
            Label_arr[18] = a.label_O_Info_Values1;
            Label_arr[19] = a.label_O_Info_Values2;
            Label_arr[20] = a.label_O_Info_Values3;

            //Info Labels:
            Label_arr_info[0] = a.label_Info0;
            Label_arr_info[1] = a.label_Info1;
            Label_arr_info[2] = a.label_Info2;
            Label_arr_info[3] = a.label_Info3;
            Label_arr_info[4] = a.label_Info4;
            Label_arr_info[5] = a.label_Info5;
            Label_arr_info[6] = a.label_Info6;
            Label_arr_info[7] = a.label_Info7;
            Label_arr_info[8] = a.label_Info8;
            Label_arr_info[9] = a.label_Info9;
            Label_arr_info[10] = a.label_Info10;
            Label_arr_info[11] = a.label_Info11;
            Label_arr_info[12] = a.label_Info12;
            Label_arr_info[13] = a.label_Info13;
            Label_arr_info[14] = a.label_Info14;
            Label_arr_info[15] = a.label_Info15;
            Label_arr_info[16] = a.label_Info16;
            Label_arr_info[17] = a.label_O_Info0;
            Label_arr_info[18] = a.label_O_Info1;
            Label_arr_info[19] = a.label_O_Info2;
            Label_arr_info[20] = a.label_O_Info3;

            TextBox_arr[0] = a.textBox;
            TextBox_arr[1] = a.textBox1;

            ComboBox = a.comboBox;


        } //Store All DisplayData into variables which are used in this class

        public static void Update(Result response)
        {

            Month = Convert.ToInt32(response.Turn); //Place in code to store the length of List for every month in the game.
            EventsCount[Month] = response.Events.Count;
            EventList = response.Events; //get copy of the Events list for future using. => Look ToSave


            if (Month > 1)
            {
                ToSave(); //prepare data from Month before to save
                TextBox_arr[1].Text = Display.Events();
                TextBox_arr[1].ScrollToEnd();
            }


            Label_arr[0].Content = response.Turn.ToString("0.") + "/24"; //Convert All Result Data to string and put it in the labels
            Label_arr[1].Content = response.ShouldIRestartExperimentAndCry;

            if (response.NehoRunes != null) //Can be null after reset so I need to cheak it
            {
                for (int i = 2; i < 9; ++i)
                {
                    Label_arr[i].Content = response.NehoRunes[i - 2]; //First element -> Label_arr[2].Content = response.NehoRunes[0];
                    IFColor(Label_arr[i], 1, response);
                }
            }

            Label_arr[9].Content = response.FoodQuantity.ToString("0.##") + "/200";
            Label_arr[10].Content = response.Waste.ToString("0.##") + "/100";
            Label_arr[11].Content = response.SocialCapital.ToString("0.##") + "/100";
            Label_arr[12].Content = response.Production.ToString("0.##") + "/200";
            Label_arr[13].Content = response.FoodCapacity.ToString("0.##") + "/200";
            Label_arr[14].Content = response.ArcologyIntegrity.ToString("0.##") + "/200";
            Label_arr[15].Content = response.Population.ToString("0.##") + "/200";
            Label_arr[16].Content = response.PopulationCapacity.ToString("0.##") + "/200";
            Label_arr[17].Content = response.IsTerminated;
            Label_arr[18].Content = response.EventScore.ToString("0.##");
            Label_arr[19].Content = response.ExperimentScore.ToString("0.##");
            Label_arr[20].Content = response.TotalScore.ToString("0.##");

            TextBox_arr[0].Text = "Value"; //Auto update the value textbox

        } //Update the window view with All Data

        public static string Order() 
        {
            string s = ComboBox.Text;
            int Choice = 0;
            if (s != "") Choice = Convert.ToInt32(s[1]) - '0'; //convert from char to int if not - '0' you will get the ascii value of number aditionaly "" is the default combobox value


            string Order = null;

            switch (Choice)
            {
                case 1:
                    Order = "ImportFood";
                    break;
                case 2:
                    Order = "Produce";
                    break;
                case 3:
                    Order = "Propaganda";
                    break;
                case 4:
                    Order = "Clean";
                    break;
                case 5:
                    Order = "BuildArcology";
                    break;
                case 6:
                    Order = "ExpandPopulationCapacity";
                    break;
                case 7:
                    Order = "ExpandFoodCapacity";
                    break;
                case 8:
                    Order = "Weareready";
                    break;
                case 9:
                    Order = "Restart";
                    break;
                default:
                    Order = null;
                    break;
            }
            return Order;
        } //Get the Order String based on combobox

        public static string Value()  
        {
            int Value = 0;

            if (Int32.TryParse(TextBox_arr[0].Text, out Value))
            {
                if (Value <= 200 && Value >= 1)
                {
                    ValueRef = TextBox_arr[0].Text;
                    return ValueRef;
                }


            }
            ValueRef = "1"; //minimal value for accidentaly don't type value in textbox
            return ValueRef;

        } //Get and cheack the value box

        public static string Events() 
        {
            if (Month == 2)
            {
                EventsText = null;
            }


            if (Month == 2)
            {

                EventsText += "=====EVENTS======\n\n =====MONTH " + (Month - 1) + "======\n";
                EventsText += "Command: " + Order() + " " + ValueRef + "\n ===================\n";
                EventsText += EventList[0] + "\n";
            }
            else
            {
                EventsText += "\n =====MONTH " + (Month - 1) + "======\n";
                EventsText += "Command: " + Order() + " " + ValueRef + "\n ===================\n";

                for (int i = EventsCount[Month - 2]; i < EventsCount[Month - 1]; ++i)
                {
                    EventsText += EventList[i] + "\n";
                }
            }

            return EventsText;

        }//Event list for window in bottom
        public static void ToSave()
        {
            if (Month == 2)
            {
                text = null;
            }



            text += "\n\n =======MONTH " + (Month - 1) + "====== \n\n";

            text += "Command: " + Order() + " " + ValueRef + "\n\n ===================\n";


            for (int i = 1; i <= 20; ++i)
            {
                text += Label_arr_info[i].Content + " : " + Label_arr[i].Content + "\n";
            }


            text += "\n=====EVENTS======\n";


            if (Month == 2)
            {
                text += EventList[0];
            }
            else
            {
                for (int i = EventsCount[Month - 2]; i < EventsCount[Month - 1]; ++i)
                {
                    text += EventList[i] + "\n";

                }
            }


        }//Prepare every thing to save

        public static string GetText()
        {
            if (text != null) return text;
            else return null;
        }

        public static void IFColor(Label Label,int Flag,Result Response)
        {
            if(Flag==1) //Runes
            {
                if ((string)Label.Content == "NN")
                {
                    Label.Foreground = Brushes.Red;
                    Label.FontWeight = FontWeights.UltraBold;
                }
                else
                {
                    Label.Foreground = Brushes.Black;
                    Label.FontWeight = FontWeights.Normal;
                }

            }
            //if(Response.Waste<15)
            //{
            //    //green
            //}
            //if(Response.Waste>15 && Response.Waste < 40)
            //{
            //    //orange
            //}
            //if (Response.Waste > 40)
            //{
            //    //red
            //}

            //if()
          
            
        }
        //public 




    }




}
