using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
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

        internal static bool IsChanged_ItemDetailInput(ItemDetailInput value, ItemDetailInput oldValue = null)
        {
            if (value is null)
                return false;

            if (value.Id != oldValue?.Id || (value.Required != oldValue?.Required))
            {
                return true;
            }

            return false;
        }

        [TestMethod]
        public void Test_IsChanged_DetailInputObject()
        {
            ItemDetailInput inputNew = new ItemDetailInput(null) { Id = "NEWID", Required = true };
            ItemDetailInput inputOld = new ItemDetailInput(null) { Id = "OLDID", Required = true };

            // unchanged
            Assert.AreEqual(false, IsChanged_ItemDetailInput(null, null));
            Assert.AreEqual(false, IsChanged_ItemDetailInput(null));
            Assert.AreEqual(false, IsChanged_ItemDetailInput(null, inputOld));

            // changed → old object not given
            Assert.AreEqual(true, IsChanged_ItemDetailInput(inputNew));

            // both objects given
            // changed
            Assert.AreEqual(true, IsChanged_ItemDetailInput(inputNew, inputOld));

            // unchanged → both objects are similar
            inputNew.Id = "OLDID";
            Assert.AreEqual(false, IsChanged_ItemDetailInput(inputNew, inputOld));

            // changed
            inputNew.Required = false;
            Assert.AreEqual(true, IsChanged_ItemDetailInput(inputNew, inputOld));
        }

        internal static bool IsChanged_ItemDetailInput2(ItemDetailInput value, string oldId = null, bool oldRequired = false)
        {
            if (value is null)
                return false;

            if (value.Id != oldId || (value.Required != oldRequired))
                return true;

            return false;
        }

        [TestMethod]
        public void Test_IsChanged_DetailInputObject2()
        {
            ItemDetailInput input = new ItemDetailInput(null) { Id = "NEWID", Required = true };
            string oldId = null;
            bool oldRequired = false;

            // unchanged
            Assert.AreEqual(false, IsChanged_ItemDetailInput2(null));
            Assert.AreEqual(false, IsChanged_ItemDetailInput2(null, oldId, oldRequired));

            // all parameters given
            // old value parameters not given
            Assert.AreEqual(true, IsChanged_ItemDetailInput2(input)); // → changed
            Assert.AreEqual(false, IsChanged_ItemDetailInput2(input, "NEWID", true)); // → unchanged
            Assert.AreEqual(true, IsChanged_ItemDetailInput2(input, "NEWID", false)); // → changed
            Assert.AreEqual(true, IsChanged_ItemDetailInput2(input, "OLDID", true)); // → changed
            Assert.AreEqual(true, IsChanged_ItemDetailInput2(input, null, true)); // → changed
        }

        private bool IsChanged_(ItemDetailInput value, string oldId = null, bool oldRequired = false)
        {
            if (value is null)
                return false;

            if (value.Id != oldId || (value.Required != oldRequired))
                return true;

            return false;
        }

        private bool IsDraftDetailsChanged()
        {
            ItemDetailInput InputSpecs = new ItemDetailInput(null) { Id = "SPC00" };
            ItemDetailInput InputSizeGroup = new ItemDetailInput(null) { Id = "SZG00" };
            ItemDetailInput InputBrand = new ItemDetailInput(null) { Id = "BRD00" };
            ItemDetailInput InputEnd = new ItemDetailInput(null) { Id = "END00" };

            bool[] detailsChanged = {
                IsChanged_(InputSpecs     , "SPC00", false),
                IsChanged_(InputSizeGroup , "SZG00", false),
                IsChanged_(InputBrand     , "BRD00", false),
                IsChanged_(InputEnd       , "END00", false),
            };

            return detailsChanged.Any(change => change);
        }

        [TestMethod]
        public void Test_IsDraftDetailsChanged()
        {
            Assert.AreEqual(false, IsDraftDetailsChanged());
        }

    }
}


