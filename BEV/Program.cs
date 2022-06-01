using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace BEV
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                OperatingSystem OS = System.Environment.OSVersion;
                if (OS.Version.Major < 6) 
                {
                   
                    MessageBox.Show(null, "Warning : BEV Not Support this Platform " + 
                        "\nBEV 3.0 Only Supports Windows Vista Platforms" + "\n" + "\n" + "Platform: " + OS.Platform.ToString()
                        + "\n" + "Platform Details: " + OS.VersionString, "BEV 3.0 Can not Loading", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (OS.Version.Major >= 6)
                {
                    try
                    {

                        //Process MyProcess = Process.GetCurrentProcess();
                        //MyProcess.ProcessorAffinity = (IntPtr)1;
                    }
                    catch (Exception err)
                    {

                    }

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                    
                }

            }
            catch (Exception err)
            {
                
                
            }
           
        }
    }
}
