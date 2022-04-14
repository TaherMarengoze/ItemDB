using System;
using System.Collections.Generic;
using System.Linq;
using Controllers.Common;
using CoreLibrary.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UT_Controllers
{
    [TestClass]
    public class ControllerOperationsTest
    {
        [TestMethod]
        public void Test_GetInputStatus_String()
        {
            // case #1: input only is provided
            Assert.AreEqual(InputStatus.Valid, Operations.GetInputStatus("A"));

            // an empty list is provided
            Assert.AreEqual(InputStatus.Valid, Operations.GetInputStatus("A",
                null, new List<string>()));

            // null list is provided; similar to case #1
            Assert.AreEqual(InputStatus.Valid, Operations.GetInputStatus("A",
                null, null));

            // edit ref. is provided same as input and
            // null list is provided (case #1)
            Assert.AreEqual(InputStatus.Duplicate, Operations.GetInputStatus("A",
                "A", null));
        }

        [TestMethod]
        public void Test_GetInputStatus_List()
        {
            var noDuplicates = new List<string> { "test1", "test2" };
            var duplicates = new List<string> { "test1", "test1", "test2" };
            var withBlankItem = new List<string> { "", "test1", "test2" };
            var withNullItem = new List<string> { null, "test1", "test2" };

            Assert.AreEqual(InputStatus.Invalid, Operations.GetInputStatus(null));
            Assert.AreEqual(InputStatus.Blank, Operations.GetInputStatus(new List<string>()));
            Assert.AreEqual(InputStatus.Valid, Operations.GetInputStatus(noDuplicates));
            Assert.AreEqual(InputStatus.Duplicate, Operations.GetInputStatus(duplicates));
            Assert.AreEqual(InputStatus.Invalid, Operations.GetInputStatus(withBlankItem));
            Assert.AreEqual(InputStatus.Invalid, Operations.GetInputStatus(withNullItem));
        }

        [TestMethod]
        public void Test_ValidateStatus()
        {
            Assert.AreEqual(InputStatus.Valid,
                InputStatus.Valid.Validate(
                    InputStatus.Duplicate,
                    InputStatus.Blank));
        }

        internal static bool IsChanged(List<string> newList, List<string> oldList = null)
        {
            if (oldList == null)
                return !(newList == null);

            // compare elements count
            if (newList.Count != oldList.Count)
                return true;

            return !newList.SequenceEqual(oldList);
        }

        [TestMethod]
        public void Test_IsChanged_List()
        {
            List<string> newList = new List<string> { "a", "b" };
            List<string> oldList = new List<string> { "a", "b" };

            // unchanged (both lists are not given)
            Assert.AreEqual(false, IsChanged(null, null));

            // changed (old list is not given)
            Assert.AreEqual(true, IsChanged(newList, null));

            // both lists are given:
            // unchanged (similar)
            Assert.AreEqual(false, IsChanged(newList, oldList));

            // changed (different item counts)
            newList = new List<string> { "a", "b", "c" };
            Assert.AreEqual(true, IsChanged(newList, oldList));

            // changed (same item counts but different elements)
            newList = new List<string> { "a", "c" };
            Assert.AreEqual(true, IsChanged(newList, oldList));

        }


    }
}


