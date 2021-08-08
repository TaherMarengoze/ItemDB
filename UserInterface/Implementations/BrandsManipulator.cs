using System.Linq;
using System.Xml.Linq;

namespace UserInterface
{
    using Enums;
    using Interfaces;

    public class BrandsManipulator : IFieldManipulator
    {
        private readonly XDocument dataSource;
        private readonly ISchema schema;

        public BrandsManipulator(XDocument source)
        {
            dataSource = source;
            schema = new Models.ListStructure("brand");
        }

        public void AddEntry(string listId, string entry)
        {

        }

        public void EditEntry(string listId, string oldValue, string newValue)
        {
            GetListEntry(listId, oldValue).SetValue(newValue);
        }

        public void DeleteEntry(string listId, string entry)
        {
            GetListEntry(listId, entry).Remove();
        }

        public void MoveEntry(string listId, string entry, ShiftDirection direction)
        {
            XElement moveXItem = GetListEntry(listId, entry);

            switch (direction)
            {
                case ShiftDirection.UP:
                    moveXItem.PreviousNode.AddBeforeSelf(moveXItem);
                    break;
                case ShiftDirection.DOWN:
                    moveXItem.NextNode.AddAfterSelf(moveXItem);
                    break;
            }
            moveXItem.Remove();
        }

        #region Private Members
        /// <summary>
        /// Gets a specific list <see cref="XElement"/> by its ID.
        /// </summary>
        /// <param name="listId">ID of the list.</param>
        /// <returns></returns>
        private XElement GetList(string listId)
        {
            return
                (from list in dataSource.Descendants(schema.ListParent)
                 where list.Attribute(schema.ListId).Value == listId
                 select list).FirstOrDefault();
        }

        /// <summary>
        /// Gets the <see cref="XElement"/> of a specific entry in a list.
        /// </summary>
        /// <param name="listId">List ID containing the entries.</param>
        /// <param name="entryValue">The entry to get.</param>
        /// <returns></returns>
        private XElement GetListEntry(string listId, string entryValue)
        {
            XElement fieldList = GetList(listId);

            return
                (from entry in fieldList.Descendants(schema.ListChild)
                 where entry.Value == entryValue
                 select entry).FirstOrDefault();
        }
        #endregion
    }
}
