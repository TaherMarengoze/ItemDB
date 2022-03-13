using System;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Enums;

namespace Controllers.Common
{
    internal static class Operations
    {
        internal static InputStatus GetInputStatus(string value,
            string oldValue = null, IEnumerable<string> existingList = null)
        {
            InputStatus status;
            if (string.IsNullOrWhiteSpace(value))
            {
                status = InputStatus.Blank;
            }
            else
            {
                // check for duplicate
                bool isDuplicate = existingList?.Contains(value) ?? false;
                bool isNotAsEdit = oldValue == null || value != oldValue;

                if (isDuplicate)
                {
                    if (isNotAsEdit)
                        status = InputStatus.Duplicate;
                    else
                        status = InputStatus.Valid;
                }
                else
                {
                    bool isValidChar = true; // valid characters check

                    if (isValidChar)
                        status = InputStatus.Valid;
                    else
                        status = InputStatus.Invalid;
                }
            }

            return status;
        }

        internal static InputStatus GetInputStatus(IEnumerable<string> list)
        {
            InputStatus status;

            if (list == null || list.Any(s => string.IsNullOrWhiteSpace(s)))
            {
                status = InputStatus.Invalid;
            }
            else
            {
                if (list.Any())
                {
                    if (list.Count() != list.Distinct().Count())
                    {
                        status = InputStatus.Duplicate;
                    }
                    else
                    {
                        status = InputStatus.Valid;
                    }
                }
                else
                {
                    status = InputStatus.Blank;
                }
            }

            return status;
        }

        internal static bool IsChanged(string value, string oldValue = null)
        {
            return value != null && value != oldValue;
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

    }
}
