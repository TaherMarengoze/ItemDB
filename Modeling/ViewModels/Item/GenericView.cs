using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Models;

namespace Modeling.ViewModels.Item
{
    public class GenericView
    {
        public GenericView(IItem item)
        {
            CatID = item.CatID;
            CatName = item.CatName;
            ID = item.ItemID;
            //BaseName = item.BaseName;
            DisplayName = item.DisplayName;

            SpecsID = FormatDetails(item.Details.SpecsID,
                                    item.Details.SpecsRequired);

            SizeGroupID = FormatDetails(item.Details.SizeGroupID,
                                        item.Details.SizeRequired);

            BrandsID = FormatDetails(item.Details.BrandListID,
                                     item.Details.BrandRequired);

            EndsID = FormatDetails(item.Details.EndsListID,
                                   item.Details.EndsRequired);
        }

        public string CatID { get; }

        [DisplayName("Category")]
        public string CatName { get; }

        [DisplayName("Item ID")]
        public string ID { get; }

        [DisplayName("Name")]
        public string DisplayName { get; }

        [DisplayName("Specs ID")]
        public string SpecsID { get; }

        [DisplayName("Size Group ID")]
        public string SizeGroupID { get; }

        [DisplayName("Brands ID")]
        public string BrandsID { get; }

        [DisplayName("Ends ID")]
        public string EndsID { get; }

        public override string ToString()
        {
            return string.Format(
                "ID: {0}, Name: {1} (CatID: {2})"
                , ID.PadRight(5), Truncate(/*Base*/DisplayName, 24).PadRight(25), CatID);
        }

        private string Truncate(string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "~";
        }

        private string FormatDetails(string detailValue, bool isDetailRequired)
        {
            return string.Format("{0}{1}"
                , detailValue, isDetailRequired ? "*" : "");
        }
    }
}