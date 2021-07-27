using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UserInterface
{
    using Interfaces;
    using Models;

    public class SpecsRepoX : ISpecsModifier
    {
        public void AddSpecs(Specs specs)
        {
            XElement xSpecs = SerializeSpecs(specs);
            Program.xDataDocs.Specs.Root.Add(xSpecs);
        }

        private XElement SerializeSpecs(Specs specs)
        {
            //XElement specItem;
            //XElement speclist;
            XElement draftSpecs =
                new XElement("specs",
                new XAttribute("specsID", specs.ID),
                new XAttribute("name", specs.Name),
                new XAttribute("textPattern", specs.TextPattern));

            foreach (Spec spec in specs.SpecItems)
            {
                XElement specItem =
                    new XElement("specsItem",
                    new XAttribute("index", spec.Index),
                    new XAttribute("name", spec.Name),
                    new XAttribute("valuePattern", spec.ValuePattern));

                if (spec.SpecType == Enums.SpecType.List)
                {
                    XElement speclist = new XElement("list");

                    foreach (SpecListEntry entry in spec.ListEntries)
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

                draftSpecs.Add(specItem);
            }

            return draftSpecs;
        }
    }
}