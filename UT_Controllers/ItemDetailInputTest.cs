using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using CoreLibrary.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UT_Controllers
{
    [TestClass]
    public class ItemDetailInputTest
    {
        bool SKIP_LOG = false;

        ItemDetailInput input;

        [TestInitialize]
        public void Initialize()
        {
            Initialization.Simulate();
            input = new ItemDetailInput(SetStatusSpecs);
        }

        private void Log(Action loggingActions)
        {
            if (SKIP_LOG)
                return;

            loggingActions.Invoke();
            Console.WriteLine();
        }

        private void LogStatus(InputStatus status, string id, bool required)
        {
            Console.WriteLine("ID: {1}, Required: {2} → Status: {0}",
                status,
                (id ?? "_null").PadRight(5),
                required.ToString().PadRight(5));
        }

        [TestMethod]
        public void TestInputs()
        {
            LogStatus(statusInput, input.Id, input.Required);

            input.Id = null;
            input.Required = true;
            LogStatus(statusInput, input.Id, input.Required);

            input.Id = string.Empty;
            input.Required = false;
            LogStatus(statusInput, input.Id, input.Required);

            input.Id = " ";
            input.Required = false;
            LogStatus(statusInput, input.Id, input.Required);

            input.Id = " ";
            input.Required = true;
            LogStatus(statusInput, input.Id, input.Required);

            input.Id = "ABCXY";
            input.Required = false;
            LogStatus(statusInput, input.Id, input.Required);

            input.Id = "ABCXY";
            input.Required = true;
            LogStatus(statusInput, input.Id, input.Required);
        }

        private InputStatus statusInput;
        

        private void SetStatusSpecs(InputStatus s) => statusInput = s;

        [TestMethod]
        public void MyTestMethod()
        {
            inputStatus = new List<InputStatus>();

            s1 = InputStatus.Blank;
            s2 = InputStatus.Blank;
            s3 = InputStatus.Blank;
            s4 = InputStatus.Blank;
            s5 = InputStatus.Blank;

            inputStatus.Add(s1);
            inputStatus.Add(s2);
            inputStatus.Add(s3);
            inputStatus.Add(s4);
            inputStatus.Add(s5);

            Console.WriteLine("{0} > {1}", string.Join(",", inputStatus), IsValidInputs());

            s1 = InputStatus.Valid;
            s2 = InputStatus.Valid;
            s3 = InputStatus.Valid;
            s4 = InputStatus.Valid;
            s5 = InputStatus.Valid;

            Console.WriteLine("{0} > {1}", string.Join(",", inputStatus), IsValidInputs());
        }

        private List<InputStatus> inputStatus = new List<InputStatus>();

        private InputStatus s1 = InputStatus.Blank;
        private InputStatus s2 = InputStatus.Blank;
        private InputStatus s3 = InputStatus.Blank;
        private InputStatus s4 = InputStatus.Blank;
        private InputStatus s5 = InputStatus.Blank;

        private bool IsValidInputs()
        {
            //InputStatus[] inputStatus = { s1, s2, s3, s4, s5 };

            return inputStatus.All(status => status == InputStatus.Valid);
        }
    }
}