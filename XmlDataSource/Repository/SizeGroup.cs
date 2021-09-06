﻿
using Interfaces.Models;
using Interfaces.Operations;
using System;
using System.Linq;
using System.Xml.Linq;
using XmlDataSource.Serialization;

namespace XmlDataSource.Repository
{
    public class SizeGroup : IEntityRepo<ISizeGroup>
    {
        private readonly XDocument dataSource;

        public SizeGroup(XDocument source)
        {
            dataSource = source;
        }

        public SizeGroup()
        {
            dataSource = new XDocument(); // this should be size group XDocument
        }

        public void Create(ISizeGroup entity)
        {
            XElement content = Entity.Serialize(entity);
            dataSource.Root.Add(content);
        }

        public ISizeGroup Read() => throw new NotImplementedException();

        public void Update(string refId, ISizeGroup entity)
        {
            XElement newContent = Entity.Serialize(entity);
            XElement oldContent = GetElement(refId);
            oldContent.ReplaceWith(newContent);
        }

        public void Delete(string entityId)
        {
            GetElement(entityId).Remove();
        }

        private XElement GetElement(string entityId)
        {
            return
                dataSource.Descendants("group")
                .Where(node => node.Attribute("groupID").Value == entityId)
                .FirstOrDefault();
        }
    }
}