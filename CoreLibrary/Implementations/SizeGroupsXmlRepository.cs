using System;
using System.Linq;
using System.Xml.Linq;

namespace CoreLibrary
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

        public void Create(ISizeGroup group)
        {
            XElement content = SerializeSizeGroup(group);
            dataSource.Root.Add(content);
        }

        public void Read() => throw new NotImplementedException();

        public void Update(string refId, ISizeGroup group)
        {
            XElement content = GetSizeGroup(refId);

            if (content != null)
            {
                XElement xAltList = new XElement("altLists");
                group.AltIdList?.ForEach(id => xAltList.Add(new XElement("listID", id)));

                content.SetAttributeValue("groupID", group.ID);
                content.SetAttributeValue("groupName", group.Name);
                content.SetElementValue("defaultListID", group.DefaultListID);
                content.Element("altLists").ReplaceWith(xAltList);

                //content.SetElementValue("customSizeDataID", group.CustomSize ?? string.Empty);
                if (group.CustomSize != null)
                {
                    content.SetElementValue("customSizeDataID", group.CustomSize);
                }
                else
                {
                    content.Element("customSizeDataID").ReplaceWith(new XElement("customSizeDataID"));
                }
            }
        }

        public void Delete(string groupId)
        {
            GetSizeGroup(groupId).Remove();
        }

        private XElement GetSizeGroup(string groupId)
        {
            return
                dataSource.Descendants("group")
                .Where(g => g.Attribute("groupID").Value == groupId)
                .FirstOrDefault();
        }

        private XElement SerializeSizeGroup(ISizeGroup group)
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