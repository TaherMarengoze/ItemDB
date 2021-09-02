
using Interfaces.Models;
using System;
using System.Linq;
using System.Xml.Linq;

namespace XmlDataSource.Serialization
{
    internal class Entity
    {
        internal static XElement Serialize(IItem entity)
        {
            XElement commonNames = new XElement("common");
            foreach (string name in entity.CommonNames)
            {
                commonNames.Add(new XElement("cname", name));
            }

            XElement images = new XElement("images");
            foreach (string image in entity.ImagesFileName)
            {
                images.Add(new XElement("image", image));
            }

            XElement details = new XElement("details");
            if (string.IsNullOrEmpty(entity.Details.SpecsID) == false)
            {
                details.Add(
                new XElement("specs",
                new XAttribute("ID", entity.Details.SpecsID),
                new XAttribute("required", entity.Details.SpecsRequired)));
            }

            if (string.IsNullOrEmpty(entity.Details.SizeGroupID) == false)
            {
                details.Add(
                new XElement("sizeGroup",
                new XAttribute("ID", entity.Details.SizeGroupID),
                new XAttribute("required", entity.Details.SizeRequired)));
            }

            if (string.IsNullOrEmpty(entity.Details.BrandListID) == false)
            {
                details.Add(
                new XElement("brandList",
                new XAttribute("ID", entity.Details.BrandListID),
                new XAttribute("required", entity.Details.BrandRequired)));
            }

            if (string.IsNullOrEmpty(entity.Details.EndsListID) == false)
            {
                details.Add(
                new XElement("endsList",
                new XAttribute("ID", entity.Details.EndsListID),
                new XAttribute("required", entity.Details.EndsRequired)));
            }

            XElement draftItem =
                new XElement("item",
                new XAttribute("itemID", entity.ItemID),
                new XAttribute("order", "0"),
                    new XElement("names",
                        new XElement("base", entity.BaseName),
                        new XElement("display", entity.DisplayName),
                        commonNames),
                    new XElement("description", entity.Description),
                    images,
                    details,
                    new XElement("uom", entity.UoM));

            return draftItem;
        }

        internal static XElement Serialize(ISpecs specs)
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

        internal static XElement Serialize(ISizeGroup group)
        {
            XElement altIdList = new XElement("altLists");
            group.AltIdList?.ForEach(id => altIdList.Add(new XElement("listID", id)));

            XElement draftSizeGroup =
                new XElement("group",
                new XAttribute("groupID", group.ID),
                new XAttribute("groupName", group.Name),
                    new XElement("defaultListID", group.DefaultListID),
                    altIdList,
                    new XElement("customSizeDataID", group.CustomSize));

            return draftSizeGroup;
        }

        internal static XElement SerializeCategory(IItem entity)
        {
            return
                new XElement("category",
                    new XAttribute("catID", entity.CatID),
                    new XAttribute("name", entity.CatName));
        }

        internal static XElement SerializeSize(IFieldList entity)
        {
            XElement draftFieldList =
                new XElement("sizeList",
                new XAttribute("listID", entity.ID),
                new XAttribute("name", entity.Name),
                    new XElement("sizes", entity.List
                        .Select(entry => new XElement("size", entry))));

            return draftFieldList;
        }

        internal static XElement SerializeBrand(IFieldList entity)
        {
            XElement draftFieldList =
                new XElement("brandList",
                new XAttribute("listID", entity.ID),
                new XAttribute("name", entity.Name),
                    new XElement("brands", entity.List
                        .Select(entry => new XElement("brand", entry))));

            return draftFieldList;
        }

        internal static XElement SerializeEnd(IFieldList entity)
        {
            XElement draftFieldList =
                new XElement("endsList",
                new XAttribute("listID", entity.ID),
                new XAttribute("name", entity.Name),
                    new XElement("ends", entity.List
                        .Select(entry => new XElement("end", entry))));

            return draftFieldList;
        }
    }
}