using UserInterface.Interfaces;

namespace UserInterface.Models
{
    public class BasicView : IBasicView
    {
        public BasicView(string id, string name)
        {
            ID = id;
            Name = name;
        }

        public string ID { get; private set; }
        public string Name { get; private set; }

        public override string ToString()
        {
            return $"{ ID }\t:\t{ Name }";
        }
    }
}