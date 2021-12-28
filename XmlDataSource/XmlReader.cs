
using Interfaces.Models;
using Interfaces.Operations;
using Modeling.DataModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace XmlDataSource
{
    /// <summary>
    /// Reads and deserializes the XML data from <see cref="DataDocuments"/>.
    /// </summary>
    public class XmlReader : IDataReader
    {
        private readonly DataDocuments dataDocs;

        public XmlReader(DataDocuments documents)
        {
            dataDocs = documents;
        }

        public IEnumerable<IItem> GetItems()
        {
            return
                from item in dataDocs.Items.Descendants("item")
                let names = item.Element("names")
                let commonNames = names.Element("common")
                let images = item.Element("images")
                let details = item.Element("details")
                let detailsSpecs = details.Element("specs")
                let detailsSize = details.Element("sizeGroup")
                let detailsBrand = details.Element("brandList")
                let detailsEnds = details.Element("endsList")
                select new Item()
                {
                    CatID = item.Parent.Attribute("catID").Value,
                    CatName = item.Parent.Attribute("name").Value,
                    ItemID = item.Attribute("itemID").Value,
                    BaseName = names.Element("base").Value,
                    DisplayName = names.Element("display").Value,
                    CommonNames =
                      commonNames.HasElements ? commonNames.Elements("cname").Select(cnm => cnm.Value).ToList() : null,

                    Description = item.Element("description").Value,
                    ImagesFileName =
                      images.HasElements ? images.Elements("image").Select(img => img.Value).ToList() : null,

                    Details = new ItemDetails()
                    {
                        SpecsID = detailsSpecs?.Attribute("ID").Value,
                        SpecsRequired =
                          detailsSpecs != null ? (bool)detailsSpecs.Attribute("required") : false,

                        SizeGroupID = detailsSize?.Attribute("ID").Value,
                        SizeRequired =
                          detailsSize != null ? (bool)detailsSize.Attribute("required") : false,

                        BrandListID = detailsBrand?.Attribute("ID").Value,
                        BrandRequired =
                          detailsBrand != null ? (bool)detailsBrand.Attribute("required") : false,

                        EndsListID = detailsEnds?.Attribute("ID").Value,
                        EndsRequired =
                          detailsEnds != null ? (bool)detailsEnds.Attribute("required") : false
                    },
                    UoM = item.Element("uom")?.Value
                };
        }

        public IEnumerable<IItemCategory> GetCategories()
        {
            return
                from cat in dataDocs.Items.Descendants("category")
                select new ItemCategory()
                {
                    ID = cat.Attribute("catID").Value,
                    Name = cat.Attribute("name").Value
                };
        }

        public IEnumerable<ISpecs> GetSpecs()
        {
            return
                from specs in dataDocs.Specs.Descendants("specs")
                select new Specs()
                {
                    ID = specs.Attribute("specsID").Value,
                    Name = specs.Attribute("name").Value,
                    TextPattern = specs.Attribute("textPattern").Value,
                    SpecItems = specs.Descendants("specsItem").Select(spec =>
                    new SpecsItem()
                    {
                        Index = (int)spec.Attribute("index"),
                        Name = spec.Attribute("name").Value,
                        ValuePattern = spec.Attribute("valuePattern").Value,
                        ListEntries = GetSpecListEntries(spec),
                        CustomInputID = GetSpecCustomId(spec)
                    }).ToList<ISpecsItem>()
                };
        }
        
        public IEnumerable<ISizeGroup> GetSizeGroups()
        {
            return
                from sg in dataDocs.SizeGroups.Descendants("group")
                let list = sg.Element("altLists").HasElements ?
                    sg.Element("altLists").Elements("listID").Select(l => l.Value).ToList() : null

                let customId = sg.Element("customSizeDataID").Value

                select new SizeGroup()
                {
                    ID = sg.Attribute("groupID").Value,
                    Name = sg.Attribute("groupName").Value,
                    DefaultListID = sg.Element("defaultListID").Value,
                    AltIdList = list,
                    CustomSize = customId != string.Empty ? customId : null
                };
        }

        public IEnumerable<IFieldList> GetSizes()
        {
            return
                from list in dataDocs.Sizes.Descendants("sizeList")
                select new SizeList()
                {
                    ID = list.Attribute("listID").Value,
                    Name = list.Attribute("name").Value,
                    List = new ObservableCollection<string>
                        (list.Descendants("size")
                        .Select(entry => entry.Value).ToList())
                };
        }

        public IEnumerable<IFieldList> GetBrands()
        {
            return
                from brands in dataDocs.Brands.Descendants("brandList")
                select new BrandList()
                {
                    ID = brands.Attribute("listID").Value,
                    Name = brands.Attribute("name").Value,
                    List = new ObservableCollection<string>
                        (brands.Descendants("brand")
                        .Select(brand => brand.Value).ToList())
                };
        }

        public IEnumerable<IFieldList> GetEnds()
        {
            return
                from ends in dataDocs.Ends.Descendants("endsList")
                select new EndList()
                {
                    ID = ends.Attribute("listID").Value,
                    Name = ends.Attribute("name").Value,
                    List = new ObservableCollection<string>
                        (ends.Descendants("end")
                        .Select(end => end.Value).ToList())
                };
        }

        public IEnumerable<string> GetImageNames()
        {
            return
                dataDocs.Items.Descendants("image")
                .Select(f => System.IO.Path.GetFileNameWithoutExtension(f.Value));
        }

        public IEnumerable<string> GetCustomSizes()
        {
            return
                dataDocs.CustomSizes.Descendants("customSizeData")
                .Select(csz => csz.Attribute("dataId").Value);
        }

        public IEnumerable<string> GetCustomSpecs()
        {
            return
                dataDocs.CustomSpecs.Descendants("customSpecData")
                .Select(csp => csp.Attribute("dataId").Value);
        }

        private IEnumerable<ISpecListEntry> GetSpecListEntries(XElement spec)
        {
            XName name = ((XElement)spec.FirstNode).Name;
            IEnumerable<ISpecListEntry> entries = null;

            if (name.LocalName == "list")
            {
                entries = spec.Descendants("entry").Select(entry => new SpecListEntry()
                {
                    ValueID = (int)entry.Attribute("valId"),
                    Value = entry.Element("val").Value,
                    Display = entry.Element("disp").Value
                });
            }

            return entries;
        }

        private string GetSpecCustomId(XElement spec)
        {
            XName name = ((XElement)spec.FirstNode).Name;
            string customId = null;

            if (name.LocalName == "custom")
            {
                customId = spec.Element("custom").Value;
            }

            return customId;
        }
    }
}