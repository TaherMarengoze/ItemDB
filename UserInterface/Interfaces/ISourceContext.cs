namespace UserInterface.Interfaces
{
    public interface ISourceContext
    {
        void Load();

        void Save();

        void Save(object arg);
    }
}