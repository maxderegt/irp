using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project21
{
    class Utilities
    {
        public static void showPopup(string message, Form form)
        {
            //Show new popup message
            PopupForm popup = new PopupForm(message, form);

            //When user presses OK, close the form
            DialogResult dialogresult = popup.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                form.Close();
            }
            popup.Dispose();

            return;
        }


        public static void showPopup(string message, Form form, bool closeForm)
        {
            //Show new popup message
            Console.WriteLine("Placing popupform at location: " + form.Location.ToString());
            PopupForm popup = new PopupForm(message, form);

            //When user presses OK, close the form
            DialogResult dialogresult = popup.ShowDialog();
            if(closeForm)
            {
                if (dialogresult == DialogResult.OK)
                {
                    form.Close();
                }
            }
            popup.Dispose();

            return;
        }

        public static void startNetworkEngine()
        {
            new Thread(runBatchProgram).Start();
        }

        private static void runBatchProgram()
        {
            string filePath = @"C:\Users\max\Desktop\NetworkEngine";
            string fileName = "sim.bat";

            Process proc = null;
            try
            {
                string targetDir = string.Format(filePath);//this is where mybatch.bat lies
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                //proc.StartInfo.WorkingDirectory = Environment.SpecialFolder;
                proc.StartInfo.FileName = fileName;
                //proc.StartInfo.Arguments = string.Format("10");//this is argument
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
        }
    }
}
