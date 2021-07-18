namespace UserInterface.Models
{
    public class CopyOrder
    {
        public CopyOrder(string s, string t)
        {
            Source = s;
            Target = t;
        }

        public string Source { get; set; }
        public string Target { get; set; }

        public override string ToString()
        {
            return $"{Source} => {Target}";
        }
    }
}
