using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UserInterface.Models;

namespace UnitTestProject
{
    [TestClass]
    public class SizeGroupDraftUnitTest
    {
        private SizeGroup testGroup;
        private SizeGroupDraft testDraft;

        //[TestMethod]
        public void Test_IsAltSizeListModified()
        {
            bool actModified;
            bool expModified;

            SimEditing();

            //byte scenarioNumber = 7;
            //switch (scenarioNumber)
            //{
            //    case 1:
            //        actModified = Scenario1();
            //        expModified = true;
            //        Assert.AreEqual(actModified, expModified);
            //        break;

            //    case 2:

            //        actModified = Scenario2();
            //        expModified = false;
            //        Assert.AreEqual(actModified, expModified);
            //        break;

            //    case 3:

            //        actModified = Scenario3();
            //        expModified = false;
            //        Assert.AreEqual(actModified, expModified);
            //        break;

            //    case 4:

            //        actModified = Scenario4();
            //        expModified = false;
            //        Assert.AreEqual(actModified, expModified);
            //        break;

            //    case 5:

            //        actModified = Scenario5();
            //        expModified = true;
            //        Assert.AreEqual(actModified, expModified);
            //        break;

            //    case 6:

            //        actModified = Scenario6();
            //        expModified = true;
            //        Assert.AreEqual(actModified, expModified);
            //        break;

            //    case 7:

            //        actModified = Scenario7();
            //        expModified = true;
            //        Assert.AreEqual(actModified, expModified);
            //        break;
            //    default:
            //        break;
            //}

            actModified = Scenario1();
            expModified = true;
            Assert.AreEqual(actModified, expModified);

            actModified = Scenario2();
            expModified = false;
            Assert.AreEqual(actModified, expModified);

            actModified = Scenario3();
            expModified = false;
            Assert.AreEqual(actModified, expModified);

            actModified = Scenario4();
            expModified = false;
            Assert.AreEqual(actModified, expModified);

            actModified = Scenario5();
            expModified = true;
            Assert.AreEqual(actModified, expModified);

            actModified = Scenario6();
            expModified = true;
            Assert.AreEqual(actModified, expModified);

            actModified = Scenario7();
            expModified = true;
            Assert.AreEqual(actModified, expModified);

            actModified = Scenario8A();
            expModified = false;
            Assert.AreEqual(actModified, expModified);

            actModified = Scenario8B();
            expModified = true;
            Assert.AreEqual(actModified, expModified);
        }

        /// <summary>
        /// Simulate editing an existing group.
        /// </summary>
        private void SimEditing()
        {
            testGroup = new SizeGroup() { ID = "TEST", Name = "Test Size Group", DefaultListID = "TEST0" };
            testDraft = new SizeGroupDraft(testGroup);
        }

        private bool Scenario1() // output: modified
        {
            //SIM: no alt list in the group being edit
            testGroup.AltIdList = null;

            //SIM: Checked the AltList Checkbox
            testDraft.HasAltList = true;

            //SIM: Added Alt List with at least one item
            testDraft.AltList = new List<string> { "TEST1" };

            return testDraft.IsAltSizeListModified();
        }

        private bool Scenario2() // output: no change
        {
            //SIM: no alt list in the group being edit
            testGroup.AltIdList = null;

            //SIM: Checked the AltList Checkbox
            testDraft.HasAltList = false;

            //SIM: Added Alt List with at least one item
            testDraft.AltList = new List<string> { "TEST1" };

            return testDraft.IsAltSizeListModified();
        }

        private bool Scenario3() // output: no change
        {
            //SIM: no alt list in the group being edit
            testGroup.AltIdList = null;

            //SIM: Checked the AltList Checkbox
            testDraft.HasAltList = true;

            //SIM: Added Alt List with at least one item
            testDraft.AltList = null;

            return testDraft.IsAltSizeListModified();
        }

        private bool Scenario4() // output: no change
        {
            //SIM: no alt list in the group being edit
            testGroup.AltIdList = null;

            //SIM: Checked the AltList Checkbox
            testDraft.HasAltList = false;

            //SIM: Added Alt List with at least one item
            testDraft.AltList = null;

            return testDraft.IsAltSizeListModified();
        }

        private bool Scenario5() // output: ?
        {
            //SIM: no alt list in the group being edit
            testGroup.AltIdList = new List<string> { "TEST1" };

            //SIM: Checked the AltList Checkbox
            testDraft.HasAltList = false;

            //SIM: Added Alt List with at least one item
            testDraft.AltList = new List<string> { "TEST1" };

            return testDraft.IsAltSizeListModified();
        }

        private bool Scenario6() // output: ? - can't be achived
        {
            //SIM: no alt list in the group being edit
            testGroup.AltIdList = new List<string> { "TEST1" };

            //SIM: Checked the AltList Checkbox
            testDraft.HasAltList = false;

            //SIM: Added Alt List with at least one item
            testDraft.AltList = null;

            return testDraft.IsAltSizeListModified();
        }

