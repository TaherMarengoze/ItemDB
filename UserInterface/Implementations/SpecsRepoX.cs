using System.Linq;
using System.Xml.Linq;

namespace UserInterface
{
    using Interfaces;
    using Models;

    public class SpecsRepoX : ISpecsRepo
    {
        private readonly XDocument dataSource;

        public SpecsRepoX(XDocument source)
        {
            dataSource = source;
        }

        public void AddSpecs(ISpecs specs)
        {
            XElement content = SerializeSpecs(specs);
            dataSource.Root.Add(content);
        }

        public ISpecs ReadSpecs(string specsId)
        {
            XElement xSpecs = dataSource.Descendants("specs")
               .Where(sp => sp.Attribute("specsID").Value == specsId)
               .FirstOrDefault();

            Specs specs = new Specs()
            {
                ID = xSpecs.Attribute("specsID").Value,
                Name = xSpecs.Attribute("name").Value,
                TextPattern = xSpecs.Attribute("textPattern").Value,
                SpecItems = xSpecs.Descendants("specsItem")
                .Select(si => new Spec((XElement)si.FirstNode)
                {
                    Index = (int)si.Attribute("index"),
                    Name = si.Attribute("name").Value,
                    ValuePattern = si.Attribute("valuePattern").Value
                }).ToList<ISpec>()
            };

            return specs;
        }

        public void UpdateSpecs(string refId, ISpecs specs)
        {
            XElement content = SerializeSpecs(specs);
            XElement replaceSpecs = GetSpecs(refId);
            replaceSpecs.ReplaceWith(content);
        }

        public void DeleteSpecs(string specsId)
        {
            GetSpecs(specsId).Remove();
        }

        private XElement SerializeSpecs(ISpecs specs)
        {
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

        private XElement GetSpecs(string specsId)
        {
            return
                dataSource.Descendants("specs")
                .Where(sp => sp.Attribute("specsID").Value == specsId)
                .FirstOrDefault();
        }
    }
}