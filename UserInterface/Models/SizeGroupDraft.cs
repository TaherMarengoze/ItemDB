
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace UserInterface.Models
{
    public class SizeGroupDraft
    {
        //public event EventHandler<bool> OnGroupValid;

        //private bool groupIdGiven = false;

        private string _groupID;

        /// <summary>
        /// Constructor for new draft.
        /// </summary>
        public SizeGroupDraft()
        {
            SavedGroup = new SizeGroup();
        }

        /// <summary>
        /// Constructor for modified draft.
        /// </summary>
        /// <param name="group"></param>
        public SizeGroupDraft(SizeGroup group)
        {
            SavedGroup = group;

            GroupID = group.ID;
            GroupName = group.Name;
            DefaultListID = group.DefaultListID;
            AltList = group.AltIdList;
            CustomSizeID = group.CustomSize;
        }

        /// <summary>
        /// The SizeGroup draft object that will be modified.
        /// </summary>
        public SizeGroup SavedGroup { get; private set; }

        public XElement DraftGroupXElement { get; set; }

        public string GroupID
        {
            get => _groupID;
            set => _groupID = value;
        }
        public string GroupName { get; set; }
        public string DefaultListID { get; set; }
        public List<string> AltList { get; set; }
        public string CustomSizeID { get; set; }

        public bool HasAltList { get; set; }
        public bool HasCustomSize { get; set; }


        public void SaveData()
        {
            SavedGroup.ID = GroupID;
            SavedGroup.Name = GroupName;
            SavedGroup.DefaultListID = DefaultListID;
            SavedGroup.AltIdList = HasAltList ? AltList : null;
            SavedGroup.CustomSize = HasCustomSize ? CustomSizeID : null;

            //Save Changes in XElement object in case of edit
            if (DraftGroupXElement != null)
            {
                XElement xAltList = new XElement("altLists");
                AltList?.ForEach(id => xAltList.Add(new XElement("listID", id)));

                DraftGroupXElement.SetAttributeValue("groupID", GroupID);
                DraftGroupXElement.SetAttributeValue("groupName", GroupName);
                DraftGroupXElement.SetElementValue("defaultListID", DefaultListID);
                DraftGroupXElement.Element("altLists").ReplaceWith(xAltList);
                DraftGroupXElement.SetElementValue("customSizeDataID", CustomSizeID);
            }
            else
            {
                DraftGroupXElement = NewXElement();
            }

        }

        private XElement NewXElement()
        {
            XElement xAltList = new XElement("altLists");
            SavedGroup.AltIdList?.ForEach(id => xAltList.Add(new XElement("listID", id)));

            return
                new XElement("group",
                new XAttribute("groupID", SavedGroup.ID),
                new XAttribute("groupName", SavedGroup.Name),
                    new XElement("defaultListID", SavedGroup.DefaultListID),
                    /*xAltList*/
                    new XElement("altLists", SavedGroup.AltIdList != null ?
                    (from id in SavedGroup.AltIdList select new XElement("listID", id)) : null),
                    new XElement("customSizeDataID", SavedGroup.CustomSize));
        }

        public bool IsModified()
        {
            bool idChanged = SavedGroup.ID != GroupID;
            bool nameChanged = SavedGroup.Name != GroupName;
            bool defaultListIdChanged = SavedGroup.DefaultListID != DefaultListID;
            bool altListChanged = IsAltSizeListModified();
            bool customSizeIdChanged = IsCustomSizeIdModified();

            return idChanged || nameChanged || defaultListIdChanged || altListChanged || customSizeIdChanged;
        }

        public bool IsAltSizeListModified()
        {
            bool modified = false;

            bool savedEmpty = SavedGroup.AltIdList == null;
            bool includeDraft = HasAltList;
            bool draftEmpty = AltList == null;

            bool expr1 = savedEmpty && includeDraft && !draftEmpty;
            bool expr2 = !savedEmpty && (!includeDraft || draftEmpty);
            bool expr3 = false;

            if (!savedEmpty && !draftEmpty)
            {
                List<string> listA = (from item in SavedGroup.AltIdList orderby item select item).ToList();
                List<string> listB = (from item in AltList orderby item select item).ToList();

                expr3 = !listA.SequenceEqual(listB);
            }
            modified = expr1 || expr2 || expr3;
            
            return modified;
        }

        public bool IsCustomSizeIdModified()
        {
            bool modified = false;

            bool savedEmpty = SavedGroup.CustomSize == null || SavedGroup.CustomSize == string.Empty;
            bool includeDraft = HasCustomSize;
            bool draftEmpty = CustomSizeID == null || CustomSizeID == string.Empty;

            bool expr1 = savedEmpty && includeDraft && !draftEmpty;
            bool expr2 = !savedEmpty && (!includeDraft || draftEmpty);
            bool expr3 = false;

            if (!savedEmpty && !draftEmpty)
            {
                expr3 = SavedGroup.CustomSize != CustomSizeID;
            }

            modified = expr1 || expr2 || expr3;
            return modified;
        }
    }
}
