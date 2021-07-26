namespace UserInterface.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Enums;

    public class Spec
    {
        public Spec()
        {
            ListEntries = new List<SpecListEntry>();
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
                     }).ToList();
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

        public List<SpecListEntry> ListEntries { get; private set; } = new List<SpecListEntry>();

        public string CustomInputID { get; private set; }

        public List<SpecListEntry> CopyEntries()
        {
            List<SpecListEntry> copyList = new List<SpecListEntry>();

            foreach (SpecListEntry entry in ListEntries)
            {
                copyList.Add(entry?.CopyEntry());
            }
            return copyList;
        }

        public void AddEntries(List<SpecListEntry> entries)
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