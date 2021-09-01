
using Interfaces.Models;
using System.Xml.Linq;

namespace XmlDataSource.Serialization
{
    internal class EntitySerializer
    {
        public static XElement Serialize(ISpecs specs)
        {
            XElement specsXElement =
                new XElement("specs",
                new XAttribute("specsID", specs.ID),
                new XAttribute("name", specs.Name),
                new XAttribute("textPattern", specs.TextPattern));

            foreach (ISpecsItem spec in specs.SpecItems)
            {
                XElement specItem =
                    new XElement("specsItem",
                    new XAttribute("index", spec.Index),
                    new XAttribute("name", spec.Name),
                    new XAttribute("valuePattern", spec.ValuePattern));

                if (spec.ListEntries != null)
                {
                    XElement speclist = new XElement("list");

                    foreach (ISpecListEntry entry in spec.ListEntries)
                    {
                        speclist.Add(
                            new XElement("entry",
                            new XAttribute("valId", entry.ValueID),
                                new XElement("val") { Value = entry.Value },
                                new XElement("disp") { Value = entry.Display }));
                    }

                    specItem.Add(speclist);
                }
                else
                {
                    specItem.Add(new XElement("custom") { Value = spec.CustomInputID });
                }

                specsXElement.Add(specItem);
            }

            return specsXElement;
        }
    }
}