using System.Collections.Generic;

namespace ClientService.Contracts
{
    public interface ITransformable<TSource>
    {
        List<TViewModel> View<TViewModel>()
            where TViewModel : IConvertable<TViewModel, TSource>, new();
    }
}