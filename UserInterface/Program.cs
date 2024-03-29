﻿
using System;
using System.IO;
using System.Windows.Forms;

namespace UserInterface
{
    static class Program
    {
        public static bool TestAutoLoad = true;
        public static string TestPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

        #region WindowsAPI
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SystemParametersInfo(int uAction, int uParam, int lpvParam, int fuWinIni);
        private const int SPI_SETKEYBOARDCUES = 4107; //100B
        private const int SPIF_SENDWININICHANGE = 2;

        /// <summary>
        /// Sets the accelerator underlines to be always shown or hidden and shown on ALT key is press.
        /// </summary>
        /// <param name="show">True for always shown and False for hidden.</param>
        private static void AlwaysShowAccelerator(bool show)
        {
            if (show)
                SystemParametersInfo(SPI_SETKEYBOARDCUES, 0, 1, 0);
        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AlwaysShowAccelerator(false);

            InitializeAppConfig();
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.Main());
        }

        static void InitializeAppConfig()
        {
            // Set app context to use XML data source
            // which can be changed to database source if applicable
            CoreLibrary.GlobalsX.context = new CoreLibrary.XmlContext();

            // Test New libraries implementation
            AppCore.Globals.context = new XmlDataSource.XmlContext();
        }
    }   
}