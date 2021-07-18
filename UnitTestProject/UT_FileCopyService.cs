using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserInterface.Operation;

namespace UnitTestProject
{
    //[TestClass]
    public class UT_FileCopyService
    {
        //[TestMethod]
        public void Test_CopyFile()
        {
            string testSource =
                @"C:\Users\taher.marengoze\Desktop\Test File Copy\source\TestWB.xlsx";

            string testDestination =
                @"C:\Users\taher.marengoze\Desktop\Test File Copy\destination\DestWB.xlsx";

            bool testOverwite = true;

            CopyService.CopyFiles(testSource, testDestination, testOverwite);


        }
    }
}
