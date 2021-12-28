namespace CoreLibrary.Enums
{
    /// <summary>
    /// A flag for data entry mode.
    /// </summary>
    public enum EntryMode
    {
        /// <summary>
        /// View mode; no data can be changed.
        /// </summary>
        View,
        /// <summary>
        /// New mode; can add new data.
        /// </summary>
        New,
        /// <summary>
        /// Edit mode; can change existing data, canceling will revert to original object data.
        /// </summary>
        Edit
    }

    public enum InputStatus
    {
        /// <summary>
        /// Input is given and meets the required criteria.
        /// </summary>
        Valid,
        /// <summary>
        /// Input is given and meets the required criteria but is not unique.
        /// </summary>
        Duplicate,
        /// <summary>
        /// Input is not given.
        /// </summary>
        Blank,
        /// <summary>
        /// Input is given but do not meet the required criteria.
        /// </summary>
        Invalid
    }

    /// <summary>
    /// A flag indicating the type of context.
    /// </summary>
    public enum ContextEntity
    {
        /// <summary>
        /// Items context.
        /// </summary>
        Items = 1,
        /// <summary>
        /// Specs context.
        /// </summary>
        Specs = 2,
        /// <summary>
        /// Size groups context.
        /// </summary>
        SizeGroups = 4,
        /// <summary>
        /// Size lists context.
        /// </summary>
        Sizes = 8,
        /// <summary>
        /// Brand lists context
        /// </summary>
        Brands = 16,
        /// <summary>
        /// Ends lists context.
        /// </summary>
        Ends = 32,
        /// <summary>
        /// Custom specs context.
        /// </summary>
        CustomSpecs = 64,
        /// <summary>
        /// Custom sizes context.
        /// </summary>
        CustomSizes = 128
    }

    /// <summary>
    /// A flag indicating the type of item field.
    /// </summary>
    public enum FieldType
    {
        /// <summary>
        /// Size list field of an item.
        /// </summary>
        SIZE,
        /// <summary>
        /// Brand list field of an item.
        /// </summary>
        BRAND,
        /// <summary>
        /// Ends list field of an item
        /// </summary>
        ENDS
    }

    /// <summary>
    /// A flag indicating the type of a specs item.
    /// </summary>
    public enum SpecType
    {
        /// <summary>
        /// List type specs item.
        /// </summary>
        List = 1,
        /// <summary>
        /// Custom input type specs item.
        /// </summary>
        Custom = 2
    }

    /// <summary>
    /// A flag for shifting direction for moving an item within list.
    /// </summary>
    public enum ShiftDirection
    {
        /// <summary>
        /// Flag for up direction.
        /// </summary>
        UP = -1,
        /// <summary>
        /// Flag for down direction.
        /// </summary>
        DOWN = +1
    }

    
}