namespace ClientService.Contracts
{
    public interface IProvider<T> : IBaseProvider<T>, ITransformable<T>
    {
    }
}