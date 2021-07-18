using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserInterface.Runtime
{
    public static class Test
    {
        public delegate void LoadCallback(string path);

        public static void AutoLoad(LoadCallback callback, string fileName = null)
        {
            if (Program.TestAutoLoad)
            {
                try
                {
                    string path =
                        fileName != null ?
                        Path.Combine(Program.TestPath, fileName) :
                        Program.TestPath + Path.DirectorySeparatorChar;

                    callback(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Test Error Auto-Load file\n\n{ ex.Message }");
                }
            }
        }

        public static void AutoJump(Action callback)
        {
            if (Program.TestAutoLoad)
            {
                try
                {
                    callback();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Test Error Auto-Load file\n\n{ ex.Message }");
                }
            }
        }
    }
}
