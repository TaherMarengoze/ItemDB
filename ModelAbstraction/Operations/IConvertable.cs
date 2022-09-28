using System.Collections.Generic;

namespace Interfaces.Operations
{
    public interface IConvertable<TOut, TSource>
    {
        List<TOut> Transform(IEnumerable<TSource> source);
    }
}