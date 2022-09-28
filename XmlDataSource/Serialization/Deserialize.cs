using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Interfaces.Models;
using Modeling.DataModels;


namespace XmlDataSource.IO
{
    internal static class Deserialize
    {
        internal static IItemCategory CategoryXElement(XElement category)
        {
            if (category is null)
                return null;

            return new ItemCategory(category.Attribute("catID").Value,
                                    category.Attribute("name").Value);
        }
        internal static IItem ItemXElement(XElement item)
        {
            /*
            ImagesFileName =
                images.HasElements ? images.Elements("image").Select(img => img.Value).ToList() : null,
            */
            XElement names = item.Element("names");
            XElement commonNames = names.Element("common");
            XElement images = item.Element("images");

            return new Item
            {
                CatID = item.Parent.Attribute("catID").Value,
                CatName = item.Parent.Attribute("name").Value,
                ItemID = item.Attribute("itemID").Value,
                BaseName = names.Element("base").Value,
                DisplayName = names.Element("display").Value,
                CommonNames = commonNames.Elements("cname")
                    .Select(cnm => cnm.Value).ToList(),

                Description = item.Element("description").Value,
                UoM = item.Element("uom")?.Value,
                Details = GetDetails(item),
                ImagesFileName = images.Elements("image")
                    .Select(img => img.Value).ToList(),
            };
        }

        internal static IFieldList SizeXElement(XElement list)
        {
            return new SizeList
            {
                ID = list.Attribute("listID").Value,
                Name = list.Attribute("name").Value,
                List = new ObservableCollection<string>(list
                    .Descendants("size")
                    .Select(entry => entry.Value)
                    /*.ToList()*/)
                // check whether .ToList() is useless
            };
        }

        private static ItemDetails GetDetails(XElement item)
        {
            XElement details = item.Element("details");
            XElement detailsSpecs = details.Element("specs");
            XElement detailsSize = details.Element("sizeGroup");
            XElement detailsBrand = details.Element("brandList");
            XElement detailsEnds = details.Element("endsList");

            return new ItemDetails
            {
                SpecsID = GetDetailID(detailsSpecs),
                SizeGroupID = GetDetailID(detailsSize),
                BrandListID = GetDetailID(detailsBrand),
                EndsListID = GetDetailID(detailsEnds),

                SpecsRequired = GetRequiredState(detailsSpecs),
                SizeRequired = GetRequiredState(detailsSize),
                BrandRequired = GetRequiredState(detailsBrand),
                EndsRequired = GetRequiredState(detailsEnds),
            };
        }

        private static string GetDetailID(XElement detail)
        {
            return detail?.Attribute("ID").Value;
        }

        private static bool GetRequiredState(XElement detail)
        {
            return detail != null && (bool)detail.Attribute("required");
        }
    }
}