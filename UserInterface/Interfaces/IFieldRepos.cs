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
        void AddFieldList(IBasicList list);

        IBasicList ReadFieldList(string listId);

        void UpdateFieldList(string refId, IBasicList list);

        /// <summary>
        /// Deletes a field from its data source.
        /// </summary>
        /// <param name="listId">The ID of the field item to be deleted.</param>
        void DeleteFieldList(string listId);
    }
}
