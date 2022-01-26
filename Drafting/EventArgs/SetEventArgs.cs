using System.Collections;

namespace Controllers
{
    public class SetEventArgs
    {
        public string OldID { get; set; }

        public string NewID { get; set; }

        public IList NewList { get; set; }
    }
}