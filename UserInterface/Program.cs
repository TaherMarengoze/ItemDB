using System;
using System.IO;
using System.Windows.Forms;

namespace UserInterface
{
    using Interfaces;
    using Models;

    static class Program
    {
        /// <summary>
        /// The <see cref="FilePathProcessor"/> global instance.
        /// </summary>
        public static FilePathProcessor fpp;
        public static ISourceReader reader;
        public static IModifier itemModifier;
        public static ISpecsRepo specsRepo;
        public static IFieldRepos sizesRepo;
        public static IFieldManipulator sizeManipulator;
        public static ISourceContext context = new XmlContext();
        public static XDataDocuments xDataDocs;

        public static bool TestAutoLoad = true;
        public static string TestPath =
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

        #region WindowsAPI
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SystemParametersInfo(int uAction, int uParam, int lpvParam, int fuWinIni);
        private const int SPI_SETKEYBOARDCUES = 4107; //100B
        private const int SPIF_SENDWININICHANGE = 2;
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AlwaysShowAccelerator(false);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.Main());
        }

        /// <summary>
        /// Sets the accelerator underlines to be always shown or hidden and shown on ALT key is press.
        /// </summary>
        /// <param name="show">True for always shown and False for hidden.</param>
        private static void AlwaysShowAccelerator(bool show)
        {
            SystemParametersInfo(SPI_SETKEYBOARDCUES, 0, 1, 0);
        }

        public static string TestSave(this string filePath)
        {
            //Get file name from path
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            return filePath.Replace(fileName, $"{fileName}_Save");
        }
    }
}
