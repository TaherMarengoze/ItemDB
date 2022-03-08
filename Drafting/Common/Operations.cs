using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Enums;

namespace Controllers.Common
{
    internal static class Operations
    {
        internal static InputStatus GetInputStatus(string value,
            IEnumerable<string> existingList = null, string oldValue = null)
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
    }
}
