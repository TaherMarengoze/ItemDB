using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserInterface.Models;

namespace UserInterface
{
    static class Program
    {
        public static FileProcessor fp;
        public static XDataDocuments xDataDocs;

        public static bool TestAutoLoad = true;
        public static string TestPath =
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SystemParametersInfo(int uAction, int uParam, int lpvParam, int fuWinIni);
        private const int SPI_SETKEYBOARDCUES = 4107; //100B
        private const int SPIF_SENDWININICHANGE = 2;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // always show accelerator underlines
            //SystemParametersInfo(SPI_SETKEYBOARDCUES, 0, 1, 0);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new Forms.ItemEditor());
            //Application.Run(new Forms.ItemViewer());
            //Application.Run(new Forms.SizeGroupEditor());
            //Application.Run(new Forms.SpecsEditor());
            Application.Run(new Forms.Main());
        }

        public static string TestSave(this string filePath)
        {
            //Get file name from path
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            return filePath.Replace(fileName, $"{fileName}_Save");
        }
    }
}
