using System;
using CoreLibrary.Enums;

namespace Controllers
{
    public class ItemDetailInput
    {
        private Action<InputStatus> _statusCallback;
        private bool required;
        private string id;

        public ItemDetailInput(Action<InputStatus> statusCallback)
        {
            _statusCallback = statusCallback;
        }

        public bool Required
        {
            get => required;
            set
            {
                required = value;
                _statusCallback(GetDetailInputs(id, required));
            }
        }

        public string Id
        {
            get => id;
            set
            {
                id = value;
                _statusCallback(GetDetailInputs(id, required));
            }
        }

        private InputStatus GetDetailInputs(string id, bool required)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                if (required)
                {
                    return InputStatus.Invalid;
                }
                else
                {
                    return InputStatus.Blank;
                }

            }
            else
            {
                return InputStatus.Valid;
            }
        }
    }
}