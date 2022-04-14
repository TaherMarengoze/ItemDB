using System.Collections.Generic;

namespace ClientService.Contracts
{
    public interface IConvertable<TOut, TSource>
    {
        List<TOut> Transform(IEnumerable<TSource> source);
    }
}
