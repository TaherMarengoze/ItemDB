using System.Collections.Generic;

namespace UserInterface.Interfaces
{
    using Enums;

    public interface ISpec
    {

        int Index { get; set; }

        string Name { get; set; }

        string ValuePattern { get; set; }

        SpecType SpecType { get; }

        List<ISpecListEntry> ListEntries { get; }

        string CustomInputID { get; }

        List<ISpecListEntry> CopyEntries();

        void AddEntries(List<ISpecListEntry> entries);

        void SetCustomId(string id);
    }
}