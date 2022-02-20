using System;
using ClientService.Contracts;
using ClientService.Data;
using Interfaces.Models;
using Interfaces.Operations;
using Modeling.DataModels;

namespace ClientService.Brokers
{
    public class BrokerSelector<T>
        where T : IFieldList, new()
    {
        public static void Assign(out IBroker<IFieldList> broker,
            out IProvider<IFieldList> provider)
        {
            Type type = typeof(T);

            if (type == typeof(SizeList))
            {
                broker = new SizeListBroker();
                provider = new SizeProvider();
            }
            else if (type == typeof(BrandList))
            {
                broker = new BrandListBroker();
                provider = new BrandProvider();
            }
            else if (type == typeof(EndList))
            {
                broker = new EndListBroker();
                provider = new EndProvider();
            }
            else
            {
                broker = null;
                provider = null;
            }
        }
    }
}
