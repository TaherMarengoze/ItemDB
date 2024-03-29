﻿using System;
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
                    bool isValidChar = true; // valid characters check, a function with regex will be provided later

                    if (isValidChar)
                    {
                        if (isNotAsEdit)
                            status = InputStatus.Valid;
                        else
                            status = InputStatus.Duplicate;
                    }
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

        internal static InputStatus Validate(this InputStatus source,
            params InputStatus[] allowedStatus)
        {
            return allowedStatus.Contains(source) ? InputStatus.Valid : source;
        }

        internal static bool IsChanged(string value, string oldValue = null)
        {
            return value != null && value != oldValue;
        }

        internal static bool IsChanged(List<string> newList,
            List<string> oldList = null)
        {
            if (newList == null)
                return false;

            if (oldList == null)
                return !(newList == null);

            // compare elements count
            if (newList.Count != oldList.Count)
                return true;

            return !newList.SequenceEqual(oldList);
        }

        internal static bool IsChanged(ItemDetailInput value, string oldId = null, bool oldRequired = false)
        {
            if (value is null)
                return false;

            if (value.Id != oldId || (value.Required != oldRequired))
                return true;

            return false;
        }
    }
}
