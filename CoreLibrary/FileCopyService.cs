using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using CoreLibrary.Models;

namespace CoreLibrary.Operation
{
    public static class CopyService
    {
        public static int OrdersCount => copyOrders.Count;
        private static List<CopyOrder> copyOrders = new List<CopyOrder>();

        public static void CopyFiles(string source, string target, bool overwrite)
        {
            string sourceFileName = Path.GetFileName(source);

            try
            {
                if (File.Exists(target)) { File.Copy(source, target, overwrite); }
                else { File.Copy(source, target); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public static bool CreateCopyOrder(string source, string target)
        {
            bool notFound = source != target;
            if (notFound) { copyOrders.Add(new CopyOrder(source, target)); }
            return notFound;
        }

        public static void ExecutePendingCopyOrders()
        {
            foreach (CopyOrder order in copyOrders) { CopyFiles(order.Source, order.Target, true); }
        }
    }
}