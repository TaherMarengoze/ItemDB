using System;
using System.IO;
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

        public static void DoSomething(Action callback)
        {
            if (Program.TestAutoLoad)
            {
                try
                {
                    callback();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Test Error Auto-Load file\n\n{ ex.Message }\n\nSource: { ex.Source }\nTarget: { ex.TargetSite.ReflectedType.FullName }.{ ex.TargetSite.Name }");
                }
            }
        }

        public static void DoSomething(params Action[] callbacks)
        {
            if (Program.TestAutoLoad)
            {
                try
                {
                    foreach (Action callback in callbacks)
                    {
                        callback();
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Test Error Auto-Load file\n\n{ ex.Message }");
                }
            }
        }
    }
}
