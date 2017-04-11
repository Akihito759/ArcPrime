using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcPrime.WPF.Model;
using ArcPrime.WPF.Actions;
using System.Windows.Controls;
using System.IO;

namespace ArcPrime.WPF.Actions
{
    static class Logger
    {
        static string FileName="test.txt";
        static string Patch= Path.Combine(Environment.CurrentDirectory, @"Logs\", FileName);

        static public void Write()
        {
            //FileStream fs = null;
            //try
            //{
            //    fs= new FileStream(FileName, FileMode.CreateNew);
            //    using (Stream)
            //}
            //finally
            //{
            //    if (fs != null) fs.Dispose();
            //}

            string text = Display.GetText();
            if (text != null) text = text.Replace("\n", System.Environment.NewLine);
            System.IO.File.WriteAllText(Patch, text);


        }


    }



}
