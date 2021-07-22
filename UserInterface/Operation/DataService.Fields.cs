namespace UserInterface.Operation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using UserInterface.Factory;
    using UserInterface.Interfaces;
    using UserInterface.Models;
    using UserInterface.Enums;

    public static partial class DataService
    {
        /// <summary>
        /// Gets a list containing the ID, Name and Entries for the given field type.
        /// </summary>
        /// <param name="fieldType">The field type is either a Size type, Brand type or Ends type.</param>
        /// <returns></returns>
        public static List<BasicListView> GetFieldLists(FieldType fieldType)
        {
            return (List<BasicListView>)
                Delegators.FieldFunctionCallback(fieldType,
                    delegate { return GetSizes(); },
                    delegate { return GetBrands(); },
                    delegate { return GetEnds(); });
        }

        public static void ReadFieldXDocument(FieldType field)
        {
            Delegators.FieldActionCallback(field,
                delegate { repos.SizesList = _GetSizes(Program.xDataDocs.Sizes); },
                delegate { repos.BrandsList = _GetBrands(Program.xDataDocs.Brands); },
                delegate { repos.EndsList = _GetEnds(Program.xDataDocs.Ends); });
        }

        public static void UpdateField(FieldType field) => ReadFieldXDocument(field);

        public static void DeleteFieldList(FieldType field, string listId, XDocument fieldXDoc)
        {
            Delegators.FieldActionCallback(field,
                delegate { DeleteSizeList(listId, fieldXDoc); },
                delegate { DeleteBrandList(listId, fieldXDoc); },
                delegate { DeleteEndsList(listId, fieldXDoc); });
        }

        private static void DeleteFieldListFromXDocument(XDocument fieldXDoc, string listId, XName nodeName)
        {
            XElement deleteFieldList =
                fieldXDoc.Descendants(nodeName)
                .Where(list => list.Attribute("listID").Value == listId).First();

            deleteFieldList.Remove();
        }

        public static object GetFieldListMetadata(FieldType field)
        {
            return Delegators.FieldFunctionCallback(field,
                sizeCallback: SizesBasicView,
                brandCallback: BrandsBasicView,
                endsCallback: delegate { return repos.EndsList.Select(l => new BasicView(l.ID, l.Name)).ToList(); });
        }

        public static List<BasicView> SizesBasicView()
        {
            return
                repos.SizesList
                .Select(l => new BasicView(l.ID, l.Name))
                .ToList();
        }

        public static List<BasicView> BrandsBasicView()
        {
            return
                repos.BrandsList
                .Select(l => new BasicView(l.ID, l.Name))
                .ToList();
        }

        /// <summary>
        /// Adds a new field List to data source.
        /// </summary>
        /// <param name="fieldType">The field type is either a SIZE, BRAND or ENDS.</param>
        /// <param name="fieldListItem">The <see cref="IFieldList"/> object that contains the field list data.</param>
        internal static void AddNewFieldList(FieldType fieldType, IFieldList fieldListItem)
        {
            DataRepos.AddNewFieldList(fieldType, fieldListItem);
            UpdateField(fieldType);
        }

        public static List<BasicListView> GetFieldItems(FieldType fieldType)
        {
            return (List<BasicListView>)
                Delegators.FieldFunctionCallback(fieldType, GetSizes, GetBrands, GetEnds);
        }

        public static List<string> GetFieldListsId(FieldType fieldType)
        {
            return (List<string>)
                Delegators.FieldFunctionCallback(fieldType, null, GetBrandListsId, GetEndsListsId);
        }

        public static ISource DataRepos { get; set; }
            = new XDataIO();
    }
}