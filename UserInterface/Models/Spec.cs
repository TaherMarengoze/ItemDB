using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace UserInterface.Models
{
    using Enums;
    using Interfaces;

    public class Spec : ISpec
    {
        public Spec()
        {
            ListEntries = new List<ISpecListEntry>();
        }

        public Spec(XElement specsItemTypeElement)
        {
            if (specsItemTypeElement.Name.LocalName == "list")
            {
                ListEntries =
                    (from entry in specsItemTypeElement.Descendants("entry")
                     select new SpecListEntry()
                     {
                         ValueID = (int)entry.Attribute("valId"),
                         Value = entry.Element("val").Value,
                         Display = entry.Element("disp").Value
                     }).ToList<ISpecListEntry>();
                SpecType = SpecType.List;
            }
            else
            {
                CustomInputID = specsItemTypeElement.Value;
                SpecType = SpecType.Custom;
            }
        }

        public int Index { get; set; }

        public string Name { get; set; }

        public string ValuePattern { get; set; } = "{val}";

        public SpecType SpecType { get; private set; }

        public List<ISpecListEntry> ListEntries { get; private set; } = new List<ISpecListEntry>();

        public string CustomInputID { get; private set; }

        public List<ISpecListEntry> CopyEntries()
        {
            List<ISpecListEntry> copyList = new List<ISpecListEntry>();

            foreach (ISpecListEntry entry in ListEntries)
            {
                copyList.Add(entry?.CopyEntry());
            }
            return copyList;
        }

        public void AddEntries(List<ISpecListEntry> entries)
        {
            ListEntries.Clear();
            ListEntries.AddRange(entries);
            SpecType = SpecType.List;
            CustomInputID = null;
        }

        public void SetCustomId(string id)
        {
            CustomInputID = id;
            SpecType = SpecType.Custom;
            ListEntries = null;
        }

    }
}