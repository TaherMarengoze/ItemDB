using System.Collections.Generic;
using System.Linq;

namespace CoreLibrary.Models
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
            // Save the reference of original SizeGroup ID to reference it incase of ID change
            refId = group.ID;

            // Save the reference of original SizeGroup object to reference it later if editing was canceled
            DraftSizeGroup = group;

            // Copy data in SizeGroup object being edited to temporary fields
            groupID = group.ID;
            groupName = group.Name;
            groupDefaultListID = group.DefaultListID;
            groupAltList = group.AltIdList?.ToList();
            groupCustomSizeID = group.CustomSize;
        }

        /// <summary>
        /// Stores the reference of the draft <see cref="SizeGroup"/> object.
        /// </summary>
        public SizeGroup DraftSizeGroup { get; private set; }

        public readonly string refId;
        /// <summary>
        /// Temporary input for <see cref="SizeGroup.ID"/>.
        /// </summary>
        public string groupID;
        /// <summary>
        /// Temporary input for <see cref="SizeGroup.Name"/>.
        /// </summary>
        public string groupName;
        /// <summary>
        /// Temporary input for <see cref="SizeGroup.DefaultListID"/>.
        /// </summary>
        public string groupDefaultListID;
        /// <summary>
        /// Temporary input for <see cref="SizeGroup.AltIdList"/>.
        /// </summary>
        public List<string> groupAltList;
        /// <summary>
        /// Temporary input for <see cref="SizeGroup.CustomSize"/>.
        /// </summary>
        public string groupCustomSizeID;

        public bool HasAltList { get; set; }
        public bool HasCustomSize { get; set; }

        /// <summary>
        /// Commit changes to the draft <see cref="SizeGroup"/> object.
        /// </summary>
        public void CommitChanges()
        {
            DraftSizeGroup.ID = groupID;
            DraftSizeGroup.Name = groupName;
            DraftSizeGroup.DefaultListID = groupDefaultListID;
            DraftSizeGroup.AltIdList = HasAltList ? groupAltList : null;
            DraftSizeGroup.CustomSize = HasCustomSize ? groupCustomSizeID : null;
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