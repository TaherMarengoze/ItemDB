using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UserInterface
{
    using Interfaces;
    using Models;

    public class SizeGroupsXmlRepository : ISizeGroupRepos
    {
        private readonly XDocument dataSource;

        public SizeGroupsXmlRepository(XDocument source)
        {
            dataSource = source;
        }

        public void Create(SizeGroup group)
        {
            XElement content = SerializeSizeGroup(group);
            dataSource.Root.Add(content);
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        private XElement SerializeSizeGroup(SizeGroup group)
        {
            XElement altIdList = new XElement("altLists");
            group.AltIdList?.ForEach(id => altIdList.Add(new XElement("listID", id)));

            XElement draftSizeGroup =
                new XElement("group",
                new XAttribute("groupID", group.ID),
                new XAttribute("groupName", group.Name),
                    new XElement("defaultListID", group.DefaultListID),
                    altIdList,
                    new XElement("customSizeDataID", group.CustomSize));

            return draftSizeGroup;
        }
    }
}