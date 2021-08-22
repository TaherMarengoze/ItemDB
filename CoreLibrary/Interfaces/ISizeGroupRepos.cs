namespace CoreLibrary.Interfaces
{
    using Models;

    public interface ISizeGroupRepos
    {
        void Create(SizeGroup group);
        void Read();
        void Update(string refId, SizeGroup group);
        void Delete(string groupId);
    }
}