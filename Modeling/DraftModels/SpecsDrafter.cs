
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.DraftModels
{
    public partial class SpecsDrafter : Interfaces.General.IDraftable
    {

        public SpecsDrafter()
        {
            DraftSpecs = new DataModels.Specs();
        }

        public SpecsDrafter(ISpecs editSpecs)
        {
            refId = editSpecs.ID;

            // Save edit object reference to call it on edit cancel
            DraftSpecs = editSpecs;

            // Copy edit object to temporary fields
            specsId = editSpecs.ID;
            specsName = editSpecs.Name;
            specsTxtPat = editSpecs.TextPattern;
            specsItems = editSpecs.SpecItems.Clone();
        }

        /// <summary>
        /// Temporary value holder for edit object ID,to refer to old ID value in case it was changed.
        /// </summary>
        private readonly string refId;

        public ISpecs DraftSpecs { get; set; }

        /// <summary>
        /// Temporary input for specs ID.
        /// </summary>
        public string specsId;

        /// <summary>
        /// Temporary input for specs name.
        /// </summary>
        public string specsName;

        /// <summary>
        /// Temporary input for specs text pattern.
        /// </summary>
        public string specsTxtPat;

        /// <summary>
        /// Temporary input for specs items.
        /// </summary>
        public List<ISpecsItem> specsItems;

        public void CommitChanges()
        {
            throw new NotImplementedException();
        }

        public void ClearDraft()
        {
            DraftSpecs = null;
        }

        public ISpecsItem DraftSpec { get; private set; }

        public void NewDraftSpec()
        {
            specDrafter = new SpecDrafter(this);

            DraftSpec = new DataModels.SpecsItem();

            int lastIdx = DraftSpecs.SpecItems.Count();
            int newIdx = lastIdx + 1;

            DraftSpec.Index = newIdx;
            DraftSpec.Name = $"SI{newIdx:000}";
        }

        public void EditSpec(int specIndex)
        {
            ISpecsItem spec =
                DraftSpecs.SpecItems//.ToList()[specIndex];
                .FirstOrDefault(si => si.Index == specIndex);

            specDrafter = new SpecDrafter(spec);
        }

        private SpecDrafter specDrafter;

        private class SpecDrafter
        {
            //private readonly SpecsDrafter parent;

            public SpecDrafter(SpecsDrafter specsDrafter)
            {
                //parent = specsDrafter;

                DraftSpec = new DataModels.SpecsItem();

                int lastIdx = specsDrafter.DraftSpecs.SpecItems.Count();
                int newIdx = lastIdx + 1;

                DraftSpec.Index = newIdx;
                DraftSpec.Name = $"SI{newIdx:000}";
            }

            public SpecDrafter(ISpecsItem editSpec)
            {
                if (editSpec.ListEntries != null)
                {

                }

                if (editSpec.CustomInputID != null)
                {

                }
            }

            public ISpecsItem DraftSpec { get; set; }
        }
    }
}