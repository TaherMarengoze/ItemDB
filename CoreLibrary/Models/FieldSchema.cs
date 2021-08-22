using System.Xml.Linq;

namespace CoreLibrary.Models
{
    using Interfaces;

    public class FieldSchema : ISchema
    {
        public FieldSchema(string listId, string listName, string listParentName, string listChildName, string childGroupName)
        {
            ListId = listId;
            ListName = listName;
            ListParent = listParentName;
            ListChild = listChildName;
            ChildGroup = childGroupName;
        }

        public FieldSchema(Enums.FieldType field)
        {
            Factory.Delegators.FieldActionCallback(field,
                SizeSchema, BrandSchema, EndsSchema);
        }

        public XName ListId { get; private set; }

        public XName ListName { get; private set; }

        public XName ListParent { get; private set; }

        public XName ListChild { get; private set; }

        public XName ChildGroup { get; private set; }

        private void SizeSchema()
        {
            ListId = "listID";
            ListName = "name";
            ListParent = "sizeList";
            ListChild = "size";
            ChildGroup = "sizes";
        }

        private void BrandSchema()
        {
            ListId = "listID";
            ListName = "name";
            ListParent = "brandList";
            ListChild = "brand";
            ChildGroup = "brands";
        }

        private void EndsSchema()
        {
            ListId = "listID";
            ListName = "name";
            ListParent = "endsList";
            ListChild = "end";
            ChildGroup = "ends";
        }
    }
}
