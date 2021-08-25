namespace CoreLibrary.Interfaces
{
    using Models;

    public interface ISizeGroupRepos
    {
        void Create(ISizeGroup group);
        void Read();
        void Update(string refId, ISizeGroup group);
        void Delete(string groupId);
    }
}