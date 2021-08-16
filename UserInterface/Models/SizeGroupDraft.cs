
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace UserInterface.Models
{
    public class SizeGroupDrafter
    {

        /// <summary>
        /// Constructor for new draft.
        /// </summary>
        public SizeGroupDrafter()
        {
            DraftSizeGroup = new SizeGroup();
        }

        /// <summary>
        /// Constructor for modified draft.
        /// </summary>
        /// <param name="group"></param>
        public SizeGroupDrafter(SizeGroup group)
        {
            // Save the reference of original SizeGroup object to reference it later if editing was canceled
            DraftSizeGroup = group;

            // Copy data in SizeGroup object being edited to temporary fields
            groupID = group.ID;
            groupName = group.Name;
            groupDefaultListID = group.DefaultListID;
            groupAltList = group.AltIdList.ToList();
            groupCustomSizeID = group.CustomSize;
        }
        

        /// <summary>
        /// Stores the reference of the draft <see cref="SizeGroup"/> object.
        /// </summary>
        public SizeGroup DraftSizeGroup { get; private set; }

        public XElement DraftGroupXElement { get; set; }

        #region SizeGroup Object Value Holder Fields
        public string groupID;
        public string groupName;
        public string groupDefaultListID;
        public List<string> groupAltList;
        public string groupCustomSizeID;
        #endregion

        public bool HasAltList { get; set; }
        public bool HasCustomSize { get; set; }


        public void ConfirmChanges()
        {
            DraftSizeGroup.ID = groupID;
            DraftSizeGroup.Name = groupName;
            DraftSizeGroup.DefaultListID = groupDefaultListID;
            DraftSizeGroup.AltIdList = HasAltList ? groupAltList : null;
            DraftSizeGroup.CustomSize = HasCustomSize ? groupCustomSizeID : null;

            //Save Changes in XElement object in case of edit
            if (DraftGroupXElement != null)
            {
                XElement xAltList = new XElement("altLists");
                groupAltList?.ForEach(id => xAltList.Add(new XElement("listID", id)));

                DraftGroupXElement.SetAttributeValue("groupID", groupID);
                DraftGroupXElement.SetAttributeValue("groupName", groupName);
                DraftGroupXElement.SetElementValue("defaultListID", groupDefaultListID);
                DraftGroupXElement.Element("altLists").ReplaceWith(xAltList);
                DraftGroupXElement.SetElementValue("customSizeDataID", groupCustomSizeID);
            }
            else
            {
                //DraftGroupXElement = NewXElement();
            }

        }

        private XElement NewXElement()
        {
            XElement xAltList = new XElement("altLists");
            DraftSizeGroup.AltIdList?.ForEach(id => xAltList.Add(new XElement("listID", id)));

            return
                new XElement("group",
                new XAttribute("groupID", DraftSizeGroup.ID),
                new XAttribute("groupName", DraftSizeGroup.Name),
                    new XElement("defaultListID", DraftSizeGroup.DefaultListID),
                    /*xAltList*/
                    new XElement("altLists", DraftSizeGroup.AltIdList != null ?
                    (from id in DraftSizeGroup.AltIdList select new XElement("listID", id)) : null),
                    new XElement("customSizeDataID", DraftSizeGroup.CustomSize));
        }

        public bool IsModified()
        {
            bool idChanged = DraftSizeGroup.ID != groupID;
            bool nameChanged = DraftSizeGroup.Name != groupName;
            bool defaultListIdChanged = DraftSizeGroup.DefaultListID != groupDefaultListID;
            bool altListChanged = IsAltSizeListModified();
            bool customSizeIdChanged = IsCustomSizeIdModified();

            return idChanged || nameChanged || defaultListIdChanged || altListChanged || customSizeIdChanged;
        }

        public bool IsAltSizeListModified()
        {
            bool modified = false;

            bool savedEmpty = DraftSizeGroup.AltIdList == null;
            bool includeDraft = HasAltList;
            bool draftEmpty = groupAltList == null;

            bool expr1 = savedEmpty && includeDraft && !draftEmpty;
            bool expr2 = !savedEmpty && (!includeDraft || draftEmpty);
            bool expr3 = false;

            if (!savedEmpty && !draftEmpty)
            {
                List<string> listA = (from item in DraftSizeGroup.AltIdList orderby item select item).ToList();
                List<string> listB = (from item in groupAltList orderby item select item).ToList();

                expr3 = !listA.SequenceEqual(listB);
            }
            modified = expr1 || expr2 || expr3;
            
            return modified;
        }

        public bool IsCustomSizeIdModified()
        {
            bool modified = false;

            bool savedEmpty = DraftSizeGroup.CustomSize == null || DraftSizeGroup.CustomSize == string.Empty;
            bool includeDraft = HasCustomSize;
            bool draftEmpty = groupCustomSizeID == null || groupCustomSizeID == string.Empty;

            bool expr1 = savedEmpty && includeDraft && !draftEmpty;
            bool expr2 = !savedEmpty && (!includeDraft || draftEmpty);
            bool expr3 = false;

            if (!savedEmpty && !draftEmpty)
            {
                expr3 = DraftSizeGroup.CustomSize != groupCustomSizeID;
            }

            modified = expr1 || expr2 || expr3;
            return modified;
        }
    }
}
