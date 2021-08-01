using System;
using System.Linq;
using System.Xml.Linq;

namespace UserInterface
{
    using Enums;
    using Factory;
    using Interfaces;

    public class SizesRepoX : IFieldRepos
    {
        public SizesRepoX()
        {

        }

        public void AddField(IBasicList field) => throw new NotImplementedException();

        public void DeleteField(string fieldId)
        {
            GetField(fieldId).Remove();
        }

        public IBasicList ReadField(string fieldItemId) => throw new NotImplementedException();

        public void UpdateField(string refId, IBasicList field) => throw new NotImplementedException();

        private XElement GetField(string fieldId)
        {
            return
                Program.xDataDocs.Sizes.Descendants("sizeList")
                .Where(list => list.Attribute("listID").Value == fieldId)
                .FirstOrDefault();
        }
    }
}
