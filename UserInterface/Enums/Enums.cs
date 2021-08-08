namespace UserInterface.Enums
{
    public enum EntryMode
    {
        View,
        New,
        Edit
    }

    public enum ContextEntity
    {
        Items = 1,
        Specs = 2,
        SizeGroups = 4,
        Sizes = 8,
        Brands = 16,
        Ends = 32,
        CustomSpecs = 64,
        CustomSizes = 128
    }

    public enum FieldType
    {
        SIZE,
        BRAND,
        ENDS
    }

    public enum SpecType
    {
        List = 1,
        Custom = 2
    }

    public enum ShiftDirection
    {
        UP = -1,
        DOWN = +1
    }

    
}