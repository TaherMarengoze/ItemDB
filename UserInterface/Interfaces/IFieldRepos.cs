using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Interfaces
{
    using Enums;

    public interface IFieldRepos
    {
        void AddField(IBasicList field);

        IBasicList ReadField(string fieldItemId);

        void UpdateField(string refId, IBasicList field);

        /// <summary>
        /// Deletes a field from its data source.
        /// </summary>
        /// <param name="fieldItemId">The ID of the field item to be deleted.</param>
        void DeleteField(string fieldItemId);
    }
}
