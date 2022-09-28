using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interfaces.Models;
using Interfaces.Operations;

namespace Modeling.ViewModels.Item
{
    public class ItemBasicView : IItemBasicView, IConvertable<ItemBasicView, IItem>
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public List<ItemBasicView> Transform(IEnumerable<IItem> source)
        {
            return
                source.Select(item =>
                new ItemBasicView { ID = item.ItemID, Name = item.BaseName }
                ).ToList();
        }
    }
}