        private bool Scenario7() // output: ? - can't be achived
        {
            //SIM: no alt list in the group being edit
            testGroup.AltIdList = new List<string> { "TEST1" };

            //SIM: Checked the AltList Checkbox
            testDraft.HasAltList = true;

            //SIM: Added Alt List with at least one item
            testDraft.AltList = null;

            return testDraft.IsAltSizeListModified();
        }

        private bool Scenario8A() // output: ? - can't be achived ?
        {
            //SIM: no alt list in the group being edit
            testGroup.AltIdList = new List<string> { "TEST1", "TEST2" };

            //SIM: Checked the AltList Checkbox
            testDraft.HasAltList = true;

            //SIM: Added Alt List with at least one item
            testDraft.AltList = new List<string> { "TEST2", "TEST1" };

            return testDraft.IsAltSizeListModified();
        }

        private bool Scenario8B() // output: ? - can't be achived ?
        {
            //SIM: no alt list in the group being edit
            testGroup.AltIdList = new List<string> { "TEST1", "TEST2" };

            //SIM: Checked the AltList Checkbox
            testDraft.HasAltList = true;

            //SIM: Added Alt List with at least one item
            testDraft.AltList = new List<string> { "TEST2", "TEST3" };

            return testDraft.IsAltSizeListModified();
        }

        //[TestMethod]
        public void Test_IsCustomSizeIdModified()
        {
            bool output;
            bool modified;

            SimEditing();

            output = CustomIdVariableScenario("A", false, "B");
            modified = true;
            Assert.AreEqual(expected: modified, actual: output);
        }

        private bool CustomIdVariableScenario(string saved, bool check, string draft)
        {
            testGroup.CustomSize = saved;
            testDraft.HasCustomSize = check;
            testDraft.CustomSizeID = draft;
            return testDraft.IsCustomSizeIdModified();
        }

        /// <summary>
        /// • Inputs:
        /// <para/>Saved Custom Size ID: Has value
        /// <para/>Include Custom Size: Checked
        /// <para/>Draft Custom Size ID: Different value
        /// <para/>==========
        /// <para/>• Output:
        /// <para/>Modified: Yes
        /// </summary>
        /// <returns></returns>
        private bool CustomIdScenario5()
        {
            testGroup.CustomSize = "TEST0";
            testDraft.HasCustomSize = false;
            testDraft.CustomSizeID = "TEST1";
            return testDraft.IsCustomSizeIdModified();
        }

        /// <summary>
        /// • Inputs:
        /// <para/>Saved Custom Size ID: Has value
        /// <para/>Include Custom Size: Checked
        /// <para/>Draft Custom Size ID: Different value
        /// <para/>==========
        /// <para/>• Output:
        /// <para/>Modified: Yes
        /// </summary>
        /// <returns></returns>
        private bool CustomIdScenario4()
        {
            //SIM: Saved 
            testGroup.CustomSize = "TEST0";

            //SIM: Checked the Custom Size Checkbox
            testDraft.HasCustomSize = false;

            //SIM: Selected item from the combobox
            testDraft.CustomSizeID = "TEST1";

            return testDraft.IsCustomSizeIdModified();
        }

        /// <summary>
        /// • Inputs:
        /// <para/>Saved Custom Size ID: Has value
        /// <para/>Include Custom Size: Checked
        /// <para/>Draft Custom Size ID: Same value
        /// <para/>==========
        /// <para/>• Output:
        /// <para/>Modified: Yes
        /// </summary>
        /// <returns></returns>
        private bool CustomIdScenario3()
        {
            testGroup.CustomSize = "TEST0";
            testDraft.HasCustomSize = false;
            testDraft.CustomSizeID = "TEST0";
            return testDraft.IsCustomSizeIdModified();
        }

        /// <summary>
        /// • Inputs:
        /// <para/>Saved Custom Size ID: Has value
        /// <para/>Include Custom Size: Checked
        /// <para/>Draft Custom Size ID: Same value
        /// <para/>==========
        /// <para/>• Output:
        /// <para/>Modified: No
        /// </summary>
        /// <returns></returns>
        private bool CustomIdScenario2()
        {
            //SIM: Saved 
            testGroup.CustomSize = "TEST0";

            //SIM: Checked the Custom Size Checkbox
            testDraft.HasCustomSize = true;

            //SIM: Selected item from the combobox
            testDraft.CustomSizeID = "TEST0";

            return testDraft.IsCustomSizeIdModified();
        }

        /// <summary>
        /// • Inputs:
        /// <para/>Saved Custom Size ID: No value
        /// <para/>Include Custom Size: Checked
        /// <para/>Draft Custom Size ID: Different value
        /// <para/>==========
        /// <para/>• Output:
        /// <para/>Modified: Yes
        /// </summary>
        /// <returns></returns>
        private bool CustomIdScenario1()
        {
            testGroup.CustomSize = null;
            testDraft.HasCustomSize = true;
            testDraft.CustomSizeID = "TEST1";
            return testDraft.IsCustomSizeIdModified();
        }
    }
}
