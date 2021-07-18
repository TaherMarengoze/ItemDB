using System;

namespace UserInterface.Models
{
    public class ItemCheckList
    {
        private bool _itemIdGiven;
        private bool _itemIdUnique;
        private bool _catIdGiven;
        private bool _catNameGiven;
        private bool _baseNameGiven;
        private bool _displayNameGiven;
        private bool _uomGiven;

        public delegate void CheckArgs();
        public event CheckArgs OnComplete;
        public event CheckArgs OnIncomplete;

        public bool ItemIdGiven
        {
            get => _itemIdGiven;
            internal set
            {
                _itemIdGiven = value;
                CheckCompletion();
            }
        }
        public bool ItemIdUnique
        {
            get => _itemIdUnique;
            internal set
            {
                _itemIdUnique = value;
                CheckCompletion();
            }
        }
        public bool CatIdGiven
        {
            get => _catIdGiven;
            internal set
            {
                _catIdGiven = value;
                CheckCompletion();
            }
        }
        public bool CatNameGiven
        {
            get => _catNameGiven;
            internal set
            {
                _catNameGiven = value;
                CheckCompletion();
            }
        }
        public bool BaseNameGiven
        {
            get => _baseNameGiven;
            internal set
            {
                _baseNameGiven = value;
                CheckCompletion();
            }
        }
        public bool DisplayNameGiven
        {
            get => _displayNameGiven;
            internal set
            {
                _displayNameGiven = value;
                CheckCompletion();
            }
        }
        public bool UomGiven
        {
            get => _uomGiven;
            internal set
            {
                _uomGiven = value;
                CheckCompletion();
            }
        }

        private void CheckCompletion()
        {
            if (_itemIdGiven &&
                _itemIdUnique &&
                _catIdGiven &&
                _catNameGiven &&
                _baseNameGiven &&
                _displayNameGiven &&
                _uomGiven)
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
