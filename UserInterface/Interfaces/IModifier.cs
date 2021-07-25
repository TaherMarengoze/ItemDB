
namespace UserInterface.Interfaces
{
    public interface ISourceModifier
    {
        void AddItem(IItem item);

        void ModifyItem(string existingId, IItem data);
    }
}
