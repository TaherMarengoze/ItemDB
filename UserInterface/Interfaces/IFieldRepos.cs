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
        void AddFieldList(IBasicList fieldList);

        IBasicList ReadFieldList(string fieldItemId);

        void UpdateFieldList(string refId, IBasicList fieldList);

        /// <summary>
        /// Deletes a field from its data source.
        /// </summary>
        /// <param name="fieldListId">The ID of the field item to be deleted.</param>
        void DeleteFieldList(string fieldListId);
    }
}
