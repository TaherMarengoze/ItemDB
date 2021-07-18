﻿

namespace UserInterface.Models.Validators
{
    public class FieldListValidator
    {
        private bool _validID;
        private bool _validName;
        private bool _validEntries;

        public delegate void CheckArgs();
        public event CheckArgs OnComplete;
        public event CheckArgs OnIncomplete;

        public bool ValidID
        {
            get => _validID; set
            {
                _validID = value;
                CheckCompletion();
            }
        }

        public bool ValidName
        {
            get => _validName; set
            {
                _validName = value;
                CheckCompletion();
            }
        }

        public bool ValidEntries
        {
            get => _validEntries; set
            {
                _validEntries = value;
                CheckCompletion();
            }
        }

        private void CheckCompletion()
        {
            if (_validID &&
                _validName &&
                _validEntries)
            {
                OnComplete?.Invoke();
            }
            else
            {
                OnIncomplete?.Invoke();
            }
        }
    }
}
