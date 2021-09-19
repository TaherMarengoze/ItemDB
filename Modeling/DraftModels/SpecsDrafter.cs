
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Modeling.DraftModels
{
    public class SpecsDrafter : Interfaces.General.IDraftable
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

    }
}