using Interfaces.Models;
using Modeling.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XmlDataSource.IO
{
    internal static class Deserialize
    {
        internal static IFieldList SizeXElement(XElement list)
        {
            return new SizeList()
            {
                ID = list.Attribute("listID").Value,
                Name = list.Attribute("name").Value,
                List = new ObservableCollection<string>(list.Descendants("size")
                .Select(entry => entry.Value).ToList())
            };
        }
    }
}
